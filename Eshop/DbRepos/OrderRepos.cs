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
                .Include(i => i.DbProduct)
                .Where(i => i.OrderId == id);
        }
        else
        {
            query = _dbContext.Orders.AsNoTracking()
                .Where(i => i.OrderId == id);
        }   

        var resp =  await query.FirstOrDefaultAsync<IOrder>();
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
                .Include(i => i.AnimalsDbM)
                .Include(i => i.EmployeesDbM);
        }

        return new ResponsePageDTO<IOrder>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            DbItemsCount = await query

            //Adding filter functionality
            .Where(i => (i.Seeded == seeded) && 
                        (i.Name.ToLower().Contains(filter) ||
                         i.City.ToLower().Contains(filter) ||
                         i.Country.ToLower().Contains(filter))).CountAsync(),

            PageItems = await query

            //Adding filter functionality
            .Where(i => (i.Seeded == seeded) && 
                        (i.Name.ToLower().Contains(filter) ||
                         i.City.ToLower().Contains(filter) ||
                         i.Country.ToLower().Contains(filter)))

            //Adding paging
            .Skip(pageNumber * pageSize)
            .Take(pageSize)

            .ToListAsync<IOrder>(),

            PageNr = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<ResponseItemDTO<IOrder>> DeleteItemAsync(Guid id)
    {
        //Find the instance with matching id
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

    public async Task<ResponseItemDTO<IOrder>> UpdateItemAsync(OrderDTO itemDto)
    {
        //Find the instance with matching id and read the navigation properties.
        var query1 = _dbContext.Orders
            .Where(i => i.OrderId == itemDto.OrderId);
        var item = await query1
            .Include(i => i.AnimalsDbM)
            .Include(i => i.EmployeesDbM)
            .FirstOrDefaultAsync<DbOrder>();

        //If the item does not exists
        if (item == null) throw new ArgumentException($"Item {itemDto.OrderId} is not existing");

        //transfer any changes from DTO to database objects
        //Update individual properties
        item.UpdateFromDTO(itemDto);

        //Update navigation properties
        await navProp_Itemdto_to_ItemDbM(itemDto, item);

        //write to database model
        _dbContext.Orders.Update(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        //return the updated item in non-flat mode
        return await ReadItemAsync(item.OrderId, false);    
    }

    public async Task<ResponseItemDTO<IOrder>> CreateItemAsync(OrderDTO itemDto)
    {
        if (itemDto.OrderId != null)
            throw new ArgumentException($"{nameof(itemDto.OrderId)} must be null when creating a new object");

        //transfer any changes from DTO to database objects
        //Update individual properties Zoo
        var item = new DbOrder(itemDto);

        //Update navigation properties
        await navProp_Itemdto_to_ItemDbM(itemDto, item);

        //write to database model
        _dbContext.Orders.Add(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();
        
        //return the updated item in non-flat mode
        return await ReadItemAsync(item.OrderId, false);
    }

    //from all Guid relationships in _itemDtoSrc finds the corresponding object in the database and assigns it to _itemDst 
    //as navigation properties. Error is thrown if no object is found corresponing to an id.
    private async Task navProp_Itemdto_to_ItemDbM(OrderDTO itemDtoSrc, DbOrder itemDst)
    {
        //update AnimalsDbM from list
        List<AnimalDbM> Animals = null;
        if (itemDtoSrc.AnimalsId != null)
        {
            Animals = new List<AnimalDbM>();
            foreach (var id in itemDtoSrc.AnimalsId)
            {
                var p = await _dbContext.Animals.FirstOrDefaultAsync(i => i.AnimalId == id);
                if (p == null)
                    throw new ArgumentException($"Item id {id} not existing");

                Animals.Add(p);
            }
        }
        itemDst.AnimalsDbM = Animals;

        //update EmployeessDbM from list
        List<EmployeeDbM> Employees = null;
        if (itemDtoSrc.EmployeesId != null)
        {
            Employees = new List<EmployeeDbM>();
            foreach (var id in itemDtoSrc.EmployeesId)
            {
                var p = await _dbContext.Employees.FirstOrDefaultAsync(i => i.EmployeeId == id);
                if (p == null)
                    throw new ArgumentException($"Item id {id} not existing");

                Employees.Add(p);
            }
        }
        itemDst.EmployeesDbM = Employees;
    }
}
