using System;
using System.Collections.Generic;


namespace Models;

public interface ISize

{
    public Guid SizeId { get; set; }
    public int MenSize { get; set; }
    public int WomenSize { get; set; }
    public int ChildrenSize { get; set; }
    public  List<IProduct> Products { get; set; }

}

