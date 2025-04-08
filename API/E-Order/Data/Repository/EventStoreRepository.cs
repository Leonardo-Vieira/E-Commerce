using System;
using System.Collections.Generic;
using Domain_Core.Events;
using Domain_Core.Bus;
using e_order.Data;
using System.Linq;
using E_Order.Data;

namespace e_order.Repository
{
    public class EventStoreRepository : IEventStoreRepository
    {
        private readonly EventStoreContext _context;

        public EventStoreRepository(EventStoreContext context)
        {
            _context = context;
        }
        public IList<StoredEvent> All(Guid aggregateId)
        {
            return (from e in _context.StoredEvents where e.AggregateId == aggregateId select e).ToList();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public void Store(StoredEvent theEvent)
        {
            _context.StoredEvents.Add(theEvent);
            _context.SaveChanges();
        }
    }
}