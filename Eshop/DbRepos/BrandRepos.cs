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

    public async Task<ResponseItemDTO<IBrand>> ReadItemAsync(Guid id, bool flat)
    {
        IQueryable<DbBrand> query;
        if (!flat)
        {
            query = _dbContext.Brands.AsNoTracking()
                .Include(i => i.AnimalsDbM)
                .Include(i => i.EmployeesDbM)
                .Where(i => i.BrandId == id);
        }
        else
        {
            query = _dbContext.Brands.AsNoTracking()
                .Where(i => i.BrandId == id);
        }   

        var resp =  await query.FirstOrDefaultAsync<IBrand>();
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
                .Include(i => i.AnimalsDbM)
                .Include(i => i.EmployeesDbM);
        }

        return new ResponsePageDTO<IBrand>()
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

            .ToListAsync<IBrand>(),

            PageNr = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<ResponseItemDTO<IBrand>> DeleteItemAsync(Guid id)
    {
        //Find the instance with matching id
        var query1 = _dbContext.Brands
            .Where(i => i.BrandId == id);
        var item = await query1.FirstOrDefaultAsync<DbBrand>();

        //If the item does not exists
        if (item == null) throw new ArgumentException($"Item {id} is not existing");

        //delete in the database model
        _dbContext.Brands.Remove(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        return new ResponseItemDTO<IBrand>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = item
        };
    }

    public async Task<ResponseItemDTO<IBrand>> UpdateItemAsync(BrandDTO itemDto)
    {
        //Find the instance with matching id and read the navigation properties.
        var query1 = _dbContext.Brands
            .Where(i => i.BrandId == itemDto.BrandId);
        var item = await query1
            .Include(i => i.AnimalsDbM)
            .Include(i => i.EmployeesDbM)
            .FirstOrDefaultAsync<DbBrand>();

        //If the item does not exists
        if (item == null) throw new ArgumentException($"Item {itemDto.BrandId} is not existing");

        //transfer any changes from DTO to database objects
        //Update individual properties
        item.UpdateFromDTO(itemDto);

        //Update navigation properties
        await navProp_Itemdto_to_ItemDbM(itemDto, item);

        //write to database model
        _dbContext.Brands.Update(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        //return the updated item in non-flat mode
        return await ReadItemAsync(item.BrandId, false);    
    }

    public async Task<ResponseItemDTO<IBrand>> CreateItemAsync(BrandDTO itemDto)
    {
        if (itemDto.BrandId != null)
            throw new ArgumentException($"{nameof(itemDto.BrandId)} must be null when creating a new object");

        //transfer any changes from DTO to database objects
        //Update individual properties Zoo
        var item = new DbBrand(itemDto);

        //Update navigation properties
        await navProp_Itemdto_to_ItemDbM(itemDto, item);

        //write to database model
        _dbContext.Brands.Add(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();
        
        //return the updated item in non-flat mode
        return await ReadItemAsync(item.BrandId, false);
    }

    //from all Guid relationships in _itemDtoSrc finds the corresponding object in the database and assigns it to _itemDst 
    //as navigation properties. Error is thrown if no object is found corresponing to an id.
    private async Task navProp_Itemdto_to_ItemDbM(BrandDTO itemDtoSrc, DbBrand itemDst)
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
