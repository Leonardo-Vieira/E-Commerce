using E_Client.Domain.Interface;
using E_Client.Models;
using E_Client.Repository;

namespace E_Client.Data.Repository
{
    public class ProductTypeRepository : GenericRepository<ProductType>, IProductTypeRepository
    {
        private readonly DataContext _context;

        public ProductTypeRepository(DataContext context)
             : base(context)
        {
            _context = context;
        }
    }
}