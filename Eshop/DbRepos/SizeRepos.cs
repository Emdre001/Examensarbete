using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Models.DTO;
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

    public async Task<List<SizeDTO>> GetAllSizesAsync()
    {
        try
        {
            var sizes = await _dbContext.Sizes
                .Include(s => s.Products)
                .ToListAsync();

            return sizes.Select(s => new SizeDTO
            {
                SizeId = s.SizeId,
                SizeValue = s.SizeValue,
                SizeStock = s.SizeStock,
                ProductsId = s.Products?.Select(p => p.ProductId).ToList()
            }).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve all sizes");
            return new List<SizeDTO>();
        }
    }

    public async Task<SizeDTO?> GetSizeByIdAsync(Guid sizeId)
    {
        try
        {
            var size = await _dbContext.Sizes
                .Include(s => s.Products)
                .FirstOrDefaultAsync(s => s.SizeId == sizeId);

            return size == null ? null : new SizeDTO(size);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to get size with ID {sizeId}");
            return null;
        }
    }

    public async Task<bool> AddSizeAsync(SizeDTO dto)
    {
        try
        {
            var products = await _dbContext.Products
                .Where(p => dto.ProductsId != null && dto.ProductsId.Contains(p.ProductId))
                .ToListAsync();

            var newSize = new DbModels.DbSize
            {
                SizeId = dto.SizeId != Guid.Empty ? dto.SizeId : Guid.NewGuid(),
                SizeValue = dto.SizeValue,
                SizeStock = dto.SizeStock,
                Products = products.Cast<Product>().ToList()
            };

            _dbContext.Sizes.Add(newSize);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to add size");
            return false;
        }
    }

    public async Task<bool> UpdateSizeAsync(SizeDTO dto)
    {
        try
        {
            var size = await _dbContext.Sizes
                .Include(s => s.Products)
                .FirstOrDefaultAsync(s => s.SizeId == dto.SizeId);

            if (size == null) return false;

            size.SizeValue = dto.SizeValue;
            size.SizeStock = dto.SizeStock;

            if (dto.ProductsId != null)
            {
                var products = await _dbContext.Products
                    .Where(p => dto.ProductsId.Contains(p.ProductId))
                    .ToListAsync();

                size.Products = products.Cast<Product>().ToList();
            }

            _dbContext.Sizes.Update(size);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to update size with ID {dto.SizeId}");
            return false;
        }
    }

    public async Task<bool> DeleteSizeAsync(Guid sizeId)
    {
        try
        {
            var size = await _dbContext.Sizes.FindAsync(sizeId);
            if (size == null) return false;

            _dbContext.Sizes.Remove(size);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to delete size with ID {sizeId}");
            return false;
        }
    }
}
