using System;
using Domain_Core.Events;

namespace E_Client.Domain.IntegrationEvents.Person
{
    public class PersonUpdatedIntegrationEvent : IntegrationEvent
    {
        public PersonUpdatedIntegrationEvent(Guid id, string name, string identificationNumber, string postalCode, string place, string address)
        {
            Id = id;
            Name = name;
            IdentificationNumber = identificationNumber;
            PostalCode = postalCode;
            Place = place;
            Address = address;
        }
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string IdentificationNumber { get; private set; }
        public string PostalCode { get; private set; }
        public string Place { get; private set; }
        public string Address { get; private set; }
    }
}