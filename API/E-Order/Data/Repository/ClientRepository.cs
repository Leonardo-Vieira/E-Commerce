using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using e_order.Data;
using e_order.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_order.Repository
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(DataContext context)
            : base(context)
        {

        }

      /*   public async Task<IEnumerable<Client>> GetAll()
        {
            var clients = await _context.Set<Client>().ToListAsync();
             for (int i = clients.Count - 1; i >= 0; i--)
            {
                clients[i].Person = _context.Persons.FirstOrDefaultAsync(x => x.Id == clients[i].PersonId).Result;
            } 
            return clients;
        }

        public async Task<Client> GetByClientId(Guid id)
        {
            var client = await _context.Set<Client>().FindAsync(id);

          //  client.Person = _context.Persons.FirstOrDefaultAsync(x => x.Id == client.PersonId).Result;

            return client;
        }

        public async Task<Person> GetByPersonId(Guid? id)
        {
            return await _context.Set<Person>().FindAsync(id);
        }

        public async Task<Client> Register(Client client)
        {
            if (client != null)
            {
                if (client.Password != null)
                {
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash(client.Password, out passwordHash, out passwordSalt);

                    client.PasswordHash = passwordHash;
                    client.PasswordSalt = passwordSalt;

                    await _context.Clients.AddAsync(client);
                    await _context.SaveChangesAsync();
                    return client;
                }
            }
            return await _context.Set<Client>().AddAsync(client);
        }

        public async Task<Client> Update(Client client)
        {
             var clientToEdit = await _context.Clients.FirstOrDefaultAsync(x => x.Id == client.Id);
            var personToEdit = await _context.Persons.FirstOrDefaultAsync(x => x.Id == clientToEdit.PersonId);

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(client.Password, out passwordHash, out passwordSalt);

            if (client.Person != null)
            {
                personToEdit.Name = client.Person.Name;
                personToEdit.IdentificationNumber = client.Person.IdentificationNumber;
                personToEdit.PostalCode = client.Person.PostalCode;
                personToEdit.Place = client.Person.Place;
                personToEdit.Address = client.Person.Address;
            }


            clientToEdit.PersonId = client.PersonId;
            clientToEdit.Email = client.Email;
            clientToEdit.Username = client.Username;
            clientToEdit.PasswordHash = passwordHash;
            clientToEdit.PasswordSalt = passwordSalt;

            _context.Set<Client>().Update(clientToEdit);
            await _context.SaveChangesAsync(); 

            return null;
        }

        public async void Remove(Guid id)
        {
            _context.Set<Client>().Remove(await GetByClientId(id));
            await _context.SaveChangesAsync();
        } */
    } 
}
