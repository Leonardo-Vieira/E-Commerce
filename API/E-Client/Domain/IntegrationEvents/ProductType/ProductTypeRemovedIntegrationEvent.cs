using System;
using Domain_Core.Events;

namespace E_Client.Domain.IntegrationEvents.ProductType
{
    public class ProductTypeRemovedIntegrationEvent : IntegrationEvent
    {
        public ProductTypeRemovedIntegrationEvent(Guid id)
        {
            Id = id;
            AggregateId = Id;
        }
        public Guid Id { get; protected set; }
    }
}