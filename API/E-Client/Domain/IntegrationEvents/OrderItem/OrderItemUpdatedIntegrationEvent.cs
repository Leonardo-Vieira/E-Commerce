using System;
using Domain_Core.Events;

namespace E_Client.Domain.Events.OrderItem
{
    public class OrderItemUpdatedIntegrationEvent : IntegrationEvent
    {
        public OrderItemUpdatedIntegrationEvent(Guid id, Guid? productId, Guid? orderId, int quantity)
        {
            Id = id;
            ProductId = productId;
            OrderId = orderId;
            Quantity = quantity;
        }
        public Guid Id { get; private set; }
        public Guid? ProductId { get; private set; }
        public Guid? OrderId { get; private set; }
        public int Quantity { get; private set; }
    }
}