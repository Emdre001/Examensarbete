using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;
using Models.DTO;
using Newtonsoft.Json;


namespace DbModels;

    [Table("Orders")]
    public class DbOrder : Order
    {
        [Key]
        public override Guid OrderId { get; set; }

        [NotMapped] 
        public override List<IProduct> Products { get; set; }
        [JsonIgnore]
        public List<DbProduct> DbProducts { get; set; }

        [NotMapped]
        public override IUser User { get; set; }
        [JsonIgnore]
        [Required]
        public  DbUser DbUser { get; set; }

        [Required]
        public virtual string OrderDetails { get; set; }

        [Required]
        public virtual DateTime OrderDate {get; set; }

        [Required]
        public virtual string OrderStatus { get; set; }

        [Required]
        public virtual int OrderAmount { get; set; }
    }
