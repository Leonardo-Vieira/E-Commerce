using System;
using Domain_Core.Events;
using E_Client.Models;

namespace E_Client.Domain.Events.Client
{
    public class ClientRegisterEvent : Event
    {
        public ClientRegisterEvent(Guid id, Guid personId, Person person, string username, string email, string password, byte[] passwordHash, byte[] passwordSalt)
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
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public Person Person { get; set; }
        public string Password { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}