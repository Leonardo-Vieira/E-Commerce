using System;
using Domain_Core.Events;

namespace E_Client.Domain.IntegrationEvents.Brand
{
    public class BrandRemovedIntegrationEvent : IntegrationEvent
    {
        public BrandRemovedIntegrationEvent(Guid id)
        {
            Id = id;  
            AggregateId = id;
           
        }
        public Guid Id  { get; protected set; }
    }
}