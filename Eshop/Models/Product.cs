using System.Collections.Generic;
using System;


namespace Models;

public class Product : IProduct
{
    public virtual Guid ProductId { get; set; }
    public virtual string ProductName { get; set; }
    public virtual string ProductType { get; set; }
    public virtual string ProductDescription { get; set; }
    public virtual int ProductStock { get; set; }
    public virtual int ProductPrice { get; set; }
    public virtual int ProductRating { get; set; }

    public virtual List<IBrand> Brands { get; set; }
    public virtual List<IColor> Colors { get; set; }
    public virtual List<ISize> Sizes { get; set; }
    public virtual List<IOrder> Orders { get; set; }
    
}