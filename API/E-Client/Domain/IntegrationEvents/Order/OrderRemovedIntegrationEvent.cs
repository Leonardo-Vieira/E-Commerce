using System;
using Domain_Core.Events;

namespace E_Client.Domain.Events.Order
{
    public class OrderRemovedIntegrationEvent : IntegrationEvent
    {
        public OrderRemovedIntegrationEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
        public Guid Id { get; protected set; }
    }
}