using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;
using Models.DTO;
using Newtonsoft.Json;

namespace DbModels;

    [Table("Users")]
    public class DbUser : User
    {
        [Key]
        public override Guid UserId { get; set; }

        [NotMapped] 
        public override List<IOrder> Orders { get; set; }

        [JsonIgnore]
        public List<DbOrder> DbOrders { get; set; }

        [Required]
        public override string UserName { get; set; }

        [Required]
        public override string UserEmail { get; set; }

        [Required]
        public override string UserPassword { get; set; }

        [Required]
        public override string UserAddress { get; set; }

        [Required]
        public override int UserPhoneNr { get; set; }

        [Required]
        public string UserRole { get; set; }

    }