using Domain_Core.Events;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Data
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