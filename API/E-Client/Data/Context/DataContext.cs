using Domain_Core.Events;
using E_Client.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Client.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<StoredEvent> StoredEvents { get; set; }
    }
}