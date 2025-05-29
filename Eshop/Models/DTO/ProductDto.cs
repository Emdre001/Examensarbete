using System.Collections.Generic;
using System;


namespace Models.DTO;

public class ProductDTO
{
    public string ProductName { get; set; }
    public string ProductType { get; set; }
    public string ProductDescription { get; set; }
    public int ProductPrice { get; set; }
    public int ProductRating { get; set; }
    public string ProductGender { get; set; }

    public virtual Guid? BrandId { get; set; } = null;
    public virtual List<Guid> ColorsId { get; set; } = null;
    public virtual List<Guid> OrdersId { get; set; } = null;
    public virtual List<Guid> SizesId { get; set; } = null;
    public virtual List<ProductSizeDTO> ProductSizes { get; set; } = null;


}