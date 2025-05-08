using System;
using System.Collections.Generic;


namespace Models;

public interface ISize

{
    public Guid SizeId { get; set; }
    public int SizeValue { get; set; }
    public int SizeStock { get; set; }
    public  List<IProduct> Products { get; set; }

}

