using System.Collections.Generic;
using System;


namespace Models.DTO;

public class ProductDTO
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductType { get; set; }
    public string ProductDescription { get; set; }
    public int ProductPrice { get; set; }
    public int ProductRating { get; set; }

    public virtual Guid? BrandId { get; set; } = null;
    public virtual List<Guid> ColorsId { get; set; } = null;
    public virtual List<Guid> OrdersId { get; set; } = null;
    public virtual List<Guid> SizesId { get; set; } = null;
    public ProductDTO() {}
    public ProductDTO (IProduct org)
    {
        ProductId = org.ProductId;
        ProductName = org.ProductName;
        ProductType = org.ProductType;
        ProductDescription = org.ProductDescription;
        ProductPrice = org.ProductPrice;
        ProductRating = org.ProductRating;

        BrandId = org?.Brand?.BrandId;
        ColorsId = org.Colors?.Select(c => c.ColorId).ToList();
        OrdersId = org.Orders?.Select(o => o.OrderId).ToList();
        SizesId = org.Sizes?.Select(s => s.SizeId).ToList();
        
    }
}