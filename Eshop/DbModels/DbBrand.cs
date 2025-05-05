using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;
using Models.DTO;
using Newtonsoft.Json;

namespace DbModels;

[Table("Brands")]
public class DbBrand : Brand
{
    [Key]
    public override Guid BrandId { get; set; }

    [NotMapped] 
    public override List<Product> Products { get => DbProducts?.ToList<Product>(); set => throw new NotImplementedException(); }

    [JsonIgnore]
    public List<DbProduct> DbProducts { get; set; }

    [Required]
    public string BrandName { get; set; }

    public DbBrand UpdateFromDTO(BrandDTO org)
    {
        if (org == null) return null;

       BrandName = org.BrandName;

        return this;
    }

    public DbBrand() { }
    public DbBrand(BrandDTO org)
    {
        BrandId = Guid.NewGuid();
        UpdateFromDTO(org);
    }
}
