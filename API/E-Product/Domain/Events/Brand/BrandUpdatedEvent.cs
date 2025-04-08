using System;
using Domain_Core.Events;

namespace E_Product.Domain.Events.Brand
{
    public class BrandUpdatedEvent : Event
    {
        public BrandUpdatedEvent(Guid id, string code, string name, string description)
        {
            Id = id;
            Code = code;
            Name = name;
            Description = description;
            AggregateId = id;
        }
        public Guid Id {get; private set;}
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
    }
}