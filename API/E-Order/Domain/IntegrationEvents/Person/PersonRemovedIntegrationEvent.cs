using System;
using Domain_Core.Events;

namespace e_order.Domain.IntegrationEvents.Person
{
    public class PersonRemovedIntegrationEvent : IntegrationEvent
    {
        public PersonRemovedIntegrationEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id {get;set;}
    }
}