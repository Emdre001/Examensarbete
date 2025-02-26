using System.Data;
using Microsoft.EntityFrameworkCore;
using Configuration;
using Models;
using Models.DTO;
using DbModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Npgsql.Replication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace MainDbContext
{
     public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ShoeBrand> ShoeBrands { get; set; } 
        public DbSet<ShoeSize> ShoeSizes { get; set; }
        public DbSet<User> Users { get; set; }
    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

              modelBuilder.Entity<Color>(entity =>
            {
                entity.HasKey(c => c.ColorID);
                entity.Property(c => c.ColorName).IsRequired().HasMaxLength(50);
            });

              modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.OrderID);
                entity.Property(o => o.OrderDetails).IsRequired().HasMaxLength(500);
                entity.Property(o => o.OrderDate).IsRequired();
                entity.Property(o => o.OrderStatus).IsRequired().HasMaxLength(50);
                entity.Property(o => o.OrderAmount).IsRequired();
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.ProductId);
                entity.Property(p => p.ProductName).IsRequired().HasMaxLength(100);
                entity.Property(p => p.ProductType).IsRequired().HasMaxLength(100);
                entity.Property(p => p.ProductDescription).HasMaxLength(300);
                entity.Property(p => p.ProductStock).IsRequired();
                entity.Property(p => p.ProductPrice).IsRequired();
                entity.Property(p => p.ProductRating).IsRequired();
            });
            
              modelBuilder.Entity<ShoeBrand>(entity =>
            {
                entity.HasKey(b => b.BrandID);
                entity.Property(b => b.BrandName).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<ShoeSize>(entity =>
            {
                entity.HasKey(s => s.SizeID);
                entity.Property(s => s.MenSize).IsRequired();
                entity.Property(s => s.WomenSize).IsRequired();
                entity.Property(s => s.ChildrenSize).IsRequired();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.UserID);
                entity.Property(u => u.UserName).IsRequired().HasMaxLength(50);
                entity.Property(u => u.UserEmail).IsRequired().HasMaxLength(100);
                entity.Property(u => u.UserPassword).IsRequired();
                entity.Property(u => u.UserAddress).HasMaxLength(100);
                entity.Property(u => u.UserPhoneNr).IsRequired();
                entity.Property(u => u.UserRole).IsRequired().HasMaxLength(50);
            });
        }
    }
}