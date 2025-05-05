using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;
using Models.DTO;
using Newtonsoft.Json;
using System.Linq;

namespace DbModels;

[Table("Products")]
public class DbProduct : Product
{
    [Key]
    public override Guid ProductId { get; set; }

    [NotMapped]
    public override Brand Brand => DbBrand;

    [JsonIgnore]
    [Required]
    public DbBrand DbBrand { get; set; }

    // === ProductColor mapping ===
    [NotMapped]
    public override List<ProductColor> ProductColors =>
        DbProductColors?.Select(p => new ProductColor
        {
            ProductId = p.ProductId,
            ColorId = p.ColorId,
            Color = p.Color,
            Product = this
        }).ToList();

    [JsonIgnore]
    public List<DbProductColor> DbProductColors { get; set; }

    // === ProductSize mapping ===
    [NotMapped]
    public override List<ProductSize> ProductSizes =>
        DbProductSizes?.Select(p => new ProductSize
        {
            ProductId = p.ProductId,
            SizeId = p.SizeId,
            Size = p.Size,
            Product = this
        }).ToList();

    [JsonIgnore]
    public List<DbProductSize> DbProductSizes { get; set; }

    // === ProductOrder mapping ===
    [NotMapped]
    public override List<ProductOrder> ProductOrders =>
        DbProductOrders?.Select(p => new ProductOrder
        {
            ProductId = p.ProductId,
            OrderId = p.OrderId,
            Order = p.Order,
            Product = this
        }).ToList();

    [JsonIgnore]
    public List<DbProductOrder> DbProductOrders { get; set; }

    [Required]
    public string ProductName { get; set; }

    [Required]
    public string ProductType { get; set; }

    [Required]
    public string ProductDescription { get; set; }

    [Required]
    public int ProductPrice { get; set; }

    [Required]
    public int ProductRating { get; set; }

    public DbProduct UpdateFromDTO(ProductDTO org)
    {
        if (org == null) return null;

        ProductName = org.ProductName;
        ProductType = org.ProductType;
        ProductDescription = org.ProductDescription;
        ProductPrice = org.ProductPrice;
        ProductRating = org.ProductRating;

        return this;
    }

    public DbProduct() { }

    public DbProduct(ProductDTO org)
    {
        ProductId = Guid.NewGuid();
        UpdateFromDTO(org);
    }
}
