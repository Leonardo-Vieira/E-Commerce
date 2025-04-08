using System;
using Domain_Core.Models;

namespace e_order.Domain.Models
{
    public class OrderItemViewModel
    {
        public int Quantity { get; set; }
        public Product Product { get; set; }
    }
}