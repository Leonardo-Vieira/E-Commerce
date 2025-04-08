using System;
using Domain_Core.Events;

namespace E_Client.Domain.Events.OrderItem
{
    public class OrderItemCreatedIntegrationEvent : IntegrationEvent
    {
        public OrderItemCreatedIntegrationEvent(Guid id, Guid? productId, Guid? orderId, int quantity)
        {
            Id = id;
            ProductId = productId;
            OrderId = orderId;
            Quantity = quantity;
        }
        public Guid Id { get; protected set; }
        public Guid? ProductId { get; protected set; }
        public Guid? OrderId { get; protected set; }
        public int Quantity { get; protected set; }

    }
}