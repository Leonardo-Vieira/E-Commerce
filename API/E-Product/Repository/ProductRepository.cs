using E_Product.Data;
using E_Product.Models;

namespace E_Product.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
       public ProductRepository(DataContext context)
            : base(context)
        {

        }
    }
}