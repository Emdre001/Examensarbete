using System.Collections.Generic;
using System;


namespace Models;

public class Product
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductType { get; set; }
    public string ProductDescription { get; set; }
    public int ProductPrice { get; set; }
    public int ProductRating { get; set; }

    public Guid BrandId { get; set; }
    public Brand Brand { get; set; }
    public List<Color> Colors { get; set; }
    public List<ProductSize> ProductSizes { get; set; }
    public List<Order> Orders { get; set; }
    
}