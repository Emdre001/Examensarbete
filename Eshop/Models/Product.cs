using System.Collections.Generic;
using System;


namespace Models;

public class Product 
{
    public virtual Guid ProductId { get; set; }
    public virtual string ProductName { get; set; }
    public virtual string ProductType { get; set; }
    public virtual string ProductDescription { get; set; }
    public virtual int ProductPrice { get; set; }
    public virtual int ProductRating { get; set; }
    public Guid BrandId { get; set; }
    public virtual Brand Brand { get; set; }

     // Many-to-many relation to Color
        public virtual List<ProductColor> ProductColors { get; set; }  // Join table for Many-to-Many relation with Color

        // Many-to-many relation to Size
        public virtual List<ProductSize> ProductSizes { get; set; }  // Join table for Many-to-Many relation with Size

        // Many-to-many relation to Order
        public virtual List<ProductOrder> ProductOrders { get; set; }  // Join table for Many-to-Many relation with Order
    }

    // Join tables for Many-to-Many relationships
    public class ProductColor
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid ColorId { get; set; }
        public Color Color { get; set; }
    }

    public class ProductSize
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid SizeId { get; set; }
        public Size Size { get; set; }
    }

    public class ProductOrder
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid OrderId { get; set; }
        public Order Order { get; set; }
    }
