using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;

namespace DbModels;

    [Table("Orders")]
    public class DbOrder : Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public virtual string OrderDetails { get; set; }

        [Required]
        public virtual DateTime OrderDate {get; set; }

        [Required]
        public virtual string OrderStatus { get; set; }

        [Required]
        public virtual int OrderAmount { get; set; }
    }
