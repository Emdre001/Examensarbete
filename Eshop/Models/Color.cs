using System.Collections.Generic;

namespace Models;

public class Color
{
    public Guid ColorId { get; set; }
    public string ColorName { get; set; }
    public List<Product> Products { get; set; }
}
