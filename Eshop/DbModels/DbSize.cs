using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;

namespace DbModels;

    [Table("Sizes")]
    public class DbSize : Size
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MenSize { get; set; }

        [Required]
        public int WomenSize { get; set; }

        [Required]
        public int ChildrenSize { get; set; }
    }
