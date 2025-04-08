using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain_Core.Repository;
using E_Client.Models;

namespace E_Client.Repository
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {

    }
}