using System;
namespace Models;

public interface IShoeBrand
{
    public Guid BrandID { get; set; }
    public string BrandName { get; set; }

}
