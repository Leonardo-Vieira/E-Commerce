using System;
using Domain_Core.Models;

namespace e_order.Domain.Models
{
    public class ProductViewModel
    {
        public Guid Id {get;set;}
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public Brand Brand { get; set; }
    }
}