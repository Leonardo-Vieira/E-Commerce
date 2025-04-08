using System;
using Domain_Core.Events;

namespace E_Product.Domain.Events.Provider
{
    public class ProviderRemovedEvent : Event
    {
         public ProviderRemovedEvent(Guid id)
        {
            BrandId = id;
            AggregateId = id;
        }

        public Guid BrandId {get;set;}
    }
}