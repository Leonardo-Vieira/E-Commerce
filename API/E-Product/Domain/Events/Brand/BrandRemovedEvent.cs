using System;
using Domain_Core.Events;

namespace E_Product.Domain.Events.Brand
{
    public class BrandRemovedEvent : Event
    {
        public BrandRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id {get;set;}
    }
}