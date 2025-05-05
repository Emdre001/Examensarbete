using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;
using Models.DTO;
using Newtonsoft.Json;

namespace DbModels;

    [Table("Colors")]
    public class DbColor : Color
    {
        [Key]
        public override Guid ColorId { get; set; }

        [NotMapped]
        public override List<ProductColor> Products 
        { 
            get => DbProducts?.Select(dp => new ProductColor { ProductId = dp.ProductId, Product = dp}).ToList(); 
            set => throw new NotImplementedException(); 
        }

        [JsonIgnore]
        public List<DbProduct> DbProducts { get; set; }


        [Required]
        public string ColorName { get; set; }


    public DbColor UpdateFromDTO(ColorDTO org)
    {
        if (org == null) return null;

        ColorName = org.ColorName;

        return this;
    }

    public DbColor() { }
    public DbColor(ColorDTO org)
    {
        ColorId = Guid.NewGuid();
        UpdateFromDTO(org);
    }
    }
