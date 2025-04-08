using System;
using Domain_Core.Models;

namespace E_Client.Models
{
    public class Brand : Entity
    {
        public Brand(Guid id, string code, string name, string description)
        {
            Id = id;
            Code = code;
            Name = name;
            Description = description;
        }

        public Brand() { }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}