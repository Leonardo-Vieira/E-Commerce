using E_Client.Domain.Interface;
using E_Client.Models;
using E_Client.Repository;

namespace E_Client.Data.Repository
{
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        private readonly DataContext _context;

        public BrandRepository(DataContext context)
             : base(context)
        {
            _context = context;
        }
    }
}