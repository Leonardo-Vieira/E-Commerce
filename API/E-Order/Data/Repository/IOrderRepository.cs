using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain_Core.Repository;
using e_order.Domain.Models;

namespace e_order.Repository
{
    public interface IOrderRepository : IRepository<Order>
    {
        void Cancel(Guid id);

        Task<List<Order>> GetByClientId (Guid clientId);
    }
}