using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;

namespace Eshop.DbModels
{
    [Table("Users")]
    public class DbUser : ShoeBrand
    {
        [Key]
        public int Id { get; set; }

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
}