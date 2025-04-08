using System;
using Domain_Core.Commands;
using E_Product.Models;

namespace E_Product.Domain.Commands.Product
{
    public abstract class ProductCommand : Command
    {
        public Guid Id { get; protected set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public Guid? ProviderId { get; set; }
        public Guid? BrandId { get; set; }
        public Guid? ProductTypeId { get; set; }
    }
}