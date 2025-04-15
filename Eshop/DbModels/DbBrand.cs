using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;
using Models.DTO;
using Newtonsoft.Json;
using Seido.Utilities.SeedGenerator;

namespace DbModels;

[Table("Brands")]
public class DbBrand : Brand
{
    [Key]
    public override Guid BrandId { get; set; }

    [NotMapped] 
    public override List<IProduct> Products { get => DbProducts?.ToList<IProduct>(); set => throw new NotImplementedException(); }

    [JsonIgnore]
    public List<DbProduct> DbProducts { get; set; }

    [Required]
    public string BrandName { get; set; }

     public override DbBrand Seed (SeedGenerator _seeder)
    {
        base.Seed (_seeder);
        return this;
    }

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
