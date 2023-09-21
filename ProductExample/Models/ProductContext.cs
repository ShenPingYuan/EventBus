using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProductExample.Models
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
     
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(p => p.ID).ValueGeneratedNever();

            modelBuilder.Entity<Product>().Property(p => p.Name).HasMaxLength(20);

            modelBuilder.Entity<Product>().Property(p => p.Price).HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Product>().HasData(new Product { ID = 123, Name = "C++", Price = 20.35M });

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Product> Products { get; set; }
    }
}
