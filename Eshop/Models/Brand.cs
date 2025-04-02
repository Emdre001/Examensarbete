using System.Collections.Generic;

namespace Models;

public class Brand : IBrand
{
    public Guid BrandID { get; set; }
    public string BrandName { get; set; }
}
