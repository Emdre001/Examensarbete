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


}
