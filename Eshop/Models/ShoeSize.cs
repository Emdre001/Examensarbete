using System.Collections.Generic;

namespace Models;

public class ShoeSize
{
    public Guid SizeID { get; set; }
    public int MenSize { get; set; }
    public int WomenSize { get; set; }
    public int ChildrenSize { get; set; }
}
