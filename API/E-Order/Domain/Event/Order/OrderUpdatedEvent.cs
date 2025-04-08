using System;

namespace e_order.Domain.Event.Order
{
    public class OrderUpdatedEvent : Domain_Core.Events.Event
    {
        public OrderUpdatedEvent(Guid id, Guid clientId, DateTime dateOrder, bool state, e_order.Domain.Models.OrderItem orderItem, Guid? orderItemId)
        {
            Id = id;
            ClientId = clientId;
            DateOrder = dateOrder;
            State = state;
            OrderItem = orderItem;
            OrderItemId = orderItemId;
        }
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public DateTime DateOrder { get; set; }
        public bool State { get; set; }
        public e_order.Domain.Models.OrderItem OrderItem {get;set;}
        public Guid? OrderItemId {get;set;}
    }
}