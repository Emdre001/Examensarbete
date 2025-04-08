using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;

namespace DbModels;

    [Table("Users")]
    public class DbUser : User
    {
        [Key]
        public override Guid UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string UserEmail { get; set; }

        [Required]
        public string UserPassword { get; set; }

        [Required]
        public string UserAddress { get; set; }

        [Required]
        public int UserPhoneNr { get; set; }

        [Required]
        public string UserRole { get; set; }
    }
