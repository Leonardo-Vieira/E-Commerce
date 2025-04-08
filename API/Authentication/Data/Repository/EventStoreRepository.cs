using System;
using System.Collections.Generic;
using Domain_Core.Events;
using Domain_Core.Bus;
using Authentication.Data;
using System.Linq;

namespace Authentication.Repository
{
    public class EventStoreRepository : IEventStoreRepository 
    {
        private readonly EventStoreContext _context;
        
        public EventStoreRepository (EventStoreContext context) 
        {   
            _context = context;
        }
        public  IList<StoredEvent> All (Guid aggregateId) 
        {
           return (from e in _context.StoredEvents where e.AggregateId == aggregateId select e).ToList();
        }
        public void Dispose () 
        {
            _context.Dispose();
        }

        public void Store (StoredEvent theEvent) 
        {
            _context.StoredEvents.Add(theEvent);
            _context.SaveChanges();
        }
    }
}