using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;

namespace Eshop.DbModels
{
    [Table("ShoeBrands")]
    public class DbBrand : Brand
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string BrandName { get; set; }
    }
}