using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;
using Models.DTO;
using Newtonsoft.Json;
using Seido.Utilities.SeedGenerator;

namespace DbModels;

    [Table("Sizes")]
    public class DbSize : Size
    {
        [Key]
        public override Guid SizeId { get; set; }

        [NotMapped] 
        public override List<IProduct> Products { get => DbProducts?.ToList<IProduct>(); set => throw new NotImplementedException(); }

        [JsonIgnore]
        public List<DbProduct> DbProducts { get; set; }

        [Required]
        public int MenSize { get; set; }

        [Required]
        public int WomenSize { get; set; }

        [Required]
        public int ChildrenSize { get; set; }

         public override DbSize Seed (SeedGenerator _seeder)
    {
        base.Seed (_seeder);
        return this;
    }

    public DbSize UpdateFromDTO(SizeDTO org)
    {
        if (org == null) return null;

        MenSize = org.MenSize;
        WomenSize = org.WomenSize;
        ChildrenSize = org.ChildrenSize;

        return this;
    }

    public DbSize() { }
    public DbSize(SizeDTO org)
    {
        SizeId = Guid.NewGuid();
        UpdateFromDTO(org);
    }
    }
