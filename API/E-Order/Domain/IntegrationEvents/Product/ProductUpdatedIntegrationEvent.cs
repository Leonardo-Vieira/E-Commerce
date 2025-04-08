using System;
using Domain_Core.Events;

namespace e_order.Domain.IntegrationEvents.Product
{
    public class ProductUpdatedIntegrationEvent : IntegrationEvent
    {
        public ProductUpdatedIntegrationEvent(Guid id, string code, string name,
        string description, bool status, decimal price, int stock, Guid brandId)
        {
            Id = id;
            Code = code;
            Status = status;
            Price = price;
            Name = name;
            Description = description;
            Stock = stock;
            BrandId = brandId;
            AggregateId = id;
        }

        public Guid Id { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool Status { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public Guid BrandId { get; private set; }
    }
}