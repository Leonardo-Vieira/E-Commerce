using System;
using Domain_Core.Models;

namespace e_order.Domain.Models
{
    public class PersonViewModel
    {
        public string Name { get; set; }
        public string IdentificationNumber { get; set; }
        public string PostalCode { get; set; }
        public string Place { get; set; }
        public string Address { get; set; }
        public BrandViewModel Brand { get; set; }
    }
}