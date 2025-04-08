using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using e_order.Data;
using e_order.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Order.Domain.Queries.EFQueries {
    public class EFClientQueries : IClientQueries 
    {
        private readonly DataContext _context;
        public EFClientQueries (DataContext context) 
        {
            _context = context;

        }
        public async Task<IEnumerable<Client>> GetAllAsync () 
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Client> GetByIdAsync (Guid id) 
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}