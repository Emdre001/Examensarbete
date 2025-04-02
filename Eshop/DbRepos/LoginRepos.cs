using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Models;
using Models.DTO;
using DbModels;
using DbContext;

namespace DbRepos;

public class LoginDbRepos
{
    private readonly ILogger<LoginDbRepos> _logger;
    private readonly MainDbContext _dbContext;

    #region contructors
    public LoginDbRepos(ILogger<LoginDbRepos> logger, MainDbContext context)
    {
        _logger = logger;
        _dbContext = context;
    }
    #endregion

    public async Task<ResponseItemDTO<ILogin>> ReadItemAsync(Guid id, bool flat)
    {
        IQueryable<DbLogin> query;
        if (!flat)
        {
            query = _dbContext.Logins.AsNoTracking()
                .Include(i => i.AnimalsDbM)
                .Include(i => i.EmployeesDbM)
                .Where(i => i.LoginId == id);
        }
        else
        {
            query = _dbContext.Logins.AsNoTracking()
                .Where(i => i.LoginId == id);
        }   

        var resp =  await query.FirstOrDefaultAsync<ILogin>();
        return new ResponseItemDTO<ILogin>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = resp
        };
    }

    public async Task<ResponsePageDTO<ILogin>> ReadItemsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize)
    {
        filter ??= "";
        IQueryable<DbLogin> query;
        if (flat)
        {
            query = _dbContext.Logins.AsNoTracking();
        }
        else
        {
            query = _dbContext.Logins.AsNoTracking()
                .Include(i => i.AnimalsDbM)
                .Include(i => i.EmployeesDbM);
        }

        return new ResponsePageDTO<ILogin>()
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

            .ToListAsync<ILogin>(),

            PageNr = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<ResponseItemDTO<ILogin>> DeleteItemAsync(Guid id)
    {
        //Find the instance with matching id
        var query1 = _dbContext.Logins
            .Where(i => i.LoginId == id);
        var item = await query1.FirstOrDefaultAsync<DbLogin>();

        //If the item does not exists
        if (item == null) throw new ArgumentException($"Item {id} is not existing");

        //delete in the database model
        _dbContext.Logins.Remove(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        return new ResponseItemDTO<ILogin>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = item
        };
    }

    public async Task<ResponseItemDTO<ILogin>> UpdateItemAsync(LoginDTO itemDto)
    {
        //Find the instance with matching id and read the navigation properties.
        var query1 = _dbContext.Logins
            .Where(i => i.LoginId == itemDto.LoginId);
        var item = await query1
            .Include(i => i.AnimalsDbM)
            .Include(i => i.EmployeesDbM)
            .FirstOrDefaultAsync<DbLogin>();

        //If the item does not exists
        if (item == null) throw new ArgumentException($"Item {itemDto.LoginId} is not existing");

        //transfer any changes from DTO to database objects
        //Update individual properties
        item.UpdateFromDTO(itemDto);

        //Update navigation properties
        await navProp_Itemdto_to_ItemDbM(itemDto, item);

        //write to database model
        _dbContext.Logins.Update(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        //return the updated item in non-flat mode
        return await ReadItemAsync(item.LoginId, false);    
    }

    public async Task<ResponseItemDTO<ILogin>> CreateItemAsync(LoginDTO itemDto)
    {
        if (itemDto.LoginId != null)
            throw new ArgumentException($"{nameof(itemDto.LoginId)} must be null when creating a new object");

        //transfer any changes from DTO to database objects
        //Update individual properties Zoo
        var item = new DbLogin(itemDto);

        //Update navigation properties
        await navProp_Itemdto_to_ItemDbM(itemDto, item);

        //write to database model
        _dbContext.Logins.Add(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();
        
        //return the updated item in non-flat mode
        return await ReadItemAsync(item.LoginId, false);
    }

    //from all Guid relationships in _itemDtoSrc finds the corresponding object in the database and assigns it to _itemDst 
    //as navigation properties. Error is thrown if no object is found corresponing to an id.
    private async Task navProp_Itemdto_to_ItemDbM(LoginDTO itemDtoSrc, DbLogin itemDst)
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
