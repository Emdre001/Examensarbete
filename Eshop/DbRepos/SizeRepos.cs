using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;
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

     public async Task<Size> CreateSizeAsync(SizeDTO sizeDto)
    {
        var size = new Size
        {
            SizeId = Guid.NewGuid(),
            SizeValue = sizeDto.SizeValue,
            SizeStock = sizeDto.SizeStock,
        };

        _dbContext.Sizes.Add(size);
        await _dbContext.SaveChangesAsync();
        return size;
    }

    public async Task<Size> GetSizeByIdAsync(Guid sizeId)
    {
        return await _dbContext.Sizes.FirstOrDefaultAsync(s => s.SizeId == sizeId);
    }

    public async Task<List<Size>> GetAllSizesAsync()
    {
        return await _dbContext.Sizes.ToListAsync();
    }

    public async Task<Size> UpdateSizeAsync(Guid sizeId, SizeDTO sizeDto)
    {
        var size = await _dbContext.Sizes.FindAsync(sizeId);
        if (size == null)
        {
            return null;
        }

        size.SizeValue = sizeDto.SizeValue;
        size.SizeStock = sizeDto.SizeStock;
        _dbContext.Sizes.Update(size);
        await _dbContext.SaveChangesAsync();
        return size;
    }

    public async Task<bool> DeleteSizeAsync(Guid sizeId)
    {
        var size = await _dbContext.Sizes.FindAsync(sizeId);
        if (size == null)
        {
            return false;
        }

        _dbContext.Sizes.Remove(size);
        await _dbContext.SaveChangesAsync();
        return true;
    }
    public async Task DeleteAllSizesAsync()
    {
        var allSizes = await _dbContext.Sizes.ToListAsync();
        if (allSizes.Any())
        {
            _dbContext.Sizes.RemoveRange(allSizes);
            await _dbContext.SaveChangesAsync();
        }
    }

}
