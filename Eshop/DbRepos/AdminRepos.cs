using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.Data.SqlClient;
using Seido.Utilities.SeedGenerator;
using Configuration;
using Models;
using Models.DTO;
using DbModels;
using DbContext;


namespace DbRepos;

public class AdminDbRepos
{
    private const string _seedSource = "./app-seeds.json";
    private readonly ILogger<AdminDbRepos> _logger;
    private readonly MainDbContext _dbContext;

    #region contructors
    public AdminDbRepos(ILogger<AdminDbRepos> logger, MainDbContext context)
    {
        _logger = logger;
        _dbContext = context;
    }
    #endregion

    public async Task<ResponseItemDTO<GstUsrInfoAllDTO>> InfoAsync()
    {
        var info = new GstUsrInfoAllDTO();
        info.Db = await _dbContext.InfoDbView.FirstAsync();
        info.Products = await _dbContext.InfoProductsView.ToListAsync();
        info.Brands = await _dbContext.InfoBrandsView.ToListAsync();
        info.Colors = await _dbContext.InfoColorsView.ToListAsync();

        return new ResponseItemDTO<GstUsrInfoAllDTO>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = info
        };
    }

    public async Task<ResponseItemDTO<GstUsrInfoAllDTO>> SeedAsync(int nrOfItems)
    {
        //First of all make sure the database is cleared from all seeded data
        await RemoveSeedAsync(true);

        //Create a seeder
        var fn = Path.GetFullPath(_seedSource);
        var seeder = new SeedGenerator(fn);

        //Generate Zoos and persons to be employed
        var zoos = seeder.ItemsToList<ZooDbM>(nrOfItems);
        var persons = seeder.ItemsToList<EmployeeDbM>(seeder.Next(nrOfItems, 5*nrOfItems));

        //Assign Animals and Employees to all the Zoos
        foreach (var zoo in zoos)
        {
            zoo.AnimalsDbM = seeder.ItemsToList<AnimalDbM>(seeder.Next(5,51));

            //Employ between 2 and 8 persons from the list
            zoo.EmployeesDbM = seeder.UniqueIndexPickedFromList<EmployeeDbM>(seeder.Next(2, 9), persons);
        }

        //Note that all other tables are automatically set through ZooDbM Navigation properties
        _dbContext.Zoos.AddRange(zoos);

        await _dbContext.SaveChangesAsync();

        return await InfoAsync();
    }
    
    public async Task<ResponseItemDTO<GstUsrInfoAllDTO>> RemoveSeedAsync(bool seeded)
    {
            var parameters = new List<SqlParameter>();

            var retValue = new SqlParameter("retval", SqlDbType.Int) { Direction = ParameterDirection.Output };
            var seededArg = new SqlParameter("seeded", seeded);

            parameters.Add(retValue);
            parameters.Add(seededArg);

            //there is no FromSqlRawAsync to I make one here
            var _query = await Task.Run(() =>
                _dbContext.InfoDbView.FromSqlRaw($"EXEC @retval = supusr.spDeleteAll @seeded",
                    parameters.ToArray()).AsEnumerable());

            //Execute the query and get the sp result set.
            //Although, I am not using this result set, but it shows how to get it
            GstUsrInfoDbDto result_set = _query.FirstOrDefault();

            //Check the return code
            int retCode = (int)retValue.Value;
            if (retCode != 0) throw new Exception("supusr.spDeleteAll return code error");

            return await InfoAsync();
    }
}