using System.Collections.Generic;
using System;


namespace Models.DTO;

public class ProductDTO
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductType { get; set; }
    public string ProductDescription { get; set; }
    public int ProductStock { get; set; }
    public int ProductPrice { get; set; }
    public int ProductRating { get; set; }
}