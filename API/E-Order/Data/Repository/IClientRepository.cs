using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain_Core.Repository;
using e_order.Domain.Models;

namespace e_order.Repository
{
    public interface IClientRepository : IRepository<Client>
    {
      /*   Task<Client> Register(Client entity);
        Task<Client> Update(Client entity);
        Task<Client> GetByClientId(Guid id);
        Task<Person> GetByPersonId(Guid? id);
        void Remove(Guid id);
        Task<IEnumerable<Client>> GetAll(); */
    }
}