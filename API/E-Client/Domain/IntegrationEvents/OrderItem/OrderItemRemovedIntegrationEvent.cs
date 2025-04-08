using System;
using Domain_Core.Events;

namespace E_Client.Domain.Events.OrderItem
{
    public class OrderItemRemovedIntegrationEvent : IntegrationEvent
    {
        public OrderItemRemovedIntegrationEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
        public Guid Id { get; protected set; }
    }
}