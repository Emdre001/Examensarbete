using System;
using System.Collections.Generic;


namespace Models;

public interface IOrder
{
    public Guid OrderId { get; set; }
    public string OrderDetails { get; set; }
    public DateTime OrderDate {get; set; }
    public string OrderStatus { get; set; }
    public int OrderAmount { get; set; }
    public  List<IProduct> Products { get; set; }
    public  IUser User { get; set; };
}