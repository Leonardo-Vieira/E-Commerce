using System;
using System.Net.Mail;
using Domain_Core.Commands;
using E_Client.Models;

namespace E_Client.Domain.Commands.Client
{
    public abstract class ClientCommand : Command
    {
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