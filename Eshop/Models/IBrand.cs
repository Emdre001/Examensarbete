using System;
using System.Collections.Generic;



namespace Models;

public interface IBrand
{
    public Guid BrandID { get; set; }
    public string BrandName { get; set; }

}
