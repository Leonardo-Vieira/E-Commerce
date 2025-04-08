using System;
using System.Collections.Generic;

namespace E_Order.Domain.Models.Dto
{
    public class OrderToClientDto
    {
        public Guid Id { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public DateTime DateOrder { get; set; }
        public bool State { get; set; }
    }
}