
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using E_Client.Data;
using E_Client.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Client.Repository
{
    public class ClientRepository<TEntity> : IClientRepository<Client>
    {
        private readonly DataContext _context;

        public ClientRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Client>> GetAll()
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

            client.Person = _context.Persons.FirstOrDefaultAsync(x => x.Id == client.PersonId).Result;

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
            return null;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<Client> Update(Client client)
        {
            var clientToEdit = await _context.Clients.FirstOrDefaultAsync(x => x.Id == client.Id);
            var personToEdit = await _context.Persons.FirstOrDefaultAsync(x => x.Id == clientToEdit.PersonId);

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(client.Password, out passwordHash, out passwordSalt);

            personToEdit.Name = client.Person.Name;
            personToEdit.IdentificationNumber = client.Person.IdentificationNumber;
            personToEdit.PostalCode = client.Person.PostalCode;
            personToEdit.Place = client.Person.Place;
            personToEdit.Address = client.Person.Address;

            clientToEdit.Email = client.Email;
            clientToEdit.Username = client.Username;
            clientToEdit.PasswordHash = passwordHash;
            clientToEdit.PasswordSalt = passwordSalt;
            await _context.SaveChangesAsync();

            return clientToEdit;
        }

        public async void Remove(Guid id)
        {
            _context.Set<Client>().Remove(await GetByClientId(id));
            await _context.SaveChangesAsync();
        }
    }
}