using System;
using Domain_Core.Models;

namespace e_order.Domain.Models
{
    public class OrderItem : Entity
    {
        public OrderItem(int quantity, Guid productId)
        {
            Quantity = quantity;
            ProductId = productId;
        }
      
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public Guid OrderId { get; set; }
    }
}