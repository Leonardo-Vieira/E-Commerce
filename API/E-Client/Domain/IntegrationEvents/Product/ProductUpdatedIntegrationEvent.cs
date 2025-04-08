using System;
using Domain_Core.Events;

namespace E_Client.Domain.IntegrationEvents.Product
{
    public class ProductUpdatedIntegrationEvent : IntegrationEvent
    {
        public ProductUpdatedIntegrationEvent(Guid id, Guid? brandId, Guid? productTypeId, string name,
        string description, bool status, decimal price)
        {
            Id = id;
            BrandId = brandId;
            ProductTypeId = productTypeId;
            Status = status;
            Price = price;
            Name = name;
            Description = description;
            AggregateId = id;
        }
        public Guid Id { get; private set; }
        public Guid? BrandId { get; private set; }
        public Guid? ProductTypeId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool Status { get; private set; }
        public decimal Price { get; private set; }
    }
}