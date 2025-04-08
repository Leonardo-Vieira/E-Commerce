using System;
using e_order.Domain.Models;

namespace E_Order.Domain.Models.Dto
{
    public class OrderItemDto
    {
        public ProductViewModel Product { get; set; }
        public int Quantity { get; set; }
    }
}