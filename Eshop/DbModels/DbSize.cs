using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;
using Models.DTO;
using Newtonsoft.Json;


namespace DbModels;

    [Table("Sizes")]
    public class DbSize : Size
    {
        [Key]
        public override Guid SizeId { get; set; }

        [NotMapped] 
        public override List<IProduct> Products { get; set; }

        [JsonIgnore]
        public List<DbProduct> DbProducts { get; set; }

        [Required]
        public int SizeValue { get; set; }

        [Required]
        public int SizeStock { get; set; }

    }
