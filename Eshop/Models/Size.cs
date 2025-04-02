using System.Collections.Generic;

namespace Models;

public class Size : ISize
{
    public Guid SizeId { get; set; }
    public int MenSize { get; set; }
    public int WomenSize { get; set; }
    public int ChildrenSize { get; set; }
    public virtual List<IProduct> Products { get; set; }
}
