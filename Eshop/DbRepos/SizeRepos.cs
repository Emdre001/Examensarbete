using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Models;
using Models.DTO;
using DbModels;
using DbContext;

namespace DbRepos;

public class SizeDbRepos
{
    private readonly ILogger<SizeDbRepos> _logger;
    private readonly MainDbContext _dbContext;
    public SizeDbRepos(ILogger<SizeDbRepos> logger, MainDbContext context)
    {
        _logger = logger;
        _dbContext = context;
    }
}
