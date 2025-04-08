using E_Product.Data;
using E_Product.Models;

namespace E_Product.Repository
{
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
       public BrandRepository(DataContext context)
            : base(context)
        {

        }

    }
}