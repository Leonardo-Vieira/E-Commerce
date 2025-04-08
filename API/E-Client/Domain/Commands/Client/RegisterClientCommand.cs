using System;
using System.Net.Mail;
using E_Client.Data;
using E_Client.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Client.Domain.Commands.Client
{
    public class RegisterClientCommand : ClientCommand
    {
        private readonly DataContext _context;

        public RegisterClientCommand(Guid personId, Person person, string username, string email, string password, byte[] passwordHash, byte[] passwordSalt, DataContext context)
        {
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
            var verifyPerson = _context.Persons.FirstOrDefaultAsync(x => x.IdentificationNumber == Person.IdentificationNumber).Result;
            var verifyClient = _context.Clients.FirstOrDefaultAsync(x => x.Username == Username && x.Email == Email).Result;

            if (verifyPerson == null && verifyClient == null && Person.PostalCode.IndexOf("-") > 0
            && Email.IndexOf("@") > 0 && Email.IndexOf(".") > 0)
            {
                return true;
            }
            return false;
        }
    }
}