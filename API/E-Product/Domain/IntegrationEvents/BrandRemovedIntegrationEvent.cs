using System;
using Domain_Core.Events;

namespace E_Product.Domain.IntegrationEvents
{
    public class BrandRemovedIntegrationEvent : IntegrationEvent
    {
        public BrandRemovedIntegrationEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
        public Guid Id {get;set;}
    }
}