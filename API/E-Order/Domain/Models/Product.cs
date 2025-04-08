using System;
using Domain_Core.Models;

namespace e_order.Domain.Models
{
    public class Product : Entity
    {
        public Product(Guid id, string code, string name, bool status, string description, decimal price, int stock, Guid brandId)
        {
            Id = id;
            Code = code;
            Status = status;
            Price = price;
            Name = name;
            Description = description;
            Stock = stock;
            BrandId = brandId;
        }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}