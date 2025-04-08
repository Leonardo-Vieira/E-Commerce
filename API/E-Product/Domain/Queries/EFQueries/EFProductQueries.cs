using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using E_Product.Data;
using E_Product.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Product.Domain.Queries.EFQueries {
    public class EFProductQueries : IProductQueries {
        private readonly DataContext _context;
        public EFProductQueries (DataContext context) {
            _context = context;

        }
        public async Task<IEnumerable<Product>> GetAllAsync () {
            return await _context.Products
                                        .Include(product => product.Brand)
                                        .Include(product => product.Provider)
                                        .Include(product => product.ProductType)
                                        .ToListAsync();
        }

        public async Task<Product> GetByIdAsync (Guid id) {
            return await _context.Products
                                    .Include(product => product.Brand)
                                    .Include(product => product.Provider)
                                    .Include(product => product.ProductType)
                                    .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}