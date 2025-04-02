using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;

namespace DbModels;

    [Table("Colors")]
    public class DbColor : Color
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ColorName { get; set; }
    }
