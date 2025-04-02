using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;

namespace DbModels;

    [Table("Products")]
    public class DbProduct : Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public string ProductType { get; set; }

        [Required]
        public string ProductDescription { get; set; }

        [Required]
        public int ProductStock { get; set; }

        [Required]
        public int ProductPrice { get; set; }

        [Required]
        public int ProductRating { get; set; }
    }
