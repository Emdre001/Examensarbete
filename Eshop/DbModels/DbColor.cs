using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;
using Models.DTO;
using Newtonsoft.Json;
using Seido.Utilities.SeedGenerator;

namespace DbModels;

    [Table("Colors")]
    public class DbColor : Color
    {
        [Key]
        public override Guid ColorId { get; set; }

        [NotMapped] 
        public override List<IProduct> Products { get => DbProducts?.ToList<IProduct>(); set => throw new NotImplementedException(); }

        [JsonIgnore]
        public List<DbProduct> DbProducts { get; set; }


        [Required]
        public string ColorName { get; set; }

         public override DbColor Seed (SeedGenerator _seeder)
    {
        base.Seed (_seeder);
        return this;
    }

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
