using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain_Core.Models;

namespace E_Client.Models
{
    public class OrderItem : Entity
    {

        public OrderItem(Guid id, Guid? productId, Guid? orderId, Product product, Order order, int quantity)
        {
            Id = id;
            ProductId = productId;
            Product = product;
            OrderId = orderId;
            Order = order;
            Quantity = quantity;
        }
        public OrderItem()
        {
        }
        public Guid? ProductId { get; set; }
        [Required]
        public Product Product { get; set; }

        public Guid? OrderId { get; set; }
        [Required]
        public Order Order { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}