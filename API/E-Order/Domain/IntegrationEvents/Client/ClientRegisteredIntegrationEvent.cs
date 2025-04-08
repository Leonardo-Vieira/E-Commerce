using System;
using Domain_Core.Events;

namespace e_order.Domain.IntegrationEvents.Client
{
    public class ClientRegisteredIntegrationEvent : IntegrationEvent
    {
        public ClientRegisteredIntegrationEvent(Guid id, string username, string email, string name, string identificationNumber, string postalCode, string place,string address ,bool isCollaborator)
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
}