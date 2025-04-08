using System;

namespace e_order.Domain.Event.Order
{
    public class OrderRemovedEvent : Domain_Core.Events.Event
    {
        public OrderRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }
    }
}