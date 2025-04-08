using System.Collections.Generic;

namespace Models;

public class Size : ISize
{
    public virtual Guid SizeId { get; set; }
    public virtual int MenSize { get; set; }
    public virtual int WomenSize { get; set; }
    public virtual int ChildrenSize { get; set; }
    public virtual List<IProduct> Products { get; set; }
}
