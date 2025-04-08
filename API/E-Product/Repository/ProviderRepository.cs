using E_Product.Data;
using E_Product.Models;

namespace E_Product.Repository
{
    public class ProviderRepository : Repository<Provider>, IProviderRepository
    {
         public ProviderRepository(DataContext context)
            : base(context)
        {

        }
      
    }
}