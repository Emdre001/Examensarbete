namespace Models.DTO;

public class OrderDTO
{
    public virtual string OrderDetails { get; set; }
    public virtual DateTime OrderDate {get; set; }
    public virtual string OrderStatus { get; set; }
    public virtual int OrderAmount { get; set; }

    public virtual List<Guid> ProductIds { get; set; } = null;
    public virtual Guid? UserId { get; set; } = null;
}