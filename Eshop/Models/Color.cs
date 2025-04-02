using System.Collections.Generic;

namespace Models;

public class Color : IColor
{
    public Guid ColorId { get; set; }
    public string ColorName { get; set; }
    public virtual List<IProduct> Products { get; set; }
}
