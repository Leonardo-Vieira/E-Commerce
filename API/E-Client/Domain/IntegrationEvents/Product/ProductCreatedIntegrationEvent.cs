using System;
using Domain_Core.Events;

namespace E_Client.Domain.IntegrationEvents.Product
{
    public class ProductCreatedIntegrationEvent : IntegrationEvent
    {
        public ProductCreatedIntegrationEvent(Guid id, Guid? brandId, Guid? productTypeId, string name,
        string description, bool status, decimal price)
        {
            Id = id;
            BrandId = brandId;
            ProductTypeId = productTypeId;
            Status = status;
            Price = price;
            Name = name;
            Description = description;
        }
        public Guid Id { get; protected set; }
        public Guid? BrandId { get; protected set; }
        public Guid? ProductTypeId { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public bool Status { get; protected set; }
        public decimal Price { get; protected set; }
    }
}