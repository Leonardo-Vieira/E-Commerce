using System;
using Domain_Core.Events;

namespace E_Client.Domain.IntegrationEvents.ProductType
{
    public class ProductTypeCreatedIntegrationEvent : IntegrationEvent
    {
        public ProductTypeCreatedIntegrationEvent(Guid id, string code, string name, string description)
        {
            Id = id;
            Code = code;
            Name = name;
            Description = description;
        }
        public Guid Id { get; protected set; }
        public string Code { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
    }
}