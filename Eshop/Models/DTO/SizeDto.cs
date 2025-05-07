using System.Collections.Generic;

namespace Models.DTO;

public class SizeDTO
{
    public Guid SizeId{ get; set; }
    public int SizeValue { get; set; }
    public int SizeStock { get; set; }
    
    public virtual List<Guid> ProductsId { get; set; } = null;
    }
