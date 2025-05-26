using System.Collections.Generic;

namespace Models;

public class Size
{
    public virtual Guid SizeId { get; set; }
    public virtual int SizeValue { get; set; }
    public virtual List<ProductSize> ProductSizes { get; set; }
}
