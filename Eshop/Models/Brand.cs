using System.Collections.Generic;

namespace Models;

public class Brand
{
    public Guid BrandId { get; set; }
    public string BrandName { get; set; }

    public List<Product> Products { get; set; }
}
