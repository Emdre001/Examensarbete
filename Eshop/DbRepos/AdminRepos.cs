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
        var brandNewBalance = new Brand
        {
            BrandId = Guid.NewGuid(),
            BrandName = "New Balance",
            Products = new List<Product>()
        };
        var brandAxelArigato = new Brand
        {
            BrandId = Guid.NewGuid(),
            BrandName = "Axel Arigato",
            Products = new List<Product>()
        };
        var brandUgg = new Brand
        {
            BrandId = Guid.NewGuid(),
            BrandName = "Ugg",
            Products = new List<Product>()
        };
        var brandDior = new Brand
        {
            BrandId = Guid.NewGuid(),
            BrandName = "Dior",
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
        var colorBeige = new Color { ColorId = Guid.NewGuid(), ColorName = "Beige", Products = new List<Product>() };

        // Create sizes
        var sizes = new List<Size>
        {
            new() { SizeId = Guid.NewGuid(), SizeValue = 35, ProductSizes = new List<ProductSize>() },
            new() { SizeId = Guid.NewGuid(), SizeValue = 36, ProductSizes = new List<ProductSize>() },
            new() { SizeId = Guid.NewGuid(), SizeValue = 37, ProductSizes = new List<ProductSize>() },
            new() { SizeId = Guid.NewGuid(), SizeValue = 38, ProductSizes = new List<ProductSize>() },
            new() { SizeId = Guid.NewGuid(), SizeValue = 39, ProductSizes = new List<ProductSize>() },
            new() { SizeId = Guid.NewGuid(), SizeValue = 40, ProductSizes = new List<ProductSize>() },
            new() { SizeId = Guid.NewGuid(), SizeValue = 41, ProductSizes = new List<ProductSize>() },
            new() { SizeId = Guid.NewGuid(), SizeValue = 42, ProductSizes = new List<ProductSize>() },
            new() { SizeId = Guid.NewGuid(), SizeValue = 43, ProductSizes = new List<ProductSize>() },
            new() { SizeId = Guid.NewGuid(), SizeValue = 44, ProductSizes = new List<ProductSize>() },
            new() { SizeId = Guid.NewGuid(), SizeValue = 45, ProductSizes = new List<ProductSize>() }
        };
        var size35 = sizes.First(s => s.SizeValue == 35);
        var size36 = sizes.First(s => s.SizeValue == 36);
        var size37 = sizes.First(s => s.SizeValue == 37);
        var size38 = sizes.First(s => s.SizeValue == 38);
        var size39 = sizes.First(s => s.SizeValue == 39);
        var size40 = sizes.First(s => s.SizeValue == 40);
        var size41 = sizes.First(s => s.SizeValue == 41);
        var size42 = sizes.First(s => s.SizeValue == 42);
        var size43 = sizes.First(s => s.SizeValue == 43);
        var size44 = sizes.First(s => s.SizeValue == 44);
        var size45 = sizes.First(s => s.SizeValue == 45);

        // Create products
        var product1 = new Product
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Nike Air Max DN Women",
            ProductType = "Sneaker",
            ProductDescription = "Nike Air Max DN Womens shoe combine comfort and style with a sleek design and responsive cushioning. Perfect for everyday wear.",
            ProductPrice = 1499,
            ProductRating = 4,
            ProductGender = "Women",
            Brand = brandNike,
            Colors = new List<Color> { colorWhite, colorBlack, colorBlue },
            Orders = new List<Order>(),
            ProductSizes = new List<ProductSize>()
            {
                
            }
        };

        var product2 = new Product
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Nike Air Force 1 '07",
            ProductType = "Sneaker",
            ProductDescription = "Nike Air Force 1 '07 Man shoe combine comfort and style with a sleek design and responsive cushioning. Perfect for everyday wear.",
            ProductPrice = 1499,
            ProductRating = 2,
            ProductGender = "Men",
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
            ProductDescription = "Nike Air Max Plus Man shoe combine comfort and style with a sleek design and responsive cushioning. Perfect for everyday wear.",
            ProductPrice = 2399,
            ProductRating = 3,
            ProductGender = "Men",
            Brand = brandNike,
            Colors = new List<Color> { colorBlack, colorBrown },
            Orders = new List<Order>(),
            ProductSizes = new List<ProductSize>() 
        };
        var product4 = new Product
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Axel arigato clean 90",
            ProductType = "Sneaker",
            ProductDescription = "Axel arigato clean 90 elegant shoe combine comfort and style with a sleek design and responsive cushioning. Perfect for everyday wear.",
            ProductPrice = 2565,
            ProductRating = 5,
            ProductGender = "Unisex",
            Brand = brandAxelArigato,
            Colors = new List<Color> { colorBlack, colorBlue, colorGreen },
            Orders = new List<Order>(),
            ProductSizes = new List<ProductSize>() 
        };
        var product5 = new Product
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Axel arigato Area Lo",
            ProductType = "Sneaker",
            ProductDescription = "Axel arigato Area Lo shoe combine comfort and style with a cool design and responsive cushioning. Perfect for everyday wear.",
            ProductPrice = 3723,
            ProductRating = 4,
            ProductGender = "Unisex",
            Brand = brandAxelArigato,
            Colors = new List<Color> { colorBlack, colorBlue, colorWhite },
            Orders = new List<Order>(),
            ProductSizes = new List<ProductSize>() 
        };
        var product6 = new Product
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Walk'n'Dior Platform Sneaker",
            ProductType = "Sneaker",
            ProductDescription = "Walk'n'Dior Platform Sneaker luxery shoe combine comfort and style with a cool design and responsive cushioning. Perfect for everyday wear.",
            ProductPrice = 10232,
            ProductRating = 5,
            ProductGender = "Women",
            Brand = brandDior,
            Colors = new List<Color> { colorBlack, colorGrey, colorWhite },
            Orders = new List<Order>(),
            ProductSizes = new List<ProductSize>() 
        };
        var product7 = new Product
        {
            ProductId = Guid.NewGuid(),
            ProductName = "New Balance 530",
            ProductType = "Sneaker",
            ProductDescription = "New Balance 530 shoe combine comfort and style with a sport design and responsive cushioning. Perfect for everyday wear.",
            ProductPrice = 1270,
            ProductRating = 5,
            ProductGender = "Women",
            Brand = brandNewBalance,
            Colors = new List<Color> { colorGrey, colorWhite, colorPink },
            Orders = new List<Order>(),
            ProductSizes = new List<ProductSize>() 
        };
        var product8 = new Product
        {
            ProductId = Guid.NewGuid(),
            ProductName = "New Balance 530 Beige",
            ProductType = "Sneaker",
            ProductDescription = "New Balance 530 Beige basic bih shoe combine comfort and style with a sport design and responsive cushioning. Perfect for everyday wear duuh.",
            ProductPrice = 1070,
            ProductRating = 3,
            ProductGender = "Women",
            Brand = brandNewBalance,
            Colors = new List<Color> { colorGrey, colorWhite, colorBeige },
            Orders = new List<Order>(),
            ProductSizes = new List<ProductSize>() 
        };
        var product9 = new Product
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Nike Baby pink",
            ProductType = "Sneaker",
            ProductDescription = "Nike Baby pink shoe combine comfort and style with a girliee design and responsive cushioning. Perfect for everyday slay.",
            ProductPrice = 799,
            ProductRating = 4,
            ProductGender = "Women",
            Brand = brandNike,
            Colors = new List<Color> { colorWhite, colorPink, colorBeige },
            Orders = new List<Order>(),
            ProductSizes = new List<ProductSize>() 
        };
        var product10 = new Product
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Nike Olive Green",
            ProductType = "Sneaker",
            ProductDescription = "Nike Olive Green shoe combine perfect comfort and style with a bad boy design and responsive cushioning. Perfect for everyday and you will become a real MAN.",
            ProductPrice = 1045,
            ProductRating = 3,
            ProductGender = "Men",
            Brand = brandNike,
            Colors = new List<Color> { colorRed, colorBlue, colorGreen },
            Orders = new List<Order>(),
            ProductSizes = new List<ProductSize>() 
        };
        var product11 = new Product
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Nike Dunks Low Panda",
            ProductType = "Sneaker",
            ProductDescription = "Nike Dunks Low Panda shoe combine perfect comfort and style with a premium design and responsive cushioning. Perfect for everyday and everyone who wants to be like a Panda.",
            ProductPrice = 1245,
            ProductRating = 4,
            ProductGender = "Unisex",
            Brand = brandNike,
            Colors = new List<Color> { colorBlack, colorWhite },
            Orders = new List<Order>(),
            ProductSizes = new List<ProductSize>() 
        };
        var product12 = new Product
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Ugg mini",
            ProductType = "Boots",
            ProductDescription = "Ugg mini shoe combine perfect comfort like walking on the clouds with a premium design and responsive cushioning. Perfect for everyday for all the girlies queen.",
            ProductPrice = 2745,
            ProductRating = 5,
            ProductGender = "Women",
            Brand = brandUgg,
            Colors = new List<Color> { colorBlack, colorBeige, colorBrown },
            Orders = new List<Order>(),
            ProductSizes = new List<ProductSize>() 
        };
        var product13 = new Product
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Ugg ultra mini",
            ProductType = "Boots",
            ProductDescription = "Ugg ultra mini shoe combine perfect comfort like walking on the clouds with a premium design and responsive cushioning. Perfect for everyday for all the girlies queen.",
            ProductPrice = 2859,
            ProductRating = 3,
            ProductGender = "Women",
            Brand = brandUgg,
            Colors = new List<Color> { colorBlack, colorBeige, colorBrown },
            Orders = new List<Order>(),
            ProductSizes = new List<ProductSize>() 
        };
        var product14 = new Product
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Adidas Campus",
            ProductType = "Sneaker",
            ProductDescription = "Adidas Campus shoe combine perfect comfort with a cool vibe design and responsive cushioning. Perfect for everyday in the sun.",
            ProductPrice = 1355,
            ProductRating = 3,
            ProductGender = "Unisex",
            Brand = brandAdidas,
            Colors = new List<Color> { colorBlack, colorBeige, colorGrey },
            Orders = new List<Order>(),
            ProductSizes = new List<ProductSize>() 
        };
        var product15 = new Product
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Nike Baby blue",
            ProductType = "Sneaker",
            ProductDescription = "Nike Baby blue shoe combine perfect comfort with a cool aesthetic vibe and responsive cushioning. Perfect for everyday in the sun, period.",
            ProductPrice = 1170,
            ProductRating = 4,
            ProductGender = "Unisex",
            Brand = brandNike,
            Colors = new List<Color> { colorBlue, colorGreen, colorRed },
            Orders = new List<Order>(),
            ProductSizes = new List<ProductSize>() 
        };
        var product16 = new Product
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Nike Jordans mid",
            ProductType = "Sneaker",
            ProductDescription = "Nike Jordans mid shoe combine perfect comfort with a cool vibe design and responsive cushioning. Perfect for everyday in a rush.",
            ProductPrice = 1499,
            ProductRating = 3,
            ProductGender = "Men",
            Brand = brandNike,
            Colors = new List<Color> { colorBlack, colorWhite, colorGrey },
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

            new() { Product = product4, Size = size37, Stock = 14 },
            new() { Product = product4, Size = size38, Stock = 6 },
            new() { Product = product4, Size = size39, Stock = 8 },
            new() { Product = product4, Size = size43, Stock = 16 },
            new() { Product = product4, Size = size44, Stock = 5 },

            new() { Product = product5, Size = size36, Stock = 7 },
            new() { Product = product5, Size = size37, Stock = 12 },
            new() { Product = product5, Size = size39, Stock = 16 },
            new() { Product = product5, Size = size41, Stock = 8 },
            new() { Product = product5, Size = size42, Stock = 9 },
            new() { Product = product5, Size = size43, Stock = 5 },

            new() { Product = product6, Size = size35, Stock = 6 },
            new() { Product = product6, Size = size36, Stock = 8 },
            new() { Product = product6, Size = size37, Stock = 15 },
            new() { Product = product6, Size = size38, Stock = 12 },
            new() { Product = product6, Size = size39, Stock = 7 },
            new() { Product = product6, Size = size40, Stock = 5 },

            new() { Product = product7, Size = size35, Stock = 5 },
            new() { Product = product7, Size = size36, Stock = 7 },
            new() { Product = product7, Size = size37, Stock = 6 },
            new() { Product = product7, Size = size38, Stock = 13 },

            new() { Product = product8, Size = size35, Stock = 7 },
            new() { Product = product8, Size = size39, Stock = 6 },
            new() { Product = product8, Size = size40, Stock = 9 },

            
            new() { Product = product9, Size = size35, Stock = 7 },
            new() { Product = product9, Size = size36, Stock = 11 },
            new() { Product = product9, Size = size38, Stock = 15 },

            new() { Product = product10, Size = size40, Stock = 7 },
            new() { Product = product10, Size = size41, Stock = 8 },
            new() { Product = product10, Size = size42, Stock = 12 },
            new() { Product = product10, Size = size43, Stock = 15 },

            new() { Product = product11, Size = size37, Stock = 14 },
            new() { Product = product11, Size = size38, Stock = 6 },
            new() { Product = product11, Size = size39, Stock = 8 },
            new() { Product = product11, Size = size43, Stock = 16 },
            new() { Product = product11, Size = size44, Stock = 5 },

            new() { Product = product12, Size = size35, Stock = 4 },
            new() { Product = product12, Size = size36, Stock = 6 },
            new() { Product = product12, Size = size37, Stock = 14 },
            new() { Product = product12, Size = size38, Stock = 12 },
            new() { Product = product12, Size = size39, Stock = 7 },
            new() { Product = product12, Size = size40, Stock = 5 },

            new() { Product = product13, Size = size35, Stock = 6 },
            new() { Product = product13, Size = size36, Stock = 9 },
            new() { Product = product13, Size = size38, Stock = 11 },

            new() { Product = product14, Size = size37, Stock = 14 },
            new() { Product = product14, Size = size38, Stock = 6 },
            new() { Product = product14, Size = size39, Stock = 8 },
            new() { Product = product14, Size = size43, Stock = 16 },
            new() { Product = product14, Size = size44, Stock = 5 },

            new() { Product = product15, Size = size35, Stock = 6 },
            new() { Product = product15, Size = size36, Stock = 8 },
            new() { Product = product15, Size = size37, Stock = 15 },
            new() { Product = product15, Size = size38, Stock = 12 },
            new() { Product = product15, Size = size39, Stock = 7 },
            new() { Product = product15, Size = size40, Stock = 5 },

            new() { Product = product16, Size = size42, Stock = 8 },
            new() { Product = product16, Size = size43, Stock = 12 },
            new() { Product = product16, Size = size44, Stock = 9 },
            new() { Product = product16, Size = size45, Stock = 6 },

        };

        var images = new List<ProductImage>
        {
            new ProductImage {ProductId = product1.ProductId, ImageUrl = "/img/AirMaxWomen1.png"},
            new ProductImage {ProductId = product1.ProductId, ImageUrl = "/img/AirMaxWomen2.png"},
            new ProductImage {ProductId = product1.ProductId, ImageUrl = "/img/AirMaxWomen3.png"},
            new ProductImage {ProductId = product1.ProductId, ImageUrl = "/img/AirMaxWomen4.png"},
            new ProductImage {ProductId = product1.ProductId, ImageUrl = "/img/AirMaxWomen5.png"},
            new ProductImage {ProductId = product1.ProductId, ImageUrl = "/img/AirMaxWomen6.png"},

            new ProductImage {ProductId = product2.ProductId, ImageUrl = "/img/AirForce.jpg"},
            new ProductImage {ProductId = product3.ProductId, ImageUrl = "/img/AirMaxPlus.webp"},
            new ProductImage {ProductId = product4.ProductId, ImageUrl = "/img/Arigattooo.jpg"},
            new ProductImage {ProductId = product5.ProductId, ImageUrl = "/img/AxelArigato.jpg"},
            new ProductImage {ProductId = product6.ProductId, ImageUrl = "/img/dior.jpg"},
            new ProductImage {ProductId = product7.ProductId, ImageUrl = "/img/NewBalanceBasic.jpg"},
            new ProductImage {ProductId = product8.ProductId, ImageUrl = "/img/NewBalanceBeige.jpg"},
            new ProductImage {ProductId = product9.ProductId, ImageUrl = "/img/NikebabyPink.jpg"},
            new ProductImage {ProductId = product10.ProductId, ImageUrl = "/img/NikeGreen.jpg"},
            new ProductImage {ProductId = product11.ProductId, ImageUrl = "/img/NikePanda.jpg"},
            new ProductImage {ProductId = product12.ProductId, ImageUrl = "/img/uggMiniSvart.jpg"},
            new ProductImage {ProductId = product13.ProductId, ImageUrl = "/img/UggsLow.jpg"},
            new ProductImage {ProductId = product14.ProductId, ImageUrl = "/img/AdidasCampus.jpg"},
            new ProductImage {ProductId = product15.ProductId, ImageUrl = "/img/NikeDunkBlue.jpg"},
            new ProductImage {ProductId = product16.ProductId, ImageUrl = "/img/AirJordan1.jpg"},

        };

        // Assign to ProductSizes collections
        product1.ProductSizes.AddRange(productSizes.Where(ps => ps.Product == product1));
        product2.ProductSizes.AddRange(productSizes.Where(ps => ps.Product == product2));
        product3.ProductSizes.AddRange(productSizes.Where(ps => ps.Product == product3));
        product4.ProductSizes.AddRange(productSizes.Where(ps => ps.Product == product4));
        product5.ProductSizes.AddRange(productSizes.Where(ps => ps.Product == product5));
        product6.ProductSizes.AddRange(productSizes.Where(ps => ps.Product == product6));
        product7.ProductSizes.AddRange(productSizes.Where(ps => ps.Product == product7));
        product8.ProductSizes.AddRange(productSizes.Where(ps => ps.Product == product8));
        product9.ProductSizes.AddRange(productSizes.Where(ps => ps.Product == product9));
        product10.ProductSizes.AddRange(productSizes.Where(ps => ps.Product == product10));
        product11.ProductSizes.AddRange(productSizes.Where(ps => ps.Product == product11));
        product12.ProductSizes.AddRange(productSizes.Where(ps => ps.Product == product12));
        product13.ProductSizes.AddRange(productSizes.Where(ps => ps.Product == product13));
        product14.ProductSizes.AddRange(productSizes.Where(ps => ps.Product == product14));
        product15.ProductSizes.AddRange(productSizes.Where(ps => ps.Product == product15));
        product16.ProductSizes.AddRange(productSizes.Where(ps => ps.Product == product16));

        // Reverse links (if needed for navigation)
        brandNike.Products.Add(product1);
        brandNike.Products.Add(product2);
        brandNike.Products.Add(product3);
        brandAxelArigato.Products.Add(product4);
        brandAxelArigato.Products.Add(product5);
        brandDior.Products.Add(product6);
        brandNewBalance.Products.Add(product7);
        brandNewBalance.Products.Add(product8);
        brandNike.Products.Add(product9);
        brandNike.Products.Add(product10);
        brandNike.Products.Add(product11);
        brandUgg.Products.Add(product12);
        brandUgg.Products.Add(product13);
        brandAdidas.Products.Add(product14);
        brandNike.Products.Add(product15);
        brandNike.Products.Add(product16);

        colorWhite.Products.Add(product1);
        colorBlack.Products.Add(product1);
        colorBlue.Products.Add(product1);

        colorWhite.Products.Add(product2);
        colorGrey.Products.Add(product2);

        colorBlack.Products.Add(product3);
        colorBrown.Products.Add(product3);

        colorBlack.Products.Add(product4);
        colorBlue.Products.Add(product4);
        colorGreen.Products.Add(product4);

        colorBlack.Products.Add(product5);
        colorBlue.Products.Add(product5);
        colorWhite.Products.Add(product5);

        colorBlack.Products.Add(product6);
        colorGrey.Products.Add(product6);
        colorWhite.Products.Add(product6);

        colorGrey.Products.Add(product7);
        colorWhite.Products.Add(product7);
        colorPink.Products.Add(product7);

        colorGrey.Products.Add(product8);
        colorWhite.Products.Add(product8);
        colorBeige.Products.Add(product8);
        
        colorWhite.Products.Add(product9);
        colorPink.Products.Add(product9);
        colorBeige.Products.Add(product9);

        colorRed.Products.Add(product10);
        colorGreen.Products.Add(product10);
        colorBlue.Products.Add(product10);

        colorBlack.Products.Add(product11);
        colorWhite.Products.Add(product11);

        colorBlack.Products.Add(product12);
        colorBeige.Products.Add(product12);
        colorBrown.Products.Add(product12);

        colorBlack.Products.Add(product13);
        colorBeige.Products.Add(product13);
        colorBrown.Products.Add(product13);

        colorBlack.Products.Add(product14);
        colorBeige.Products.Add(product14);
        colorGrey.Products.Add(product14);

        colorBlue.Products.Add(product15);
        colorGreen.Products.Add(product15);
        colorRed.Products.Add(product15);

        colorBlack.Products.Add(product16);
        colorWhite.Products.Add(product16);
        colorGrey.Products.Add(product16);

        // Add to DB
        _dbContext.Brands.AddRange(brandNike, brandAdidas);
        _dbContext.Colors.AddRange(colorRed, colorBlue, colorGreen, colorWhite, colorBlack, colorGrey, colorBrown, colorPink, colorBeige);
        _dbContext.Sizes.AddRange(sizes);
        _dbContext.Products.AddRange(product1, product2, product3, product4, product5, product6, product7, product8, product9, product10, product11, product12, product13, product14, product15, product16);
        _dbContext.AddRange(productSizes);
        _dbContext.ProductImages.AddRange(images);

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



