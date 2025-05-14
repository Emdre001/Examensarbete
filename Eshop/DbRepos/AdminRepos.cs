using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Models;
using Models.DTO;
using DbModels;
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

    public void CreateTestData()
    {
        // Create brands
        var brand1 = new Brand
        {
            BrandId = Guid.NewGuid(),
            BrandName = "Nike",
            Products = new List<IProduct>()
        };

        var brand2 = new Brand
        {
            BrandId = Guid.NewGuid(),
            BrandName = "Adidas",
            Products = new List<IProduct>()
        };

        // Create colors
        var colorRed = new Color
        {
            ColorId = Guid.NewGuid(),
            ColorName = "Red",
            Products = new List<IProduct>()
        };

        var colorBlue = new Color
        {
            ColorId = Guid.NewGuid(),
            ColorName = "Blue",
            Products = new List<IProduct>()
        };

        var colorGreen = new Color
        {
            ColorId = Guid.NewGuid(),
            ColorName = "Green",
            Products = new List<IProduct>()
        };

        var colorWhite = new Color
        {
            ColorId = Guid.NewGuid(),
            ColorName = "White",
            Products = new List<IProduct>()
        };

        var colorBlack = new Color
        {
            ColorId = Guid.NewGuid(),
            ColorName = "Black",
            Products = new List<IProduct>()
        };

        var colorGrey = new Color
        {
            ColorId = Guid.NewGuid(),
            ColorName = "Grey",
            Products = new List<IProduct>()
        };

        // Create sizes
        var size36 = new Size
        {
            SizeId = Guid.NewGuid(),
            SizeValue = 36,
            SizeStock = 16,
            Products = new List<IProduct>()
        };

        var size37 = new Size
        {
            SizeId = Guid.NewGuid(),
            SizeValue = 37,
            SizeStock = 22,
            Products = new List<IProduct>()
        };

        var size38 = new Size
        {
            SizeId = Guid.NewGuid(),
            SizeValue = 38,
            SizeStock = 9,
            Products = new List<IProduct>()
        };

        var size39 = new Size
        {
            SizeId = Guid.NewGuid(),
            SizeValue = 39,
            SizeStock = 14,
            Products = new List<IProduct>()
        };

        var size40 = new Size
        {
            SizeId = Guid.NewGuid(),
            SizeValue = 40,
            SizeStock = 15,
            Products = new List<IProduct>()
        };

        var size41 = new Size
        {
            SizeId = Guid.NewGuid(),
            SizeValue = 41,
            SizeStock = 20,
            Products = new List<IProduct>()
        };

        var size42 = new Size
        {
            SizeId = Guid.NewGuid(),
            SizeValue = 42,
            SizeStock = 18,
            Products = new List<IProduct>()
        };

        var size43 = new Size
        {
            SizeId = Guid.NewGuid(),
            SizeValue = 43,
            SizeStock = 12,
            Products = new List<IProduct>()
        };

        var size44 = new Size
        {
            SizeId = Guid.NewGuid(),
            SizeValue = 44,
            SizeStock = 8,
            Products = new List<IProduct>()
        };

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
            Colors = new List<IColor> { colorWhite, colorBlack },
            Sizes = new List<ISize> { size38, size40 },
            Orders = new List<IOrder>() // Add mock orders if necessary
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
            Colors = new List<IColor> { colorBlue, colorRed },
            Sizes = new List<ISize> { size40, size42 },
            Orders = new List<IOrder>() // Add mock orders if necessary
        };

        // Add products to their respective brands
        brand1.Products.Add(product1);
        brand2.Products.Add(product2);

        // Add products to their respective colors and sizes
        colorWhite.Products.Add(product1);
        colorBlack.Products.Add(product1);
        colorBlue.Products.Add(product2);
        colorRed.Products.Add(product2);

        size38.Products.Add(product1);
        size40.Products.Add(product1);
        size40.Products.Add(product2);
        size42.Products.Add(product2);

        // In an actual application, save these objects to your database
        // e.g., dbContext.Brands.AddRange(new List<Brand> { brand1, brand2 });
        // e.g., dbContext.SaveChanges();
    }



}



