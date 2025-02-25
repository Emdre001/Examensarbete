using System;
using System.Collections.Generic;


namespace Models;

public interface IOrder
{
    public virtual Guid OrderID { get; set; }
    public virtual string OrderDetails { get; set; }
    public virtual DateAndTime OrderDate {get; set; }
    public virtual string OrderStatus { get; set; }
    public virtual int OrderAmount { get; set; }
}