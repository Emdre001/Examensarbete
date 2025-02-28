using System;
using Configuration;

namespace Models.DTO;

public class GstUsrInfoDbDto
{
    public string Title {get;  set;}
    public int SeededProducts { get; set; }
    
}

public class GstUsrInfoProductDto
{
    public string ProductName { get; set; }
    public string ProductType { get; set; }
    public string ProductDescription { get; set; }
    public int ProductStock { get; set; }
    public int ProductPrice { get; set; }
    public int ProductRating { get; set; }
    public int NrProducts { get; set; }
}
public class GstUsrInfoColorDto
{
    public string ColorName { get; set; }
    public int NrColors { get; set; }
}
public class GstUsrInfoOrderDto
{
    public virtual string OrderDetails { get; set; }
    public virtual DateTime OrderDate {get; set; }
    public virtual string OrderStatus { get; set; }
    public virtual int OrderAmount { get; set; }
    public int NrOrders { get; set; }
}
public class GstUsrInfoShoeBrandDto
{
    public string BrandName { get; set; }
    public int NrBrands { get; set; }
}
public class GstUsrInfoShoeSizeDto
{
    public int MenSize { get; set; }
    public int WomenSize { get; set; }
    public int ChildrenSize { get; set; }
    public int NrSizes { get; set; }
}
public class GstUsrInfoUserDto
{
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public string UserPassword { get; set; }
    public string UserAddress { get; set; }
    public int UserPhoneNr { get; set; }
    public string UserRole { get; set; }
    public int NrUsers { get; set; }
}
public class GstUsrInfoAllDto
{
    public GstUsrInfoDbDto Db { get; set; } = null;
    public List<GstUsrInfoZoosDto> Zoos { get; set; } = null;
}