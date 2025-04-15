using System.Collections.Generic;

namespace Models.DTO;

public class SizeDTO
{
    public Guid SizeId{ get; set; }
    public int MenSize { get; set; }
    public int WomenSize { get; set; }
    public int ChildrenSize { get; set; }
    
    public virtual List<Guid> ProductsId { get; set; } = null;

    public SizeDTO() {}
    public SizeDTO (ISize org)
    {
        SizeId = org.SizeId;
        MenSize = org.MenSize;
        WomenSize = org.WomenSize;
        ChildrenSize = org.ChildrenSize;

        ProductsId = org.Products?.Select(p => p.ProductId).ToList();
    }
    }
