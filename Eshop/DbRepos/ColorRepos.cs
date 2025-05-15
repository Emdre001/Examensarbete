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
 public async Task<Color> CreateColorAsync(ColorDTO colorDto)
    {
        var color = new Color
        {
            ColorId = Guid.NewGuid(),
            ColorName = colorDto.ColorName,
            
        };

        _dbContext.Colors.Add(color);
        await _dbContext.SaveChangesAsync();
        return color;
    }

    public async Task<Color> GetColorByIdAsync(Guid colorId)
    {
        return await _dbContext.Colors.FirstOrDefaultAsync(s => s.ColorId == colorId);
    }

    public async Task<List<Color>> GetAllColorsAsync()
    {
        return await _dbContext.Colors.ToListAsync();
    }

    public async Task<Color> UpdateColorAsync(Guid colorId, ColorDTO colorDto)
    {
        var color = await _dbContext.Colors.FindAsync(colorId);
        if (color == null)
        {
            return null;
        }

        color.ColorName = colorDto.ColorName;
        _dbContext.Colors.Update(color);
        await _dbContext.SaveChangesAsync();
        return color;
    }

    public async Task<bool> DeleteColorAsync(Guid colorId)
    {
        var color = await _dbContext.Colors.FindAsync(colorId);
        if (color == null)
        {
            return false;
        }

        _dbContext.Colors.Remove(color);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}