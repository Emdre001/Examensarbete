using System.Collections.Generic;

namespace Models;

public class Brand 
{
    public virtual Guid BrandId { get; set; }
    public virtual string BrandName { get; set; }

    public virtual List<Product> Products { get; set; }
    
}
