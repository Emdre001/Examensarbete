using System.Collections.Generic;

namespace Models.DTO;

public class BrandDTO
{
    public string BrandName { get; set; }
    public virtual List<Guid> ProductsId { get; set; } = null;
  
}
