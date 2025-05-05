using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;
using Models.DTO;
using Newtonsoft.Json;

namespace DbModels;

    [Table("Orders")]
    public class DbOrder : Order
    {
        [Key]
        public override Guid OrderId { get; set; }

        [NotMapped] 
        public override List<IProduct> Products { get => DbProducts?.ToList<IProduct>(); set => throw new NotImplementedException(); }

        [JsonIgnore]
        public List<DbProduct> DbProducts { get; set; }

        [NotMapped]
        public override IUser User { get => DbUser; set => throw new NotImplementedException(); }
        [JsonIgnore]
        [Required]
        public  DbUser DbUser { get; set; }

        [Required]
        public virtual string OrderDetails { get; set; }

        [Required]
        public virtual DateTime OrderDate {get; set; }

        [Required]
        public virtual string OrderStatus { get; set; }

        [Required]
        public virtual int OrderAmount { get; set; }

    public DbOrder UpdateFromDTO(OrderDTO org)
    {
        if (org == null) return null;

        OrderDate = org.OrderDate;
        OrderStatus = org.OrderStatus;
        OrderAmount = org.OrderAmount;

        return this;
    }

    public DbOrder() { }
    public DbOrder(OrderDTO org)
    {
        OrderId = Guid.NewGuid();
        UpdateFromDTO(org);
    }
    }
