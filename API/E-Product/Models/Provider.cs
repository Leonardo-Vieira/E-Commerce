using System;
using Domain_Core.Models;

namespace E_Product.Models
{
    public class Provider
    {
      /*   public Provider(string code, string name, string description, string phone, string postalCode, string place, string identificationNumber)
        {
            Code = code;
            Name = name;
            Description = description;
            Phone = phone;
            PostalCode = postalCode;
            Place = place;
            IdentificationNumber = identificationNumber;
        }
        public Provider() {} */
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string PostalCode { get; set; }
        public string Place { get; set; }
        public string IdentificationNumber { get; set; }
    }
}