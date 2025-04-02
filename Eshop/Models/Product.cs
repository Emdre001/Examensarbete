using System.Collections.Generic;
using System;


namespace Models;

public class Product : IProduct
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductType { get; set; }
    public string ProductDescription { get; set; }
    public int ProductStock { get; set; }
    public int ProductPrice { get; set; }
    public int ProductRating { get; set; }

    public virtual List<IBrand> Brands { get; set; }
    public virtual List<IColor> Colors { get; set; }
    public virtual List<ISize> Sizes { get; set; }
    public virtual List<IOrder> Orders { get; set; }
    
}