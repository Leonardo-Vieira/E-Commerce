using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Order.Domain.Queries
{
    public interface IGenericQueries<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id);
         Task<IEnumerable<T>> GetAllAsync();
    }
}