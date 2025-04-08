using System;
using Domain_Core.Events;

namespace E_Client.Domain.IntegrationEvents.ProductType
{
    public class ProductTypeUpdatedIntegrationEvent : IntegrationEvent
    {
        public ProductTypeUpdatedIntegrationEvent(Guid id, string code, string name, string description)
        {
            Id = id;
            Code = code;
            Name = name;
            Description = description;
            AggregateId = id;
        }
        public Guid Id { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
    }
}