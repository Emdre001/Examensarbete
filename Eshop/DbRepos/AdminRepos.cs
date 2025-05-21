using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Models;
using Models.DTO;
using DbContext;

namespace DbRepos;

public class AdminDbRepos
{
    private readonly ILogger<AdminDbRepos> _logger;
    private readonly MainDbContext _dbContext;
    public AdminDbRepos(ILogger<AdminDbRepos> logger, MainDbContext context)
    {
        _logger = logger;
        _dbContext = context;
    }

   public async Task CreateTestDataAsync()
    {
        // Create brands
        var brand1 = new Brand
        {
            BrandId = Guid.NewGuid(),
            BrandName = "Nike",
            Products = new List<Product>()
        };

        var brand2 = new Brand
        {
            BrandId = Guid.NewGuid(),
            BrandName = "Adidas",
            Products = new List<Product>()
        };

        // Create colors
        var colorRed = new Color { ColorId = Guid.NewGuid(), ColorName = "Red", Products = new List<Product>() };
        var colorBlue = new Color { ColorId = Guid.NewGuid(), ColorName = "Blue", Products = new List<Product>() };
        var colorGreen = new Color { ColorId = Guid.NewGuid(), ColorName = "Green", Products = new List<Product>() };
        var colorWhite = new Color { ColorId = Guid.NewGuid(), ColorName = "White", Products = new List<Product>() };
        var colorBlack = new Color { ColorId = Guid.NewGuid(), ColorName = "Black", Products = new List<Product>() };
        var colorGrey = new Color { ColorId = Guid.NewGuid(), ColorName = "Grey", Products = new List<Product>() };

        // Create sizes
        var sizes = new List<Size>
        {
            new() { SizeId = Guid.NewGuid(), SizeValue = 36, ProductSizes = new List<ProductSize>() },
            new() { SizeId = Guid.NewGuid(), SizeValue = 37, ProductSizes = new List<ProductSize>() },
            new() { SizeId = Guid.NewGuid(), SizeValue = 38, ProductSizes = new List<ProductSize>() },
            new() { SizeId = Guid.NewGuid(), SizeValue = 39, ProductSizes = new List<ProductSize>() },
            new() { SizeId = Guid.NewGuid(), SizeValue = 40, ProductSizes = new List<ProductSize>() },
            new() { SizeId = Guid.NewGuid(), SizeValue = 41, ProductSizes = new List<ProductSize>() },
            new() { SizeId = Guid.NewGuid(), SizeValue = 42, ProductSizes = new List<ProductSize>() },
            new() { SizeId = Guid.NewGuid(), SizeValue = 43, ProductSizes = new List<ProductSize>() },
            new() { SizeId = Guid.NewGuid(), SizeValue = 44, ProductSizes = new List<ProductSize>() }
        };

        var size38 = sizes.First(s => s.SizeValue == 38);
        var size40 = sizes.First(s => s.SizeValue == 40);
        var size42 = sizes.First(s => s.SizeValue == 42);

        // Create products
        var product1 = new Product
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Shoe A",
            ProductType = "Sneaker",
            ProductDescription = "Shoe A Desc",
            ProductPrice = 899,
            ProductRating = 4,
            Brand = brand1,
            Colors = new List<Color> { colorWhite, colorBlack },
            Orders = new List<Order>(),
            ProductSizes = new List<ProductSize>() // ✅ New
        };

        var product2 = new Product
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Shoe B",
            ProductType = "Sneaker",
            ProductDescription = "Shoe B Desc",
            ProductPrice = 649,
            ProductRating = 5,
            Brand = brand2,
            Colors = new List<Color> { colorBlue, colorRed },
            Orders = new List<Order>(),
            ProductSizes = new List<ProductSize>() // ✅ New
        };

        // Create ProductSize entries (Stock is defined per product/size combo)
        var productSizes = new List<ProductSize>
        {
            new() { Product = product1, Size = size38, Stock = 9 },
            new() { Product = product1, Size = size40, Stock = 14 },
            new() { Product = product2, Size = size40, Stock = 15 },
            new() { Product = product2, Size = size42, Stock = 20 }
        };

        // Assign to ProductSizes collections
        product1.ProductSizes.AddRange(productSizes.Where(ps => ps.Product == product1));
        product2.ProductSizes.AddRange(productSizes.Where(ps => ps.Product == product2));

        // Reverse links (if needed for navigation)
        brand1.Products.Add(product1);
        brand2.Products.Add(product2);

        colorWhite.Products.Add(product1);
        colorBlack.Products.Add(product1);
        colorBlue.Products.Add(product2);
        colorRed.Products.Add(product2);

        // Add to DB
        _dbContext.Brands.AddRange(brand1, brand2);
        _dbContext.Colors.AddRange(colorRed, colorBlue, colorGreen, colorWhite, colorBlack, colorGrey);
        _dbContext.Sizes.AddRange(sizes);
        _dbContext.Products.AddRange(product1, product2);
        _dbContext.AddRange(productSizes); // ✅ Add join entities with stock

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAllDataAsync()
    {
        try
        {
            // Radera i omvänd ordning av relationshierarkin för att undvika FK-problem
            _dbContext.Orders.RemoveRange(_dbContext.Orders);
            _dbContext.Products.RemoveRange(_dbContext.Products);
            _dbContext.Brands.RemoveRange(_dbContext.Brands);
            _dbContext.Colors.RemoveRange(_dbContext.Colors);
            _dbContext.Sizes.RemoveRange(_dbContext.Sizes);

            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting all data");
            throw;
        }
    }

}



