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
        var brandNike = new Brand
        {
            BrandId = Guid.NewGuid(),
            BrandName = "Nike",
            Products = new List<Product>()
        };

        var brandAdidas = new Brand
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
        var colorBrown = new Color { ColorId = Guid.NewGuid(), ColorName = "Brown", Products = new List<Product>() };
        var colorPink = new Color { ColorId = Guid.NewGuid(), ColorName = "Pink", Products = new List<Product>() };

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
        var size36 = sizes.First(s => s.SizeValue == 36);
        var size37 = sizes.First(s => s.SizeValue == 37);
        var size38 = sizes.First(s => s.SizeValue == 38);
        var size39 = sizes.First(s => s.SizeValue == 39);
        var size40 = sizes.First(s => s.SizeValue == 40);
        var size41 = sizes.First(s => s.SizeValue == 41);
        var size42 = sizes.First(s => s.SizeValue == 42);
        var size43 = sizes.First(s => s.SizeValue == 43);
        var size44 = sizes.First(s => s.SizeValue == 44);

        // Create products
        var product1 = new Product
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Nike Air Max DN Women",
            ProductType = "Sneaker",
            ProductDescription = "Nike Air Max DN Womens shoes combine comfort and style with a sleek design and responsive cushioning. Perfect for everyday wear.",
            ProductPrice = 1499,
            ProductRating = 4,
            ProductGender = "Woman",
            Brand = brandNike,
            Colors = new List<Color> { colorWhite, colorBlack, colorBlue },
            Orders = new List<Order>(),
            ProductSizes = new List<ProductSize>() // ✅ New
        };

        var product2 = new Product
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Nike Air Force 1 '07",
            ProductType = "Sneaker",
            ProductDescription = "Nike Air Force 1 '07 Man shoes combine comfort and style with a sleek design and responsive cushioning. Perfect for everyday wear.",
            ProductPrice = 1499,
            ProductRating = 5,
            ProductGender = "Man",
            Brand = brandNike,
            Colors = new List<Color> { colorWhite, colorGrey },
            Orders = new List<Order>(),
            ProductSizes = new List<ProductSize>() 
        };

         var product3 = new Product
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Nike Air Max Plus",
            ProductType = "Sneaker",
            ProductDescription = "Nike Air Max Plus Man shoes combine comfort and style with a sleek design and responsive cushioning. Perfect for everyday wear.",
            ProductPrice = 2399,
            ProductRating = 5,
            ProductGender = "Man",
            Brand = brandNike,
            Colors = new List<Color> { colorBlack, colorBrown },
            Orders = new List<Order>(),
            ProductSizes = new List<ProductSize>() 
        };

        // Create ProductSize entries (Stock is defined per product/size combo)
        var productSizes = new List<ProductSize>
        {
            new() { Product = product1, Size = size36, Stock = 9 },
            new() { Product = product1, Size = size37, Stock = 12 },
            new() { Product = product1, Size = size38, Stock = 10 },
            new() { Product = product1, Size = size39, Stock = 9 },
            new() { Product = product1, Size = size40, Stock = 14 },

            new() { Product = product2, Size = size38, Stock = 15 },
            new() { Product = product2, Size = size39, Stock = 9 },
            new() { Product = product2, Size = size40, Stock = 9 },
            new() { Product = product2, Size = size41, Stock = 8 },
            new() { Product = product2, Size = size42, Stock = 20 },
            new() { Product = product2, Size = size43, Stock = 7 },
            new() { Product = product2, Size = size44, Stock = 17 },

            new() { Product = product3, Size = size40, Stock = 17 },
            new() { Product = product3, Size = size41, Stock = 15 },
            new() { Product = product3, Size = size42, Stock = 10 },
            new() { Product = product3, Size = size43, Stock = 14 },
            new() { Product = product3, Size = size44, Stock = 6 },

        };

        // Assign to ProductSizes collections
        product1.ProductSizes.AddRange(productSizes.Where(ps => ps.Product == product1));
        product2.ProductSizes.AddRange(productSizes.Where(ps => ps.Product == product2));
        product3.ProductSizes.AddRange(productSizes.Where(ps => ps.Product == product3));

        // Reverse links (if needed for navigation)
        brandNike.Products.Add(product1);
        brandNike.Products.Add(product2);
        brandNike.Products.Add(product3);

        colorWhite.Products.Add(product1);
        colorBlack.Products.Add(product1);
        colorBlue.Products.Add(product1);

        colorWhite.Products.Add(product2);
        colorGrey.Products.Add(product2);

        colorBlack.Products.Add(product3);
        colorBrown.Products.Add(product3);

        // Add to DB
        _dbContext.Brands.AddRange(brandNike, brandAdidas);
        _dbContext.Colors.AddRange(colorRed, colorBlue, colorGreen, colorWhite, colorBlack, colorGrey, colorBrown, colorPink);
        _dbContext.Sizes.AddRange(sizes);
        _dbContext.Products.AddRange(product1, product2, product3);
        _dbContext.AddRange(productSizes); 

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



