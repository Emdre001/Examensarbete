using System.Collections.Generic;

namespace Models.DTO;

public class ColorDTO
{
    public string ColorName { get; set; }
    public virtual List<Guid> ProductsId { get; set; } = null;

}
