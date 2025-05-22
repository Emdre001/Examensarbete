using System.Collections.Generic;

namespace Models.DTO;

public class ProductSizeDTO
{
    public Guid ProductId { get; set; }
    public Guid SizeId { get; set; }
    public int Stock { get; set; }
    
}