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
            .Include(p => p.ProductSizes)
            .ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(Guid id)
    {
        return await _dbContext.Products
            .Include(p => p.Brand)
            .Include(p => p.Colors)
            .Include(p => p.ProductSizes)
            .FirstOrDefaultAsync(p => p.ProductId == id);
    }

    public async Task<Product> CreateProductAsync(ProductDTO dto)
    {
        var brand = await _dbContext.Brands.FindAsync(dto.BrandId);

        var colors = await _dbContext.Colors
            .Where(c => dto.ColorsId.Contains(c.ColorId))
            .ToListAsync();

        // Assuming ProductSize is an entity that links Product and Size
        // You will need to fetch Sizes by dto.SizesId, then create ProductSize objects.
        var sizes = await _dbContext.Sizes
            .Where(s => dto.SizesId.Contains(s.SizeId))
            .ToListAsync();

        // Create ProductSize objects linking to Sizes
        var productSizes = sizes.Select(size => new ProductSize
        {
            Size = size,
            SizeId = size.SizeId
            // If ProductId is required here, assign after product creation or use navigation property
        }).ToList();

        var product = new Product
        {
            ProductId = Guid.NewGuid(),
            ProductName = dto.ProductName,
            ProductType = dto.ProductType,
            ProductDescription = dto.ProductDescription,
            ProductPrice = dto.ProductPrice,
            ProductRating = dto.ProductRating,
            ProductGender = dto.ProductGender,
            
            Brand = brand,
            BrandId = brand?.BrandId ?? Guid.Empty, // Optional but good for clarity
            Colors = colors,
            ProductSizes = productSizes,
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
            .Include(p => p.ProductSizes)
            .FirstOrDefaultAsync(p => p.ProductId == id);

        if (product == null) return null;

        // Update scalar properties
        product.ProductName = dto.ProductName;
        product.ProductType = dto.ProductType;
        product.ProductDescription = dto.ProductDescription;
        product.ProductPrice = dto.ProductPrice;
        product.ProductRating = dto.ProductRating;
        product.ProductGender = dto.ProductGender;

        // Update brand
        product.Brand = await _dbContext.Brands.FindAsync(dto.BrandId);

        // Update colors
        var colors = await _dbContext.Colors
            .Where(c => dto.ColorsId.Contains(c.ColorId))
            .ToListAsync();
        product.Colors.Clear();
        foreach (var color in colors)
        {
            product.Colors.Add(color);
        }

        // Update product sizes
        // Remove sizes not in the DTO
        var dtoSizeIds = dto.ProductSizes.Select(ps => ps.SizeId).ToHashSet();
        product.ProductSizes.RemoveAll(ps => !dtoSizeIds.Contains(ps.SizeId));

        // Update existing sizes and add new ones
        foreach (var psDto in dto.ProductSizes)
        {
            var existing = product.ProductSizes.FirstOrDefault(ps => ps.SizeId == psDto.SizeId);
            if (existing != null)
            {
                existing.Stock = psDto.Stock; // Update stock
            }
            else
            {
                product.ProductSizes.Add(new ProductSize
                {
                    ProductId = product.ProductId,
                    SizeId = psDto.SizeId,
                    Stock = psDto.Stock
                });
            }
        }

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