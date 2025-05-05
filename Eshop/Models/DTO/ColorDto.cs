using System.Collections.Generic;

namespace Models.DTO;

public class ColorDTO
{
    public Guid ColorId { get; set; }
    public string ColorName { get; set; }

    public virtual List<Guid> ProductsId { get; set; } = null;

    public ColorDTO() {}
    public ColorDTO (Color org)
    {
        ColorId = org.ColorId;
        ColorName = org.ColorName;

        ProductsId = org.Products?.Select(p => p.ProductId).ToList();
    }
}
