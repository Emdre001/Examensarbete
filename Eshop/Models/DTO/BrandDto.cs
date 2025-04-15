using System.Collections.Generic;

namespace Models.DTO;

public class BrandDTO
{
    public Guid BrandId { get; set; }
    public string BrandName { get; set; }

    public virtual List<Guid> ProductsId { get; set; } = null;
  
    public BrandDTO() {}
    public BrandDTO (IBrand org)
    {
        BrandId = org.BrandId;
        BrandName = org.BrandName;

        ProductsId = org.Products?.Select(p => p.ProductId).ToList();
    }
}
