using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Models;
using Models.DTO;
using DbContext;

namespace DbRepos;

public class BrandDbRepos
{
    private readonly ILogger<BrandDbRepos> _logger;
    private readonly MainDbContext _dbContext;

    #region contructors
    public BrandDbRepos(ILogger<BrandDbRepos> logger, MainDbContext context)
    {
        _logger = logger;
        _dbContext = context;
    }
    #endregion

    public async Task<ResponseItemDTO<IBrand>> ReadItemAsync(Guid id, bool flat)
    {
        IQueryable<DbBrand> query;
        if (!flat)
        {
            query = _dbContext.Brands.AsNoTracking()
                .Include(i => i.BrandDbM)
                .Where(i => i.BrandId == id);
        }
        else
        {
            query = _dbContext.Brands.AsNoTracking()
                .Where(i => i.BrandId == id);
        }

        var resp = await query.FirstOrDefaultAsync<IBrand>();
        return new ResponseItemDTO<IBrand>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = resp
        };
    }

    public async Task<ResponsePageDTO<IBrand>> ReadItemsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize)
    {
        filter ??= "";
        IQueryable<DbBrand> query;
        if (flat)
        {
            query = _dbContext.Brands.AsNoTracking();
        }
        else
        {
            query = _dbContext.Brands.AsNoTracking()
                .Include(i => i.DbBrand);
        }

        var ret = new ResponsePageDTO<IBrand>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            DbItemsCount = await query

            //Adding filter functionality
            .Where(i => (i.Seeded == seeded) && 
                        (i.Name.ToLower().Contains(filter) ||
                         i.strMood.ToLower().Contains(filter) ||
                         i.strKind.ToLower().Contains(filter) ||
                         i.Age.ToString().Contains(filter) ||
                         i.Description.ToLower().Contains(filter))).CountAsync(),

            PageItems = await query

            //Adding filter functionality
            .Where(i => (i.Seeded == seeded) && 
                        (i.Name.ToLower().Contains(filter) ||
                         i.strMood.ToLower().Contains(filter) ||
                         i.strKind.ToLower().Contains(filter) ||
                         i.Age.ToString().Contains(filter) ||
                         i.Description.ToLower().Contains(filter)))

            //Adding paging
            .Skip(pageNumber * pageSize)
            .Take(pageSize)

            .ToListAsync<IBrand>(),

            PageNr = pageNumber,
            PageSize = pageSize
        };
        return ret;
    }

    public async Task<ResponseItemDTO<IBrand>> DeleteItemAsync(Guid id)
    {
        var query1 = _dbContext.Brands
            .Where(i => i.BrandId == id);

        var item = await query1.FirstOrDefaultAsync<DbBrand>();

        //If the item does not exists
        if (item == null) throw new ArgumentException($"Item {id} is not existing");

        //delete in the database model
        _dbContext.Animals.Remove(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        return new ResponseItemDTO<IBrand>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = item
        };
    }

    public async Task<ResponseItemDTO<IBrand>> UpdateItemAsync(BrandDTO itemDTO)
    {
        var query1 = _dbContext.Brands
            .Where(i => i.BrandId == itemDTO.BrandId);
        var item = await query1
                .Include(i => i.ZooDbM)
                .FirstOrDefaultAsync<DbBrand>();

        //If the item does not exists
        if (item == null) throw new ArgumentException($"Item {itemDTO.BrandId} is not existing");

        //transfer any changes from DTO to database objects
        //Update individual properties 
        item.UpdateFromDTO(itemDTO);

        //Update navigation properties
        await navProp_ItemCUdto_to_ItemDbM(itemDTO, item);

        //write to database model
        _dbContext.Animals.Update(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        //return the updated item in non-flat mode
        return await ReadItemAsync(item.BrandId, false);    
    }

    public async Task<ResponseItemDTO<IBrand>> CreateItemAsync(BrandDTO itemDTO)
    {
        if (itemDTO.BrandId != null)
            throw new ArgumentException($"{nameof(itemDTO.BrandId)} must be null when creating a new object");

        //transfer any changes from DTO to database objects
        //Update individual properties
        var item = new BrandDbM(itemDTO);

        //Update navigation properties
        await navProp_ItemCUdto_to_ItemDbM(itemDTO, item);

        //write to database model
        _dbContext.Animals.Add(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        //return the updated item in non-flat mode
        return await ReadItemAsync(item.BrandId, false);    
    }

    private async Task navProp_ItemCUdto_to_ItemDbM(BrandDTO itemDTOSrc, DbBrand itemDst)
    {
        //update zoo nav props
        var brand = await _dbContext.products.FirstOrDefaultAsync(
            a => (a.productId == itemDTOSrc.productId));

        if (brand == null)
            throw new ArgumentException($"Item id {itemDTOSrc.productId} not existing");

        itemDst.productDbM = product;
    }
}
