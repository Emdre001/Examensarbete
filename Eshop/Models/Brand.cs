using System.Collections.Generic;

namespace Models;

public class Brand : IBrand
{
    public virtual Guid BrandId { get; set; }
    public virtual string BrandName { get; set; }

    public virtual List<IProduct> Products { get; set; }
    
}
