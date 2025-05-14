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

    public BrandDbRepos(ILogger<BrandDbRepos> logger, MainDbContext context)
    {
        _logger = logger;
        _dbContext = context;
    }

    public async Task<DbBrand> CreateBrandAsync(BrandDTO brandDto)
    {
        var brand = new DbBrand
        {
            BrandId = Guid.NewGuid(),
            BrandName = brandDto.BrandName
        };

        _dbContext.Brands.Add(brand);
        await _dbContext.SaveChangesAsync();
        return brand;
    }

    public async Task<DbBrand> GetBrandByIdAsync(Guid brandId)
    {
        return await _dbContext.Brands.FirstOrDefaultAsync(b => b.BrandId == brandId);
    }

    public async Task<List<DbBrand>> GetAllBrandsAsync()
    {
        return await _dbContext.Brands.ToListAsync();
    }

    public async Task<DbBrand> UpdateBrandAsync(Guid brandId, BrandDTO brandDto)
    {
        var brand = await _dbContext.Brands.FindAsync(brandId);
        if (brand == null)
        {
            return null;
        }

        brand.BrandName = brandDto.BrandName;
        _dbContext.Brands.Update(brand);
        await _dbContext.SaveChangesAsync();
        return brand;
    }

    public async Task<bool> DeleteBrandAsync(Guid brandId)
    {
        var brand = await _dbContext.Brands.FindAsync(brandId);
        if (brand == null)
        {
            return false;
        }

        _dbContext.Brands.Remove(brand);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}
