using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using E_Product.Data;
using E_Product.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Product.Domain.Queries.EFQueries {
    public class EFProviderQueries : IProviderQueries {
        private readonly DataContext _context;
        public EFProviderQueries (DataContext context) {
            _context = context;

        }
        public async Task<IEnumerable<Provider>> GetAllAsync () {
            return await _context.Provider.ToListAsync();
        }

        public async Task<Provider> GetByIdAsync (Guid id) {
            return await _context.Provider.FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}