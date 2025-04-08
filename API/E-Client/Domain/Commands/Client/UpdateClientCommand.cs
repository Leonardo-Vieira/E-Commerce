using System;
using System.Net.Mail;
using E_Client.Data;
using E_Client.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Client.Domain.Commands.Client
{
    public class UpdateClientCommand : ClientCommand
    {
        private readonly DataContext _context;

        public UpdateClientCommand(Guid id, Guid personId, Person person, string username, string email, string password, byte[] passwordHash, byte[] passwordSalt, DataContext context)
        {
            Id = id;
            PersonId = personId;
            Person = person;
            Username = username;
            Email = email;
            Password = password;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            _context = context;
        }
        public override bool IsValid()
        {
            var verifyClient = _context.Clients.FirstOrDefaultAsync(x => x.Id == Id).Result;
            var verifyPerson = _context.Persons.FirstOrDefaultAsync(x => x.Id == verifyClient.PersonId).Result;

            if (verifyClient != null && verifyPerson != null)
            {
                return true;
            }
            return false;
        }
    }
}