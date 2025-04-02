using System;
using System.Collections.Generic;

namespace Models;

public interface IBrand
{
    public Guid BrandId { get; set; }
    public string BrandName { get; set; }
    public IProduct Product { get; set; }
}
