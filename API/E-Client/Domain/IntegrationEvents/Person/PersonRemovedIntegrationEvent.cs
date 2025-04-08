using System;
using Domain_Core.Events;

namespace E_Client.Domain.IntegrationEvents.Person
{
    public class PersonRemovedIntegrationEvent : IntegrationEvent
    {
        public PersonRemovedIntegrationEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
        public Guid Id { get; protected set; }
    }
}