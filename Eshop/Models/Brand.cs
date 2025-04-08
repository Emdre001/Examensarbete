using System.Collections.Generic;

namespace Models;

public class Brand : IBrand
{
    public virtual Guid BrandId { get; set; }
    public virtual string BrandName { get; set; }

    public virtual IProduct Product { get; set; }
    
}
