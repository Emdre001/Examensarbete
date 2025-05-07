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
    
    }