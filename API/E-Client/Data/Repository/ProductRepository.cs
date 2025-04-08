using E_Client.Data;
using E_Client.Models;

namespace E_Client.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context)
             : base(context)
        {
            _context = context;
        }
    }
}