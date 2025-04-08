using System;
using Domain_Core.Events;

namespace E_Client.Domain.Events.Client
{
    public class ClientRemovedEvent : Event
    {
        public ClientRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
        public Guid Id { get; set; }
    }
}