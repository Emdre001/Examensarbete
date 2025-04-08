using System.Collections.Generic;

namespace Models;

public class Color : IColor
{
    public virtual Guid ColorId { get; set; }
    public virtual string ColorName { get; set; }
    public virtual List<IProduct> Products { get; set; }
}
