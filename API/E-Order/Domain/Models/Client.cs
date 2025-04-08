using System;
using System.ComponentModel.DataAnnotations.Schema;
using Domain_Core.Models;

namespace e_order.Domain.Models
{
    public class Client //: Entity
    {
       public Client(Guid id, string username, string email, string name, string identificationNumber, string postalCode, string place,string address ,bool isCollaborator)
        {
            Id = id;
            Username = username;
            Email = email;
            Name = name;
            IdentificationNumber = identificationNumber;
            PostalCode = postalCode;
            Place = place;
            Address = address;
            IsCollaborator = isCollaborator;
        }
        public Client() {
            
        }
        public Guid Id {get;set;}
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string IdentificationNumber { get; set; }
        public string PostalCode { get; set; }
        public string Place { get; set; }
        public string Address { get; set; }
        public bool IsCollaborator { get; set; }
    }
    public class TokenConfigurations
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Hour { get; set; }
    }
}