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
        public override List<IProduct> Products { get; set; }
        [JsonIgnore]
        public List<DbProduct> DbProducts { get; set; }


        [Required]
        public string ColorName { get; set; }


    }
