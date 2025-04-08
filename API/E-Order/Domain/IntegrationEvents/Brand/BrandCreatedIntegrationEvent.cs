using System;
using Domain_Core.Events;

namespace e_order.Domain.IntegrationEvents.Brand
{
    public class BrandCreatedIntegrationEvent : IntegrationEvent
    {
        public BrandCreatedIntegrationEvent(Guid id, string code, string name, string description)
        {
            Id = id;
            Code = code;
            Name = name;
            Description = description;
        }
        public Guid Id { get; protected set; }
        public string Code {get; protected set;}
        public string Name {get; protected set;}
        public string Description {get; protected set;}
    }
}