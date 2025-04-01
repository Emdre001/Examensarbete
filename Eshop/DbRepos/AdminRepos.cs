using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.Data.SqlClient;
using Seido.Utilities.SeedGenerator;
using Models.DTO;
using DbModels;
using DbContext;
using Configuration;
using Models;
using System.Security;

using Models;
using System.Security;

namespace DbRepos;

public class AdminDbRepos
{
    private const string _seedSource = "./app-seeds.json";
    private readonly ILogger<AdminDbRepos> _logger;
    //private Encryptions _encryptions;
    //private readonly MainDbContext _dbContext;

    #region contructors
    public AdminDbRepos(ILogger<AdminDbRepos> logger/*, Encryptions encryptions, MainDbContext context*/)
    {
        _logger = logger;
        //_encryptions = encryptions;
        //_dbContext = context;
    }
    #endregion
}