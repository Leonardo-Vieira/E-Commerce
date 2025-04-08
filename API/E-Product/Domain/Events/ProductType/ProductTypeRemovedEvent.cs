using System;
using Domain_Core.Events;

namespace E_Product.Domain.Events.ProductType
{
    public class ProductTypeRemovedEvent : Event
    {
        public ProductTypeRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
        public Guid Id {get; private set;}
    }
}