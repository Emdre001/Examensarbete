using System.Collections.Generic;

namespace Models.DTO;

public class ShoeSizeDTO
{
    public Guid SizeID { get; set; }
    public int MenSize { get; set; }
    public int WomenSize { get; set; }
    public int ChildrenSize { get; set; }

}
