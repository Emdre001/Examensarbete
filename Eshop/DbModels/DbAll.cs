namespace DbModels; 
public class DbProductColor
{
    public Guid ProductId { get; set; }
    public DbProduct Product { get; set; }

    public Guid ColorId { get; set; }
    public DbColor Color { get; set; }
}

public class DbProductSize
{
    public Guid ProductId { get; set; }
    public DbProduct Product { get; set; }

    public Guid SizeId { get; set; }
    public DbSize Size { get; set; }
}

public class DbProductOrder
{
    public Guid ProductId { get; set; }
    public DbProduct Product { get; set; }

    public Guid OrderId { get; set; }
    public DbOrder Order { get; set; }
}
