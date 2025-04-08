using System;

namespace E_Order.Domain.Models.Dto
{
    public class OrderDto
    {
        public Guid ClientId { get; set; }
        public OrderItemDto[] OrderItems { get; set; }
    }
}