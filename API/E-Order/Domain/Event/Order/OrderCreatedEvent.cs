using System;
using System.Collections.Generic;

namespace e_order.Domain.Event.Order
{
    public class OrderCreatedEvent : Domain_Core.Events.Event
    {
        public OrderCreatedEvent(Guid id, Guid clientId, ICollection<e_order.Domain.Models.OrderItem> orderItems, DateTime date, bool state )
        {
            Id = id;
            ClientId = clientId;
            Date = date;
            State = state;
            OrderItems = orderItems;
        }

        public Guid Id { get; set; }
        public Guid ClientId {get;set;}
        public ICollection<e_order.Domain.Models.OrderItem> OrderItems { get; set; }
        public DateTime Date { get; set; }
        public bool State { get; set; }
        public Guid ProductId {get;set;}
    }
}