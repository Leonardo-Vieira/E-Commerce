using System;
using System.Collections.Generic;
using Domain_Core.Models;
using e_order.Domain.Models;
using E_Order.Domain.Models.Dto;

namespace e_order.Domain.ViewModels
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public Client Client { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public DateTime DateOrder { get; set; }
        public bool State { get; set; }
    }
}