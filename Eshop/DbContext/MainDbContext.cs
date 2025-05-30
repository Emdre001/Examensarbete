using Microsoft.EntityFrameworkCore;
using Models;


namespace DbContext;


public class MainDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Color> Colors { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Size> Sizes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<ProductSize> ProductSizes { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // ProductSize (with payload)
    modelBuilder.Entity<ProductSize>()
        .HasKey(ps => new { ps.ProductId, ps.SizeId });

    modelBuilder.Entity<ProductSize>()
        .HasOne(ps => ps.Product)
        .WithMany(p => p.ProductSizes)
        .HasForeignKey(ps => ps.ProductId);

    modelBuilder.Entity<ProductSize>()
        .HasOne(ps => ps.Size)
        .WithMany(s => s.ProductSizes)
        .HasForeignKey(ps => ps.SizeId);

    // Product - Color
    modelBuilder.Entity<Product>()
        .HasMany(p => p.Colors)
        .WithMany(c => c.Products);

    // Product - Order
    modelBuilder.Entity<Product>()
        .HasMany(p => p.Orders)
        .WithMany(o => o.Products);

    // Brand - Product
    modelBuilder.Entity<Product>()
        .HasOne(p => p.Brand)
        .WithMany(b => b.Products)
        .HasForeignKey(p => p.BrandId);

        // One-to-Many: User - Order
        modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.userId);

    // Brand config
    modelBuilder.Entity<Brand>(entity =>
    {
        entity.HasKey(b => b.BrandId);
        entity.Property(b => b.BrandName).IsRequired().HasMaxLength(100);
    });

    // ✅ Product - ProductImage
    modelBuilder.Entity<ProductImage>()
        .HasKey(pi => pi.ImageId);

    modelBuilder.Entity<ProductImage>()
        .HasOne(pi => pi.Product)
        .WithMany(p => p.ProductImages)
        .HasForeignKey(pi => pi.ProductId)
        .OnDelete(DeleteBehavior.Cascade);
}

    
}
