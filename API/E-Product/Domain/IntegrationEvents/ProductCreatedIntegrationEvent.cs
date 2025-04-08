using System;
using Domain_Core.Events;

namespace E_Product.Domain.IntegrationEvents
{
    public class ProductCreatedIntegrationEvent : IntegrationEvent
    {
        public ProductCreatedIntegrationEvent(Guid id, string code, string name, bool status, string description, decimal price, int stock, Guid? brandId, Guid? productTypeId)
        {
            Id = id;
            Code = code;
            Name = name;
            Status = status;
            Description = description;
            Price = price;
            Stock = stock;
            BrandId = brandId;
            ProductTypeId = productTypeId;
        }
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public Guid? BrandId { get; set; }
        public Guid? ProductTypeId { get; set; }
    }
}