using System.Collections.Generic;

namespace Models;

public class Color 
{
    public virtual Guid ColorId { get; set; }
    public virtual string ColorName { get; set; }
    public virtual List<ProductColor> Products { get; set; }
}
