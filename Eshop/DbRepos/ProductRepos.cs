using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Models;
using Models.DTO;
using DbContext;

namespace DbRepos;

public class ProductDbRepos
{
    private readonly ILogger<ProductDbRepos> _logger;
    private readonly MainDbContext _dbContext;
    public ProductDbRepos(ILogger<ProductDbRepos> logger, MainDbContext context)
    {
        _logger = logger;
        _dbContext = context;
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _dbContext.Products
            .Include(p => p.Brand)
            .Include(p => p.Colors)
            .Include(p => p.Sizes)
            .ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(Guid id)
    {
        return await _dbContext.Products
            .Include(p => p.Brand)
            .Include(p => p.Colors)
            .Include(p => p.Sizes)
            .FirstOrDefaultAsync(p => p.ProductId == id);
    }

    public async Task<Product> CreateProductAsync(ProductDTO dto)
    {
        var brand = await _dbContext.Brands.FindAsync(dto.BrandId);
        var colors = await _dbContext.Colors.Where(c => dto.ColorsId.Contains(c.ColorId)).ToListAsync();
        var sizes = await _dbContext.Sizes.Where(s => dto.SizesId.Contains(s.SizeId)).ToListAsync();

        var product = new Product
        {
            ProductId = Guid.NewGuid(),
            ProductName = dto.ProductName,
            ProductType = dto.ProductType,
            ProductDescription = dto.ProductDescription,
            ProductPrice = dto.ProductPrice,
            ProductRating = dto.ProductRating,
            Brand = brand,
            Colors = colors,
            Sizes = sizes,
            Orders = new List<Order>()
        };

        _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync();
        return product;
    }

    public async Task<Product?> UpdateProductAsync(Guid id, ProductDTO dto)
    {
        var product = await _dbContext.Products
            .Include(p => p.Colors)
            .Include(p => p.Sizes)
            .FirstOrDefaultAsync(p => p.ProductId == id);

        if (product == null) return null;

        product.ProductName = dto.ProductName;
        product.ProductType = dto.ProductType;
        product.ProductDescription = dto.ProductDescription;
        product.ProductPrice = dto.ProductPrice;
        product.ProductRating = dto.ProductRating;

        product.Brand = await _dbContext.Brands.FindAsync(dto.BrandId);
        product.Colors = await _dbContext.Colors.Where(c => dto.ColorsId.Contains(c.ColorId)).ToListAsync();
        product.Sizes = await _dbContext.Sizes.Where(s => dto.SizesId.Contains(s.SizeId)).ToListAsync();

        await _dbContext.SaveChangesAsync();
        return product;
    }

    public async Task<bool> DeleteProductAsync(Guid id)
    {
        var product = await _dbContext.Products.FindAsync(id);
        if (product == null) return false;

        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task DeleteAllProductsAsync()
    {
        _dbContext.Products.RemoveRange(_dbContext.Products);
        await _dbContext.SaveChangesAsync();
    }

}