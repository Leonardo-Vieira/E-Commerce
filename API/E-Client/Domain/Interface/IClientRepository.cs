using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using E_Client.Models;

namespace E_Client.Repository
{
    public interface IClientRepository<Client>
    {
        Task<Client> Register(Client entity);
        Task<Client> Update(Client entity);
        Task<Client> GetByClientId(Guid id);
        Task<Person> GetByPersonId(Guid? id);
        void Remove(Guid id);
        Task<IEnumerable<Client>> GetAll();
    }
}