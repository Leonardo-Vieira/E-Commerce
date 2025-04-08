using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain_Core.Repository;
using E_Product.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Product.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DataContext _context;
        public Repository(DataContext dataContext) => _context = dataContext;

        public async Task Create(TEntity obj)
        {
            await _context.Set<TEntity>().AddAsync(obj);
          
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetById(Guid? id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task Remove(Guid id)
        {
            var entity = await GetById(id);
            if(entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
            }
        }

        public async Task Save()
        {
             await _context.SaveChangesAsync();
        }

        public void Update(TEntity obj)
        {
            _context.Set<TEntity>().Update(obj);
        }
    }
}