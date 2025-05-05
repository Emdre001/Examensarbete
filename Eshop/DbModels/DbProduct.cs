using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;
using Models.DTO;
using Newtonsoft.Json;

namespace DbModels;

    [Table("Products")]
    public class DbProduct : Product
    {
        [Key]
        public override Guid ProductId { get; set; }

        [NotMapped]
        public override IBrand Brand { get => DbBrand; set => throw new NotImplementedException(); }

        [JsonIgnore]
        [Required]
        public  DbBrand DbBrand { get; set; }

        [NotMapped]
        public override List<IColor> Colors { get => DbColors?.ToList<IColor>(); set => throw new NotImplementedException(); }

        [JsonIgnore]
        public List<DbColor> DbColors { get; set; }

        [NotMapped]
        public override List<ISize> Sizes { get => DbSizes?.ToList<ISize>(); set => throw new NotImplementedException(); }
        
        [JsonIgnore]
        public List<DbSize> DbSizes { get; set; }

        [NotMapped]
        public override List<IOrder> Orders { get => DbOrders?.ToList<IOrder>(); set => throw new NotImplementedException(); }

        [JsonIgnore]
        public List<DbOrder> DbOrders { get; set; }

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
