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

}
