using e_order.Domain.Models;
using e_order.Repository;

namespace e_order.Data.Repository
{
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        public BrandRepository(DataContext dataContext) : base(dataContext)
        {
        }
    }
}