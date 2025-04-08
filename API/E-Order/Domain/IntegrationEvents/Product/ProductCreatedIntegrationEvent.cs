using System;
using Domain_Core.Events;

namespace e_order.Domain.IntegrationEvents.Product
{
    public class ProductCreatedIntegrationEvent : IntegrationEvent
    {
        public ProductCreatedIntegrationEvent(Guid id, string code, string name,
        string description, bool status, decimal price, int stock, Guid brandId)
        {
            Code = code;
            Id = id;
            Status = status;
            Price = price;
            Name = name;
            Description = description;
            Stock = stock;
            BrandId = brandId;
        }
        public Guid Id { get; protected set; }
        public string Code { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public bool Status { get; protected set; }
        public decimal Price { get; protected set; }
        public int Stock { get; protected set; }
        public Guid BrandId { get; protected set; }
    }
}