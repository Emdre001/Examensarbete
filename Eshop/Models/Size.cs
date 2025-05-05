using System.Collections.Generic;

namespace Models;

public class Size : ISize
{
    public virtual Guid SizeId { get; set; }
    public virtual int SizeValue { get; set; }
    public virtual int SizeStock { get; set; }
    public virtual List<IProduct> Products { get; set; }
}
