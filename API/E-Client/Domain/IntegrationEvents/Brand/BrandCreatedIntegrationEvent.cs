using System;
using Domain_Core.Events;

namespace E_Client.Domain.IntegrationEvents.Brand
{
    public class BrandCreatedIntegrationEvent : IntegrationEvent
    {
        public BrandCreatedIntegrationEvent(Guid id, string code, string name, string description)
        {
            Id = Id;
            Code = code;
            Name = name;
            Description = description;
        }
        public Guid Id { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
    }
}