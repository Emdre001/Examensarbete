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

    public virtual List<Guid> ProductsId { get; set; } = null;
    public virtual Guid? UserId { get; set; } = null;

    public OrderDTO() {}
    public OrderDTO (Order org)
    {
        OrderId = org.OrderId;

        OrderDetails = org.OrderDetails;
        OrderDate = org.OrderDate;
        OrderStatus = org.OrderStatus;
        OrderAmount = org.OrderAmount;

        ProductsId = org.Products?.Select(p => p.ProductId).ToList();
        UserId = org?.User?.UserId;
    }
}