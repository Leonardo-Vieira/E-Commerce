using Domain_Core.Events;
using Microsoft.EntityFrameworkCore;

namespace E_Product.Data
{
    public class EventStoreContext: DbContext
    {
        public EventStoreContext(DbContextOptions<EventStoreContext> options)
            : base(options)
        {
        }
          public DbSet<StoredEvent> StoredEvents { get; set; }
    }
}