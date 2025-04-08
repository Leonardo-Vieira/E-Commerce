using Domain_Core.Events;
using E_Product.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Product.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Provider> Provider { get; set; }
      
    
    }
}