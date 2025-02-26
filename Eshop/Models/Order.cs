using System;
using System.Collections.Generic;


namespace Models;

public class Order : IOrder
{
    public virtual Guid OrderID { get; set; }
    public virtual string OrderDetails { get; set; }
    public virtual DateTime OrderDate {get; set; }
    public virtual string OrderStatus { get; set; }
    public virtual int OrderAmount { get; set; }
}