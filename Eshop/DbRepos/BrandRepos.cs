using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Models;
using Models.DTO;
using DbModels;
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

    public async Task<ResponseItemDto<IShoeBrand>> ReadItemAsync(Guid id, bool flat)
    {
        IQueryable<ShoeBrandDbM> query;
        if (!flat)
        {
            query = _dbContext.ShoeBrands.AsNoTracking()
                .Include(i => i.ShoeBrandDbM)
                .Where(i => i.BrandId == id);
        }
        else
        {
            query = _dbContext.ShoeBrands.AsNoTracking()
                .Where(i => i.BrandId == id);
        }

        var resp = await query.FirstOrDefaultAsync<IShoeBrand>();
        return new ResponseItemDto<IShoeBrand>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = resp
        };
    }

    public async Task<ResponsePageDto<IShoeBrand>> ReadItemsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize)
    {
        filter ??= "";
        IQueryable<ShoeBrandDbM> query;
        if (flat)
        {
            query = _dbContext.ShoeBrands.AsNoTracking();
        }
        else
        {
            query = _dbContext.Animals.AsNoTracking()
                .Include(i => i.ShoeBrandDbM);
        }

        var ret = new ResponsePageDto<IShoeBrand>()
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

            .ToListAsync<IShoeBrand>(),

            PageNr = pageNumber,
            PageSize = pageSize
        };
        return ret;
    }

    public async Task<ResponseItemDto<IShoeBrand>> DeleteItemAsync(Guid id)
    {
        var query1 = _dbContext.Animals
            .Where(i => i.BrandId == id);

        var item = await query1.FirstOrDefaultAsync<ShoeBrandDbM>();

        //If the item does not exists
        if (item == null) throw new ArgumentException($"Item {id} is not existing");

        //delete in the database model
        _dbContext.Animals.Remove(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        return new ResponseItemDto<IShoeBrand>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = item
        };
    }

    public async Task<ResponseItemDto<IShoeBrand>> UpdateItemAsync(ShoeBrandDTO itemDto)
    {
        var query1 = _dbContext.Animals
            .Where(i => i.BrandId == itemDto.BrandId);
        var item = await query1
                .Include(i => i.ZooDbM)
                .FirstOrDefaultAsync<ShoeBrandDbM>();

        //If the item does not exists
        if (item == null) throw new ArgumentException($"Item {itemDto.BrandId} is not existing");

        //transfer any changes from DTO to database objects
        //Update individual properties 
        item.UpdateFromDTO(itemDto);

        //Update navigation properties
        await navProp_ItemCUdto_to_ItemDbM(itemDto, item);

        //write to database model
        _dbContext.Animals.Update(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        //return the updated item in non-flat mode
        return await ReadItemAsync(item.BrandId, false);    
    }

    public async Task<ResponseItemDto<IShoeBrand>> CreateItemAsync(ShoeBrandDTO itemDto)
    {
        if (itemDto.BrandId != null)
            throw new ArgumentException($"{nameof(itemDto.BrandId)} must be null when creating a new object");

        //transfer any changes from DTO to database objects
        //Update individual properties
        var item = new ShoeBrandDbM(itemDto);

        //Update navigation properties
        await navProp_ItemCUdto_to_ItemDbM(itemDto, item);

        //write to database model
        _dbContext.Animals.Add(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        //return the updated item in non-flat mode
        return await ReadItemAsync(item.BrandId, false);    
    }

    private async Task navProp_ItemCUdto_to_ItemDbM(ShoeBrandDTO itemDtoSrc, ShoeBrandDbM itemDst)
    {
        //update zoo nav props
        var zoo = await _dbContext.products.FirstOrDefaultAsync(
            a => (a.productId == itemDtoSrc.productId));

        if (zoo == null)
            throw new ArgumentException($"Item id {itemDtoSrc.productId} not existing");

        itemDst.productDbM = product;
    }
}
