using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using e_order.Data;
using e_order.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace e_order.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(DataContext dataContext) : base(dataContext)
        {

        }

        public async void Cancel(Guid id)
        {
            Order obj = await GetById(id);
            obj.State = false;
            
            _context.Set<Order>().Update(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Order>> GetByClientId(Guid clientId)
        {
            return await (from e in _context.Orders where e.ClientId == clientId select e).ToListAsync();
        }
    }
}