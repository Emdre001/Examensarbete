using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Models;
using Models.DTO;
using DbContext;

namespace DbRepos;

public class ColorDbRepos
{
    private readonly ILogger<ColorDbRepos> _logger;
    private readonly MainDbContext _dbContext;
    public ColorDbRepos(ILogger<ColorDbRepos> logger, MainDbContext context)
    {
        _logger = logger;
        _dbContext = context;
    }

}