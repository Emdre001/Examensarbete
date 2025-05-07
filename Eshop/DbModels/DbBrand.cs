using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;
using Models.DTO;
using Newtonsoft.Json;


namespace DbModels;

[Table("Brands")]
public class DbBrand : Brand
{
    [Key]
    public override Guid BrandId { get; set; }

    [NotMapped] 
    public override List<IProduct> Products { get; set; }

    [JsonIgnore]
    public List<DbProduct> DbProducts { get; set; }

    [Required]
    public string BrandName { get; set; }

}