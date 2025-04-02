using System;
using System.Collections.Generic;


namespace Models.DTO;

public class OrderDTO
{
    public virtual Guid OrderId { get; set; }
    public virtual string OrderDetails { get; set; }
    public virtual DateTime OrderDate {get; set; }
    public virtual string OrderStatus { get; set; }
    public virtual int OrderAmount { get; set; }
}