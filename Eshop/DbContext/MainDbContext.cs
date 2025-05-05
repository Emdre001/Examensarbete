using System.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;
using DbModels;

namespace DbContext;

     public class MainDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }

        public DbSet<DbProduct> Products { get; set; }
        public DbSet<DbColor> Colors { get; set; }
        public DbSet<DbOrder> Orders { get; set; }
        public DbSet<DbBrand> Brands { get; set; } 
        public DbSet<DbSize> Sizes { get; set; }
        public DbSet<DbUser> Users { get; set; }
    
        #region model the Views
        public DbSet<GstUsrInfoDbDTO> InfoDbView { get; set; }
        public DbSet<GstUsrInfoProductsDTO> InfoProductsView { get; set; }
        public DbSet<GstUsrInfoColorsDTO> InfoColorsView { get; set; }
        public DbSet<GstUsrInfoBrandsDTO> InfoBrandsView { get; set; }
        public DbSet<GstUsrInfoSizesDTO> InfoSizesView { get; set; }
        public DbSet<GstUsrInfoOrdersDTO> InfoOrdersView { get; set; }
        public DbSet<GstUsrInfoUsersDTO> InfoUsersView { get; set; }

    //gör en för varje view i gstuserDTO
    #endregion

      protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    
          modelBuilder.Entity<Color>(entity =>
           {
            entity.HasKey(c => c.ColorId);
            entity.Property(c => c.ColorName).IsRequired().HasMaxLength(50);
        });

          modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(o => o.OrderId);
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
            entity.Property(p => p.ProductPrice).IsRequired();
            entity.Property(p => p.ProductRating).IsRequired();
        });
        
          modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(b => b.BrandId);
            entity.Property(b => b.BrandName).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Size>(entity =>
            {
                entity.HasKey(s => s.SizeId);
                entity.Property(s => s.SizeValue).IsRequired();
                entity.Property(s => s.SizeStock).IsRequired();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.UserId);
                entity.Property(u => u.UserName).IsRequired().HasMaxLength(50);
                entity.Property(u => u.UserEmail).IsRequired().HasMaxLength(100);
                entity.Property(u => u.UserPassword).IsRequired();
                entity.Property(u => u.UserAddress).HasMaxLength(100);
                entity.Property(u => u.UserPhoneNr).IsRequired();
                entity.Property(u => u.UserRole).IsRequired().HasMaxLength(50);
            });
        }
    }