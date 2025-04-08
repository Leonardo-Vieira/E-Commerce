using System;
using Domain_Core.Models;

namespace e_order.Domain.Models
{
    public class Brand
    {
        public Brand(Guid id, string code, string name, string description)
        {
            Id = id;
            Code = code;
            Name = name;
            Description = description;
        }
        public Brand () {}
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}