using E_Product.Data;
using E_Product.Models;

namespace E_Product.Repository
{
    public class ProductTypeRepository : Repository<ProductType>, IProductTypeRepository
    {
      public ProductTypeRepository(DataContext context)
            : base(context)
        {

        }
    }
}