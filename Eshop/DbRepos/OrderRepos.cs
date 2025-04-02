using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;

using Models;
using Models.DTO;
using DbModels;
using DbContext;

namespace DbRepos;

public class OrderDbRepos
{
    private readonly ILogger<OrderDbRepos> _logger;
    private readonly MainDbContext _dbContext;

    #region contructors
    public OrderDbRepos(ILogger<OrderDbRepos> logger, MainDbContext context)
    {
        _logger = logger;
        _dbContext = context;
    }
    #endregion

    public async Task<ResponseItemDTO<IOrder>> ReadItemAsync(Guid id, bool flat)
    {
        IQueryable<DbOrder> query;
        if (!flat)
        {
            query = _dbContext.Orders.AsNoTracking()
                .Include(i => i.DbOrder)
                .Where(i => i.OrderId == id);
        }
        else
        {
            query = _dbContext.Orders.AsNoTracking()
                .Where(i => i.OrderId == id);
        }

        var resp = await query.FirstOrDefaultAsync<IOrder>();
        return new ResponseItemDTO<IOrder>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = resp
        };
    }

    public async Task<ResponsePageDTO<IOrder>> ReadItemsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize)
    {
        filter ??= "";
        IQueryable<DbOrder> query;
        if (flat)
        {
            query = _dbContext.Orders.AsNoTracking();
        }
        else
        {
            query = _dbContext.Orders.AsNoTracking()
                .Include(i => i.ProductDbM);
        }

        var ret = new ResponsePageDTO<IOrder>()
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

            .ToListAsync<IOrder>(),

            PageNr = pageNumber,
            PageSize = pageSize
        };
        return ret;
    }

    public async Task<ResponseItemDTO<IOrder>> DeleteItemAsync(Guid id)
    {
        var query1 = _dbContext.Orders
            .Where(i => i.OrderId == id);

        var item = await query1.FirstOrDefaultAsync<DbOrder>();

        //If the item does not exists
        if (item == null) throw new ArgumentException($"Item {id} is not existing");

        //delete in the database model
        _dbContext.Orders.Remove(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        return new ResponseItemDTO<IOrder>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = item
        };
    }

    public async Task<ResponseItemDTO<IOrder>> UpdateItemAsync(OrderDTO itemDTO)
    {
        var query1 = _dbContext.Orders
            .Where(i => i.OrderId == itemDTO.OrderId);
        var item = await query1
                .Include(i => i.ProductDbM)
                .FirstOrDefaultAsync<DbOrder>();

        //If the item does not exists
        if (item == null) throw new ArgumentException($"Item {itemDTO.OrderId} is not existing");

        //transfer any changes from DTO to database objects
        //Update individual properties 
        item.UpdateFromDTO(itemDTO);

        //Update navigation properties
        await navProp_ItemCUdto_to_ItemDbM(itemDTO, item);

        //write to database model
        _dbContext.Orders.Update(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        //return the updated item in non-flat mode
        return await ReadItemAsync(item.OrderId, false);    
    }

    public async Task<ResponseItemDTO<IOrder>> CreateItemAsync(OrderDTO itemDTO)
    {
        if (itemDTO.OrderId != null)
            throw new ArgumentException($"{nameof(itemDTO.OrderId)} must be null when creating a new object");

        //transfer any changes from DTO to database objects
        //Update individual properties
        var item = new DbOrder(itemDTO);

        //Update navigation properties
        await navProp_ItemCUdto_to_ItemDbM(itemDTO, item);

        //write to database model
        _dbContext.Orders.Add(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        //return the updated item in non-flat mode
        return await ReadItemAsync(item.OrderId, false);    
    }
    /*
    private async Task navProp_ItemCUdto_to_ItemDbM(AnimalCuDto itemDtoSrc, OrderDbM itemDst)
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
