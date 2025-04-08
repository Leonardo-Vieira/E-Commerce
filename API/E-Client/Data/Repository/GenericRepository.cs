using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain_Core.Repository;
using E_Client.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Client.Repository
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DataContext _context;
        public GenericRepository(DataContext dataContext) => _context = dataContext;

        public async Task Create(TEntity obj)
        {
            await _context.Set<TEntity>().AddAsync(obj);
            await _context.SaveChangesAsync();
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
            await _context.SaveChangesAsync();
        }

        public async void Update(TEntity obj)
        {
            _context.Set<TEntity>().Update(obj);
            await _context.SaveChangesAsync();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}