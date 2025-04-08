using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain_Core.Repository;
using e_order.Domain.Models;

namespace e_order.Repository
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
         IList<OrderItem> GetByOrderId(Guid id);
    }
}