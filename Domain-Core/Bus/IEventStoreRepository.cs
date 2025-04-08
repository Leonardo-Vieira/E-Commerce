using System;
using System.Collections.Generic;
using Domain_Core.Events;


namespace Domain_Core.Bus
{
    public interface IEventStoreRepository
    {
        void Store(StoredEvent theEvent);
        IList<StoredEvent> All(Guid aggregateId);
    }
}