using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain_Core.Models;

namespace E_Client.Models
{
    public class Product : Entity
    {
        public Product(Guid id, Guid? brandId, Guid? productTypeId, string name, string description, bool state, decimal price)
        {
            Id = id;
            BrandId = brandId;
            ProductTypeId = productTypeId;
            Name = name;
            Description = description;
            State = state;
            Price = price;
        }
        public Product()
        {

        }

        public Guid? BrandId { get; set; }
        public Brand Brand { get; set; }
        public Guid? ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }

        [MinLength(3)]
        [MaxLength(255)]
        public string Name { get; set; }

        [MinLength(3)]
        [MaxLength(255)]
        public string Description { get; set; }

        public bool State { get; set; }

        public decimal Price { get; set; }
    }
}