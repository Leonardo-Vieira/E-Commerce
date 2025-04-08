using Domain_Core.Events;
using e_order.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace e_order.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        public DbSet<Brand> Brands { get; set; } 
        public DbSet<Product> Products { get; set; } 
        public DbSet<Client> Clients { get; set; } 
        public DbSet<Order> Orders { get; set; } 
        public DbSet<OrderItem> OrderItems { get; set; } 
        public DbSet<Person> Persons { get; set; } 
        public DbSet<StoredEvent> StoredEvents { get; set; }
    }
}