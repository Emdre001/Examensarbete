using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;
using Models.DTO;
using Newtonsoft.Json;
using System.Linq;

namespace DbModels
{
    [Table("Orders")]
    public class DbOrder : Order
    {
        [Key]
        public override Guid OrderId { get; set; }

        // Mappa DbProducts till ProductOrder
        [NotMapped]
        public override List<ProductOrder> Products 
        { 
            get => DbProducts?.Select(dp => new ProductOrder { ProductId = dp.ProductId, Product = dp}).ToList(); 
            set => throw new NotImplementedException(); 
        }

        [JsonIgnore]
        public List<DbProduct> DbProducts { get; set; }

        // User relation
        [NotMapped]
        public override User User { get => DbUser; set => throw new NotImplementedException(); }

        [JsonIgnore]
        [Required]
        public DbUser DbUser { get; set; }

        [Required]
        public virtual string OrderDetails { get; set; }

        [Required]
        public virtual DateTime OrderDate { get; set; }

        [Required]
        public virtual string OrderStatus { get; set; }

        [Required]
        public virtual int OrderAmount { get; set; }

        // Uppdatera från DTO
        public DbOrder UpdateFromDTO(OrderDTO org)
        {
            if (org == null) return null;

            OrderDate = org.OrderDate;
            OrderStatus = org.OrderStatus;
            OrderAmount = org.OrderAmount;

            return this;
        }

        // Konstruktorer
        public DbOrder() { }
        public DbOrder(OrderDTO org)
        {
            OrderId = Guid.NewGuid();
            UpdateFromDTO(org);
        }
    }
}
