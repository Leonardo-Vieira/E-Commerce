using System;
using Domain_Core.Events;

namespace E_Product.Domain.IntegrationEvents
{
    public class ProductTypeRemovedIntegrationEvent : IntegrationEvent
    {
          public ProductTypeRemovedIntegrationEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
        public Guid Id {get;set;}
    
    }
}