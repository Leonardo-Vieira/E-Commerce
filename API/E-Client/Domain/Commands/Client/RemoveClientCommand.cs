using System;
using E_Client.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Client.Domain.Commands.Client
{
    public class RemoveClientCommand : ClientCommand
    {
        private readonly DataContext _context;

        public RemoveClientCommand(Guid id, DataContext context)
        {
            Id = id;
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