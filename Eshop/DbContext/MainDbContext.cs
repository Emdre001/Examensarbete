using Microsoft.EntityFrameworkCore;
using Models;

namespace DbContext
{
    public class MainDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<User> Users { get; set; }

        // Join tables for Many-to-Many relations
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Product - Brand: One-to-Many relationship (A Brand can have many Products)
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.Cascade); // Assuming you want cascading delete for Brand-Product relation

            // Product - Color: Many-to-Many relationship (Through ProductColor join table)
            modelBuilder.Entity<ProductColor>()
                .HasKey(pc => new { pc.ProductId, pc.ColorId });

            modelBuilder.Entity<ProductColor>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductColors)
                .HasForeignKey(pc => pc.ProductId)
                .OnDelete(DeleteBehavior.NoAction);  // No cascading delete for many-to-many

            modelBuilder.Entity<ProductColor>()
                .HasOne(pc => pc.Color)
                .WithMany(c => c.Products)
                .HasForeignKey(pc => pc.ColorId)
                .OnDelete(DeleteBehavior.NoAction);  // No cascading delete for many-to-many

            // Product - Size: Many-to-Many relationship (Through ProductSize join table)
            modelBuilder.Entity<ProductSize>()
                .HasKey(ps => new { ps.ProductId, ps.SizeId });

            modelBuilder.Entity<ProductSize>()
                .HasOne(ps => ps.Product)
                .WithMany(p => p.ProductSizes)
                .HasForeignKey(ps => ps.ProductId)
                .OnDelete(DeleteBehavior.NoAction);  // No cascading delete for many-to-many

            modelBuilder.Entity<ProductSize>()
                .HasOne(ps => ps.Size)
                .WithMany(s => s.Products)
                .HasForeignKey(ps => ps.SizeId)
                .OnDelete(DeleteBehavior.NoAction);  // No cascading delete for many-to-many

            // Product - Order: Many-to-Many relationship (Through ProductOrder join table)
            modelBuilder.Entity<ProductOrder>()
                .HasKey(po => new { po.ProductId, po.OrderId });

            modelBuilder.Entity<ProductOrder>()
                .HasOne(po => po.Product)
                .WithMany(p => p.ProductOrders)
                .HasForeignKey(po => po.ProductId)
                .OnDelete(DeleteBehavior.NoAction);  // No cascading delete for many-to-many

            modelBuilder.Entity<ProductOrder>()
                .HasOne(po => po.Order)
                .WithMany(o => o.Products)
                .HasForeignKey(po => po.OrderId)
                .OnDelete(DeleteBehavior.NoAction);  
        }
    }
}
