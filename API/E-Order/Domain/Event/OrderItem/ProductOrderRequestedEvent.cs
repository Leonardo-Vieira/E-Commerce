using System;

namespace e_order.Domain.Event.OrderItem
{
    public class ProductOrderRequestedEvent : Domain_Core.Events.Event
    {
        public ProductOrderRequestedEvent(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }

        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}