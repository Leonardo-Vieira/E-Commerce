using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using e_order.Data;
using e_order.Domain.Models;
using E_Order.Domain.Queries;
using Microsoft.EntityFrameworkCore;

namespace E_Order.Domain.Queries.EFQueries {
    public class EFProductQueries : IProductQueries 
    {
        private readonly DataContext _context;
        public EFProductQueries (DataContext context) 
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetAllAsync () 
        {
            return await _context.Products.Include(p => p.Brand).ToListAsync();
        }

        public async Task<Product> GetByIdAsync (Guid id) 
        {
           return await _context.Products.Include(p => p.Brand).FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}