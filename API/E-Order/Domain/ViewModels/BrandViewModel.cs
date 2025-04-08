using System;
using Domain_Core.Models;

namespace e_order.Domain.Models
{
    public class BrandViewModel
    {
        public BrandViewModel(string code, string name, string description)
        {
            Code = code;
            Name = name;
            Description = description;
        }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}