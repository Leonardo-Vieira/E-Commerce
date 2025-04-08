using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using E_Product.Data;
using E_Product.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Product.Domain.Queries.EFQueries {
    public class EFBrandQueries : IBrandQueries {
        private readonly DataContext _context;
        public EFBrandQueries (DataContext context) {
            _context = context;

        }
        public async Task<IEnumerable<Brand>> GetAllAsync () {
            return await _context.Brands.ToListAsync();
        }

        public async Task<Brand> GetByIdAsync (Guid id) {
           return await _context.Brands.FirstOrDefaultAsync(b => b.Id == id);
        }
    }

}