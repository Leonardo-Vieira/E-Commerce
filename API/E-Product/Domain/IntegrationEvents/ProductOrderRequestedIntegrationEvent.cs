using System;
using Domain_Core.Events;

namespace E_Product.Domain.IntegrationEvents
{
    public class ProductOrderRequestedIntegrationEvent : IntegrationEvent
    {
        public ProductOrderRequestedIntegrationEvent(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }

        public Guid ProductId { get; protected set; }
        public int Quantity { get; protected set; }
    }
}