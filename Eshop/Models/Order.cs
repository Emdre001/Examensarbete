using System;
using System.Collections.Generic;


namespace Models;

public class Order
{
    public virtual Guid OrderId { get; set; }
    public virtual string OrderDetails { get; set; }
    public virtual DateTime OrderDate { get; set; }
    public virtual string OrderStatus { get; set; }
    public virtual int OrderAmount { get; set; }

    public List<OrderProduct> OrderProducts { get; set; }
    public virtual User User { get; set; }
    public virtual Guid userId { get; set; }
}