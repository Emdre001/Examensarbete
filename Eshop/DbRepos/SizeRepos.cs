using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;

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

    public async Task<ResponseItemDto<IShoeSize>> ReadItemAsync(Guid id, bool flat)
    {
        IQueryable<ShoeSizeDbM> query;
        if (!flat)
        {
            query = _dbContext.Sizes.AsNoTracking()
                .Include(i => i.ProductDbM)
                .Where(i => i.SizeId == id);
        }
        else
        {
            query = _dbContext.Sizes.AsNoTracking()
                .Where(i => i.SizeId == id);
        }

        var resp = await query.FirstOrDefaultAsync<IShoeSize>();
        return new ResponseItemDto<IShoeSize>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = resp
        };
    }

    public async Task<ResponsePageDto<IShoeSize>> ReadItemsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize)
    {
        filter ??= "";
        IQueryable<ShoeSizeDbM> query;
        if (flat)
        {
            query = _dbContext.Sizes.AsNoTracking();
        }
        else
        {
            query = _dbContext.Sizes.AsNoTracking()
                .Include(i => i.ProductDbM);
        }

        var ret = new ResponsePageDto<IShoeSize>()
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

            .ToListAsync<IShoeSize>(),

            PageNr = pageNumber,
            PageSize = pageSize
        };
        return ret;
    }

    public async Task<ResponseItemDto<IShoeSize>> DeleteItemAsync(Guid id)
    {
        var query1 = _dbContext.Sizes
            .Where(i => i.SizeId == id);

        var item = await query1.FirstOrDefaultAsync<ShoeSizeDbM>();

        //If the item does not exists
        if (item == null) throw new ArgumentException($"Item {id} is not existing");

        //delete in the database model
        _dbContext.Sizes.Remove(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        return new ResponseItemDto<IShoeSize>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = item
        };
    }

    public async Task<ResponseItemDto<IShoeSize>> UpdateItemAsync(AnimalCuDto itemDto)
    {
        var query1 = _dbContext.Sizes
            .Where(i => i.SizeId == itemDto.SizeId);
        var item = await query1
                .Include(i => i.ProductDbM)
                .FirstOrDefaultAsync<ShoeSizeDbM>();

        //If the item does not exists
        if (item == null) throw new ArgumentException($"Item {itemDto.SizeId} is not existing");

        //transfer any changes from DTO to database objects
        //Update individual properties 
        item.UpdateFromDTO(itemDto);

        //Update navigation properties
        await navProp_ItemCUdto_to_ItemDbM(itemDto, item);

        //write to database model
        _dbContext.Sizes.Update(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        //return the updated item in non-flat mode
        return await ReadItemAsync(item.SizeId, false);    
    }

    public async Task<ResponseItemDto<IShoeSize>> CreateItemAsync(AnimalCuDto itemDto)
    {
        if (itemDto.SizeId != null)
            throw new ArgumentException($"{nameof(itemDto.SizeId)} must be null when creating a new object");

        //transfer any changes from DTO to database objects
        //Update individual properties
        var item = new ShoeSizeDbM(itemDto);

        //Update navigation properties
        await navProp_ItemCUdto_to_ItemDbM(itemDto, item);

        //write to database model
        _dbContext.Sizes.Add(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        //return the updated item in non-flat mode
        return await ReadItemAsync(item.SizeId, false);    
    }
    /*
    private async Task navProp_ItemCUdto_to_ItemDbM(AnimalCuDto itemDtoSrc, ShoeSizeDbM itemDst)
    {
        //update zoo nav props
        var zoo = await _dbContext.Zoos.FirstOrDefaultAsync(
            a => (a.ZooId == itemDtoSrc.ZooId));

        if (zoo == null)
            throw new ArgumentException($"Item id {itemDtoSrc.ZooId} not existing");

        itemDst.ProductDbM = zoo;
    }
    */
}
