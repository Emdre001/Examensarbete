using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;
using Models.DTO;
using Newtonsoft.Json;

namespace DbModels;

    [Table("Products")]
    public class DbProduct : Product
    {
        [Key]
        public override Guid ProductId { get; set; }

        [NotMapped]
        public override List<IBrand> Brands { get => DbBrand?.ToList<IBrand>(); set => throw new NotImplementedException(); }

        [JsonIgnore]
        public List<DbBrand> DbBrands { get; set; }


        [NotMapped]
        public override List<IColor> Colors { get => DbColor?.ToList<IColor>(); set => throw new NotImplementedException(); }

        [JsonIgnore]
        public List<DbColor> DbColors { get; set; }

        [NotMapped]
        public override List<ISize> Sizes { get => DbSize?.ToList<ISize>(); set => throw new NotImplementedException(); }
        
        [JsonIgnore]
        public List<DbSize> DbSizes { get; set; }

        [NotMapped]
        public override List<IOrder> Orders { get => DbOrder?.ToList<IOrder>(); set => throw new NotImplementedException(); }

        [JsonIgnore]
        public List<DbOrder> DbOrders { get; set; }



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

        //SEEDING STUFF GOES HERE
        //
        //
        //
    }
