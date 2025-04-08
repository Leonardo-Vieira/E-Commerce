using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using e_order.Data;
using e_order.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Order.Domain.Queries.EFQueries {
    public class EFOrderQueries : IOrderQueries {
        private readonly DataContext _context;
        public EFOrderQueries (DataContext context) 
        {
            _context = context;

        }
        public async Task<IEnumerable<Order>> GetAllAsync () 
        {
            return await _context.Orders
                                .Include(o => o.Client)
                                .Include(o => o.OrderItems)
                                    .ThenInclude(oi => oi.Product)
                                        .ThenInclude(p => p.Brand)
                                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetByClientId (Guid clientId) 
        {
            return await _context.Orders
                                .Where(o => o.ClientId == clientId)
                                .Include(o => o.Client)
                                .Include(o => o.OrderItems)
                                    .ThenInclude(oi => oi.Product)
                                        .ThenInclude(p => p.Brand)
                                
                                .ToListAsync();
        }

        public async Task<Order> GetByIdAsync (Guid id) 
        {
           return await _context.Orders
                                .Include(o => o.Client)
                                .Include(o => o.OrderItems)
                                    .ThenInclude(oi => oi.Product)
                                        .ThenInclude(p => p.Brand)
                                .FirstOrDefaultAsync(o => o.Id == id);
        }
    }
}