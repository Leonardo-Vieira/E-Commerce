using System;
using Domain_Core.Commands;
using E_Order.Domain.Models.Dto;

namespace e_order.Domain.Commands.Order
{
    public abstract class OrderCommand : Command
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public OrderItemDto[] OrderItems {get;set;}
        public DateTime DateOrder { get; set; }
        public bool State { get; set; }
        
    }
}