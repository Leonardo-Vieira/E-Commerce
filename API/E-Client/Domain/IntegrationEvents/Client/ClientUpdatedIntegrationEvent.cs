using System;
using System.Net.Mail;
using Domain_Core.Events;

namespace E_Client.Domain.IntegrationEvents.Client
{
    public class ClientUpdatedIntegrationEvent : IntegrationEvent
    {
        public ClientUpdatedIntegrationEvent(Guid id, Guid personId, E_Client.Models.Person person, string username, string email, string password, byte[] passwordHash, byte[] passwordSalt)
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
        public Guid Id { get; private set; }
        public Guid PersonId { get; private set; }
        public E_Client.Models.Person Person { get; private set; }
        public string Password { get; private set; }
        public byte[] PasswordHash { get; private set; }
        public byte[] PasswordSalt { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
    }
}