using System.Collections.Generic;

namespace Models.DTO;

public class SizeDTO
{
    public Guid SizeId{ get; set; }
    public int SizeValue { get; set; }
    public int SizeStock { get; set; }
    
    public virtual List<Guid> ProductsId { get; set; } = null;

    public SizeDTO() {}
    public SizeDTO (Size org)
    {
        SizeId = org.SizeId;
        SizeValue = org.SizeValue;
        SizeStock = org.SizeStock;

        ProductsId = org.Products?.Select(p => p.ProductId).ToList();
    }
}
