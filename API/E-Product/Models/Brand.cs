using System;
using Domain_Core.Models;

namespace E_Product.Models
{
    public class Brand 
    {
       /*  public Brand(string code, string name, string description )
        {
            Code = code;
            Name = name;
            Description = description;
        } */

        //public Brand() { }
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}