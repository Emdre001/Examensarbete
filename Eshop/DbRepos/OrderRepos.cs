using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Models;
using Models.DTO;
using DbModels;
using DbContext;

namespace DbRepos;

public class OrderDbRepos
{
    private readonly ILogger<OrderDbRepos> _logger;
    private readonly MainDbContext _dbContext;
    public OrderDbRepos(ILogger<OrderDbRepos> logger, MainDbContext context)
    {
        _logger = logger;
        _dbContext = context;
    }

}