using System;
using Domain_Core.Events;

namespace E_Client.Domain.IntegrationEvents.Product
{
    public class ProductRemovedIntegrationEvent : IntegrationEvent
    {
        public ProductRemovedIntegrationEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
        public Guid Id { get; protected set; }
    }
}