using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using E_Product.Data;
using E_Product.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Product.Domain.Queries.EFQueries {
    public class EFProductTypeQueries : IProductTypeQueries {
        private readonly DataContext _context;
        public EFProductTypeQueries (DataContext context) {
            _context = context;

        }
        public async Task<IEnumerable<ProductType>> GetAllAsync () {
           return await _context.ProductTypes.ToListAsync();
        }

        public async Task<ProductType> GetByIdAsync (Guid id) {
            return await _context.ProductTypes.FirstOrDefaultAsync(pt => pt.Id == id);
        }
    }
}