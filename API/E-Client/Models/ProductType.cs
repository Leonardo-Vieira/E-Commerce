using System;
using Domain_Core.Models;

namespace E_Client.Models
{
    public class ProductType : Entity
    {
        public ProductType(Guid id, string code, string name, string description)
        {
            Id = id;
            Code = code;
            Name = name;
            Description = description;
        }
        public ProductType() {}
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}