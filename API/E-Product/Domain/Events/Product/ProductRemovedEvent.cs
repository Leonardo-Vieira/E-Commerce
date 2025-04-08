using System;
using Domain_Core.Events;

namespace E_Product.Domain.Events.Product
{
    public class ProductRemovedEvent : Event
    {
        public ProductRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
        public Guid Id {get;set;}
    }
}