using System;
using Domain_Core.Events;

namespace e_order.Domain.IntegrationEvents.Person
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

        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string IdentificationNumber { get; protected set; }
        public string PostalCode { get; protected set; }
        public string Place { get; protected set; }
        public string Address { get; protected set; }
    }
}