using System;

namespace e_order.Domain.Event.Order
{
    public class ProductOrderCancelledEvent : Domain_Core.Events.Event
    {
        public ProductOrderCancelledEvent(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }

        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}