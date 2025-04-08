using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain_Core.Repository;
using e_order.Data;
using Microsoft.EntityFrameworkCore;

namespace e_order.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public readonly DataContext _context;
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
            _context.Set<TEntity>().Remove(entity);
        }

        public void Update(TEntity obj)
        {
            _context.Set<TEntity>().Update(obj);
        }

        public void Cancel(TEntity obj)
        {
            _context.Set<TEntity>().Update(obj);
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}