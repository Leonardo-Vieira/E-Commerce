using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain_Core.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task Create(TEntity obj);
        Task<TEntity> GetById(Guid? id);
        Task<IEnumerable<TEntity>> GetAll();
        void Update(TEntity obj);
        Task Remove(Guid id);
        Task Save();
    }
}