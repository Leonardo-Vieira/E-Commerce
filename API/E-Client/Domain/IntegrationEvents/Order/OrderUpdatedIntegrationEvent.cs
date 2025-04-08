using System;
using Domain_Core.Events;

namespace E_Client.Domain.Events.Order
{
    public class OrderUpdatedIntegrationEvent : IntegrationEvent
    {
        public OrderUpdatedIntegrationEvent(Guid id, Guid? clientId, DateTime dateOrder, bool state)
        {
            Id = id;
            ClientId = clientId;
            DateOrder = dateOrder;
            State = state;
            AggregateId = id;
        }
        public Guid Id { get; private set; }
        public Guid? ClientId { get; private set; }
        public E_Client.Models.Client Client { get; private set; }
        public DateTime DateOrder { get; private set; }
        public bool State { get; private set; }
    }
}