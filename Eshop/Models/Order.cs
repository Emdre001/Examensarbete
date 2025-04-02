using System;
using System.Collections.Generic;


namespace Models;

public class Order : IOrder
{
    public virtual Guid OrderId { get; set; }
    public virtual string OrderDetails { get; set; }
    public virtual DateTime OrderDate {get; set; }
    public virtual string OrderStatus { get; set; }
    public virtual int OrderAmount { get; set; }
    public virtual List<IProduct> Products { get; set; }
    public virtual IUser User { get; set; };
}