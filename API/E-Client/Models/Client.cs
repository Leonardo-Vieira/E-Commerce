using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;
using Domain_Core.Models;

namespace E_Client.Models
{
    public class Client : Entity
    {
        public Client(Guid id, Guid personId, Person person, string username, string email, string password, byte[] passwordHash, byte[] passwordSalt)
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
        public Client()
        {
        }
        public Guid PersonId { get; set; }
        public Person Person { get; set; }
        [NotMapped]
        public string Password { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        [MinLength(3)]
        [MaxLength(255)]
        public string Username { get; set; }
        public string Email { get; set; }
    }

    public class TokenConfigurations
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Hour { get; set; }
    }
}