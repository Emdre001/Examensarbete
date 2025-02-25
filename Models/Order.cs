using System.Collections.Generic;


namespace Models;

public class Order
{
    public Guid OrderID { get; set; }
    public string OrderDetails { get; set; }
    public DateAndTime OrderDate {get; set; }
    public string OrderStatus { get; set; }
    public int OrderAmount { get; set; }
}