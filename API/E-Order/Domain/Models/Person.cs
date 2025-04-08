using System;
using Domain_Core.Models;

namespace e_order.Domain.Models
{
    public class Person : Entity
    {
        public Person(Guid id, string name, string identificationNumber, string postalCode, string place, string address)
        {
            Id = id;
            Name = name;
            IdentificationNumber = identificationNumber;
            PostalCode = postalCode;
            Place = place;
            Address = address;
        }
        public string Name { get; set; }
        public string IdentificationNumber { get; set; }
        public string PostalCode { get; set; }
        public string Place { get; set; }
        public string Address { get; set; }
    }
}