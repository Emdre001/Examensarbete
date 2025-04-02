using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;

using Models;
using Models.DTO;
using DbModels;
using DbContext;

namespace DbRepos;

public class ColorDbRepos
{
    private readonly ILogger<ColorDbRepos> _logger;
    private readonly MainDbContext _dbContext;

    #region contructors
    public ColorDbRepos(ILogger<ColorDbRepos> logger, MainDbContext context)
    {
        _logger = logger;
        _dbContext = context;
    }
    #endregion

    public async Task<ResponseItemDTO<IColor>> ReadItemAsync(Guid id, bool flat)
    {
        IQueryable<DbColor> query;
        if (!flat)
        {
            query = _dbContext.Colors.AsNoTracking()
                .Include(i => i.DbColor)
                .Where(i => i.ColorId == id);
        }
        else
        {
            query = _dbContext.Colors.AsNoTracking()
                .Where(i => i.ColorId == id);
        }

        var resp = await query.FirstOrDefaultAsync<IColor>();
        return new ResponseItemDTO<IColor>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = resp
        };
    }

    public async Task<ResponsePageDTO<IColor>> ReadItemsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize)
    {
        filter ??= "";
        IQueryable<DbColor> query;
        if (flat)
        {
            query = _dbContext.Colors.AsNoTracking();
        }
        else
        {
            query = _dbContext.Colors.AsNoTracking()
                .Include(i => i.DbColor);
        }

        var ret = new ResponsePageDTO<IColor>()
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

            .ToListAsync<IColor>(),

            PageNr = pageNumber,
            PageSize = pageSize
        };
        return ret;
    }

    public async Task<ResponseItemDTO<IColor>> DeleteItemAsync(Guid id)
    {
        var query1 = _dbContext.Colors
            .Where(i => i.ColorId == id);

        var item = await query1.FirstOrDefaultAsync<DbColor>();

        //If the item does not exists
        if (item == null) throw new ArgumentException($"Item {id} is not existing");

        //delete in the database model
        _dbContext.Colors.Remove(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        return new ResponseItemDTO<IColor>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = item
        };
    }

    public async Task<ResponseItemDTO<IColor>> UpdateItemAsync(ColorDTO itemDTO)
    {
        var query1 = _dbContext.Colors
            .Where(i => i.ColorId == itemDTO.ColorId);
        var item = await query1
                .Include(i => i.ZooDbM)
                .FirstOrDefaultAsync<DbColor>();

        //If the item does not exists
        if (item == null) throw new ArgumentException($"Item {itemDTO.ColorId} is not existing");

        //transfer any changes from DTO to database objects
        //Update individual properties 
        item.UpdateFromDTO(itemDTO);

        //Update navigation properties
        await navProp_ItemCUdto_to_ItemDbM(itemDTO, item);

        //write to database model
        _dbContext.Colors.Update(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        //return the updated item in non-flat mode
        return await ReadItemAsync(item.ColorId, false);    
    }

    public async Task<ResponseItemDTO<IColor>> CreateItemAsync(ColorDTO itemDTO)
    {
        if (itemDTO.ColorId != null)
            throw new ArgumentException($"{nameof(itemDTO.ColorId)} must be null when creating a new object");

        //transfer any changes from DTO to database objects
        //Update individual properties
        var item = new DbColor(itemDTO);

        //Update navigation properties
        await navProp_ItemCUdto_to_ItemDbM(itemDTO, item);

        //write to database model
        _dbContext.Colors.Add(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        //return the updated item in non-flat mode
        return await ReadItemAsync(item.ColorId, false);    
    }
    /*
    private async Task navProp_ItemCUdto_to_ItemDbM(ColorDto itemDtoSrc, ColorDbM itemDst)
    {
        //update zoo nav props
        var zoo = await _dbContext.Zoos.FirstOrDefaultAsync(
            a => (a.ZooId == itemDtoSrc.ZooId));

        if (zoo == null)
            throw new ArgumentException($"Item id {itemDtoSrc.ZooId} not existing");

        itemDst.ZooDbM = zoo;
    }
    */
}
