using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;

using Models;
using Models.DTO;
using DbModels;
using DbContext;

namespace DbRepos;

public class ProductDbRepos
{
    private readonly ILogger<ProductDbRepos> _logger;
    private readonly MainDbContext _dbContext;

    #region contructors
    public ProductDbRepos(ILogger<ProductDbRepos> logger, MainDbContext context)
    {
        _logger = logger;
        _dbContext = context;
    }
    #endregion

    public async Task<ResponseItemDTO<IProduct>> ReadItemAsync(Guid id, bool flat)
    {
        IQueryable<DbProduct> query;
        if (!flat)
        {
            query = _dbContext.Products.AsNoTracking()
                .Include(i => i.AnimalsDbM)
                .Include(i => i.EmployeesDbM)
                .Where(i => i.ProductId == id);
        }
        else
        {
            query = _dbContext.Products.AsNoTracking()
                .Where(i => i.ProductId == id);
        }   

        var resp =  await query.FirstOrDefaultAsync<IProduct>();
        return new ResponseItemDTO<IProduct>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = resp
        };
    }

    public async Task<ResponsePageDto<IProduct>> ReadItemsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize)
    {
        filter ??= "";
        IQueryable<DbProduct> query;
        if (flat)
        {
            query = _dbContext.Products.AsNoTracking();
        }
        else
        {
            query = _dbContext.Products.AsNoTracking()
                .Include(i => i.AnimalsDbM)
                .Include(i => i.EmployeesDbM);
        }

        return new ResponsePageDto<IProduct>()
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

            .ToListAsync<IProduct>(),

            PageNr = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<ResponseItemDTO<IProduct>> DeleteItemAsync(Guid id)
    {
        //Find the instance with matching id
        var query1 = _dbContext.Products
            .Where(i => i.ProductId == id);
        var item = await query1.FirstOrDefaultAsync<DbProduct>();

        //If the item does not exists
        if (item == null) throw new ArgumentException($"Item {id} is not existing");

        //delete in the database model
        _dbContext.Products.Remove(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        return new ResponseItemDTO<IProduct>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = item
        };
    }

    public async Task<ResponseItemDTO<IProduct>> UpdateItemAsync(ProductDTO itemDTO)
    {
        //Find the instance with matching id and read the navigation properties.
        var query1 = _dbContext.Products
            .Where(i => i.ProductId == itemDTO.ProductId);
        var item = await query1
            .Include(i => i.AnimalsDbM)
            .Include(i => i.EmployeesDbM)
            .FirstOrDefaultAsync<DbProduct>();

        //If the item does not exists
        if (item == null) throw new ArgumentException($"Item {itemDTO.ProductId} is not existing");

        //transfer any changes from DTO to database objects
        //Update individual properties
        item.UpdateFromDTO(itemDTO);

        //Update navigation properties
        await navProp_ItemdTO_to_ItemDbM(itemDTO, item);

        //write to database model
        _dbContext.Products.Update(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        //return the updated item in non-flat mode
        return await ReadItemAsync(item.ProductId, false);    
    }

    public async Task<ResponseItemDTO<IProduct>> CreateItemAsync(ProductDTO itemDTO)
    {
        if (itemDTO.ProductId != null)
            throw new ArgumentException($"{nameof(itemDTO.ProductId)} must be null when creating a new object");

        //transfer any changes from DTO to database objects
        //Update individual properties Zoo
        var item = new DbProduct(itemDTO);

        //Update navigation properties
        await navProp_ItemdTO_to_ItemDbM(itemDTO, item);

        //write to database model
        _dbContext.Products.Add(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();
        
        //return the updated item in non-flat mode
        return await ReadItemAsync(item.ProductId, false);
    }
    /*

    //from all Guid relationships in _itemDTOSrc finds the corresponding object in the database and assigns it to _itemDst 
    //as navigation properties. Error is thrown if no object is found corresponing to an id.
    private async Task navProp_ItemdTO_to_ItemDbM(ProductDTO itemDTOSrc, DbProduct itemDst)
    {
        //update AnimalsDbM from list
        List<AnimalDbM> Animals = null;
        if (itemDTOSrc.AnimalsId != null)
        {
            Animals = new List<AnimalDbM>();
            foreach (var id in itemDTOSrc.AnimalsId)
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
        if (itemDTOSrc.EmployeesId != null)
        {
            Employees = new List<EmployeeDbM>();
            foreach (var id in itemDTOSrc.EmployeesId)
            {
                var p = await _dbContext.Employees.FirstOrDefaultAsync(i => i.EmployeeId == id);
                if (p == null)
                    throw new ArgumentException($"Item id {id} not existing");

                Employees.Add(p);
            }
        }
        itemDst.EmployeesDbM = Employees;
    }*/
}
