using System;
using Domain_Core.Events;

namespace E_Client.Domain.Events.Order
{
    public class OrderCreatedIntegrationEvent : IntegrationEvent
    {
        public OrderCreatedIntegrationEvent(Guid id, Guid? clientId, DateTime dateOrder, bool state)
        {
            Id = id;
            ClientId = clientId;
            DateOrder = dateOrder;
            State = state;
        }
        public Guid Id { get; protected set; }
        public Guid? ClientId { get; protected set; }
        public DateTime DateOrder { get; protected set; }
        public bool State { get; protected set; }
    }
}