using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;
using Models.DTO;
using Newtonsoft.Json;
using Seido.Utilities.SeedGenerator;

namespace DbModels;

    [Table("Users")]
    public class DbUser : User
    {
        [Key]
        public override Guid UserId { get; set; }

        [NotMapped] 
        public override List<IOrder> Orders { get => DbOrders?.ToList<IOrder>(); set => throw new NotImplementedException(); }

        [JsonIgnore]
        public List<DbOrder> DbOrders { get; set; }

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

         public override DbUser Seed (SeedGenerator _seeder)
    {
        base.Seed (_seeder);
        return this;
    }

    public DbUser UpdateFromDTO(UserDTO org)
    {
        if (org == null) return null;

        UserName = org.UserName;
        UserEmail = org.UserEmail;
        UserPassword = org.UserPassword;
        UserAddress = org.UserAddress;
        UserPhoneNr = org.UserPhoneNr;
        UserRole = org.UserRole;

        return this;
    }

    public DbUser() { }
    public DbUser(UserDTO org)
    {
        UserId = Guid.NewGuid();
        UpdateFromDTO(org);
    }
    }
