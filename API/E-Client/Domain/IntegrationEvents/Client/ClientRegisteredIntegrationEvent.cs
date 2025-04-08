using System;
using System.Net.Mail;
using Domain_Core.Events;
using E_Client.Models;

namespace E_Client.Domain.IntegrationEvents.Client
{
    public class ClientRegisteredIntegrationEvent : IntegrationEvent
    {
        public ClientRegisteredIntegrationEvent(Guid id, Guid personId, E_Client.Models.Person person, string username, string email, string password, byte[] passwordHash, byte[] passwordSalt)
        {
            Id = id;
            PersonId = personId;
            Person = person;
            Username = username;
            Email = email;
            Password = password;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }
        public Guid Id { get; protected set; }
        public Guid PersonId { get; protected set; }
        public E_Client.Models.Person Person { get; protected set; }
        public string Password { get; protected set; }
        public byte[] PasswordHash { get; protected set; }
        public byte[] PasswordSalt { get; protected set; }
        public string Username { get; protected set; }
        public string Email { get; protected set; }
    }
}