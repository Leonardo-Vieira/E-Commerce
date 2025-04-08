using System;
using Domain_Core.Commands;

namespace e_order.Domain.Commands.OrderItem
{
    public abstract class OrderItemCommand : Command
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public Guid? ProductId { get; set; }
    }
}