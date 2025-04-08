using System;

namespace e_order.Domain.Event.OrderItem
{
    public class OrderItemRemovedEvent : Domain_Core.Events.Event
    {
        public OrderItemRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }
    }
}