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
        public override string UserRole { get; set; }
    }
