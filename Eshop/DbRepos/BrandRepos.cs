using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Models;
using Models.DTO;
using DbContext;

namespace DbRepos;

public class BrandDbRepos
{
    private readonly ILogger<BrandDbRepos> _logger;
    private readonly MainDbContext _dbContext;
    public BrandDbRepos(ILogger<BrandDbRepos> logger, MainDbContext context)
    {
        _logger = logger;
        _dbContext = context;
    }

}