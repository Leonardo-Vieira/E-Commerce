using System;
using Domain_Core.Events;

namespace E_Client.Domain.IntegrationEvents.Client
{
    public class ClientRemovedIntegrationEvent : IntegrationEvent
    {
        public ClientRemovedIntegrationEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
        public Guid Id { get; protected set; }
    }
}