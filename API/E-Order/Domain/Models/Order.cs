using System;
using System.Collections.Generic;
using Domain_Core.Models;

namespace e_order.Domain.Models
{
    public class Order : Entity
    {
        public Order(Guid clientId, OrderItem[] orderItems, bool state)
        {
            ClientId = clientId;
            DateOrder = DateTime.Now;
            State = state;
            OrderItems = orderItems;
        }
        public Order()
        {
        }
        public Guid ClientId { get; set; }
        public Client Client { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
        public DateTime DateOrder { get; set; }
        public bool State { get; set; }
    }
}