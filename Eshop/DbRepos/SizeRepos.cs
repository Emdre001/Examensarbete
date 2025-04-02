using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Configuration;
using Models;
using Models.DTO;
using DbModels;
using DbContext;

namespace DbRepos;

public class SizeDbrepos
{
    private readonly ILogger<SizeDbrepos> _logger;
    private readonly MainDbContext _dbContext;

    #region contructors
    public SizeDbrepos(ILogger<SizeDbrepos> logger, MainDbContext context)
    {
        _logger = logger;
        _dbContext = context;
    }
    #endregion

    public async Task<ResponseItemDTO<ISize>> ReadItemAsync(Guid id, bool flat)
    {
        IQueryable<DbSize> query;
        if (!flat)
        {
            query = _dbContext.Sizes.AsNoTracking()
                .Include(i => i.DbProduct)
                .Where(i => i.SizeId == id);
        }
        else
        {
            query = _dbContext.Sizes.AsNoTracking()
                .Where(i => i.SizeId == id);
        }

        var resp = await query.FirstOrDefaultAsync<ISize>();
        return new ResponseItemDTO<ISize>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = resp
        };
    }

    public async Task<ResponsePageDTO<ISize>> ReadItemsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize)
    {
        filter ??= "";
        IQueryable<DbSize> query;
        if (flat)
        {
            query = _dbContext.Sizes.AsNoTracking();
        }
        else
        {
            query = _dbContext.Sizes.AsNoTracking()
                .Include(i => i.DbProduct);
        }

        var ret = new ResponsePageDTO<ISize>()
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

            .ToListAsync<ISize>(),

            PageNr = pageNumber,
            PageSize = pageSize
        };
        return ret;
    }

    public async Task<ResponseItemDTO<ISize>> DeleteItemAsync(Guid id)
    {
        var query1 = _dbContext.Sizes
            .Where(i => i.SizeId == id);

        var item = await query1.FirstOrDefaultAsync<DbSize>();

        //If the item does not exists
        if (item == null) throw new ArgumentException($"Item {id} is not existing");

        //delete in the database model
        _dbContext.Sizes.Remove(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        return new ResponseItemDTO<ISize>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = item
        };
    }

    public async Task<ResponseItemDTO<ISize>> UpdateItemAsync(ProductDTO itemDTO)
    {
        var query1 = _dbContext.Sizes
            .Where(i => i.SizeId == itemDTO.SizeId);
        var item = await query1
                .Include(i => i.ProductDbM)
                .FirstOrDefaultAsync<DbSize>();

        //If the item does not exists
        if (item == null) throw new ArgumentException($"Item {itemDTO.SizeId} is not existing");

        //transfer any changes from DTO to database objects
        //Update individual properties 
        item.UpdateFromDTO(itemDTO);

        //Update navigation properties
        await navProp_ItemCUdto_to_ItemDbM(itemDTO, item);

        //write to database model
        _dbContext.Sizes.Update(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        //return the updated item in non-flat mode
        return await ReadItemAsync(item.SizeId, false);    
    }

    public async Task<ResponseItemDTO<ISize>> CreateItemAsync(ProductDTO itemDTO)
    {
        if (itemDTO.SizeId != null)
            throw new ArgumentException($"{nameof(itemDTO.SizeId)} must be null when creating a new object");

        //transfer any changes from DTO to database objects
        //Update individual properties
        var item = new DbSize(itemDTO);

        //Update navigation properties
        await navProp_ItemCUdto_to_ItemDbM(itemDTO, item);

        //write to database model
        _dbContext.Sizes.Add(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        //return the updated item in non-flat mode
        return await ReadItemAsync(item.SizeId, false);    
    }
  
}
