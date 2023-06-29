using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTelephone
{
    public class StoreContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Colors> ProductColors { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ProductSize>().HasKey(p => new { p.ProductId, p.Size });
            modelBuilder.Entity<ProductImage>().HasKey(p => new { p.ProductId, p.ImageUrl });
            modelBuilder.Entity<Colors>().HasKey(p => new { p.ProductId, p.Color });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=USER;Initial Catalog=Store;Integrated Security=True;TrustServerCertificate=True;");
        }

    }
}
