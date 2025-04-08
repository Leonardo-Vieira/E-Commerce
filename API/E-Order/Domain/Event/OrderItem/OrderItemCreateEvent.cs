using System;

namespace e_order.Domain.Event.OrderItem
{
    public class OrderItemCreateEvent : Domain_Core.Events.Event
    {
        public OrderItemCreateEvent(Guid id, Guid productId, int quantity)
        {
            Id = id;
            ProductId = productId;
            Quantity = quantity;
        }

        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}