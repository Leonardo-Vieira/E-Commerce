using System;
using Domain_Core.Events;

namespace e_order.Domain.IntegrationEvents.Client
{
    public class ClientUpdatedIntegrationEvent : IntegrationEvent
    {
        public ClientUpdatedIntegrationEvent(Guid id, Guid personId, Domain.Models.Person person,
         string username, string email, string password, byte[] passwordHash, byte[] passwordSalt)
        {            
            Id = id;
            PersonId = personId;
            Person = person;
            Username = username;
            Email = email;
            Password = password;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            AggregateId = Id;
        }

        public Guid Id { get; protected set; }
        public Guid PersonId { get; protected set; }
        public Domain.Models.Person Person { get; protected set; }
        public string Username { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public byte[] PasswordSalt { get; protected set; }
        public byte[] PasswordHash { get; protected set; }
    }
}