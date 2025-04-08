using System;
using Domain_Core.Events;

namespace E_Product.Domain.Events.Provider
{
    public class ProviderUpdatedEvent : Event
    {
        public ProviderUpdatedEvent(Guid id, string code, string name, string description, string phone, string postalCode, string place, string identificationNumber)
        {
            ProviderId = id;
            Code = code;
            Name = name;
            Description = description;
            Phone = phone;
            PostalCode = postalCode;
            Place = place;
            IdentificationNumber = identificationNumber;
            AggregateId = id;
        }
        public Guid ProviderId {get;set;}
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string PostalCode { get; set; }
        public string Place { get; set; }
        public string IdentificationNumber { get; set; }
    }
}