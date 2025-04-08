using System;
using Domain_Core.Events;

namespace e_order.Domain.IntegrationEvents.Product
{
    public class ProductRemovedIntegrationEvent : IntegrationEvent
    {
        public ProductRemovedIntegrationEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id {get;set;}
    }
}