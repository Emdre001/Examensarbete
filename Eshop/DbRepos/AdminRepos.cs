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
        new() { SizeId = Guid.NewGuid(), SizeValue = 36, SizeStock = 16, Products = new List<Product>() },
        new() { SizeId = Guid.NewGuid(), SizeValue = 37, SizeStock = 22, Products = new List<Product>() },
        new() { SizeId = Guid.NewGuid(), SizeValue = 38, SizeStock = 9, Products = new List<Product>() },
        new() { SizeId = Guid.NewGuid(), SizeValue = 39, SizeStock = 14, Products = new List<Product>() },
        new() { SizeId = Guid.NewGuid(), SizeValue = 40, SizeStock = 15, Products = new List<Product>() },
        new() { SizeId = Guid.NewGuid(), SizeValue = 41, SizeStock = 20, Products = new List<Product>() },
        new() { SizeId = Guid.NewGuid(), SizeValue = 42, SizeStock = 18, Products = new List<Product>() },
        new() { SizeId = Guid.NewGuid(), SizeValue = 43, SizeStock = 12, Products = new List<Product>() },
        new() { SizeId = Guid.NewGuid(), SizeValue = 44, SizeStock = 8, Products = new List<Product>() }
    };

    // Pick some sizes to assign
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
        Sizes = new List<Size> { size38, size40 },
        Orders = new List<Order>()
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
        Sizes = new List<Size> { size40, size42 },
        Orders = new List<Order>()
    };

    // Reverse links
    brand1.Products.Add(product1);
    brand2.Products.Add(product2);

    colorWhite.Products.Add(product1);
    colorBlack.Products.Add(product1);
    colorBlue.Products.Add(product2);
    colorRed.Products.Add(product2);

    size38.Products.Add(product1);
    size40.Products.Add(product1);
    size40.Products.Add(product2);
    size42.Products.Add(product2);

    // Save to database
    _dbContext.Brands.AddRange(brand1, brand2);
    _dbContext.Colors.AddRange(colorRed, colorBlue, colorGreen, colorWhite, colorBlack, colorGrey);
    _dbContext.Sizes.AddRange(sizes);
    _dbContext.Products.AddRange(product1, product2);

     await _dbContext.SaveChangesAsync();
}


}



