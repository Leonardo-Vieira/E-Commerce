using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using e_order.Domain.Models;
using E_Order.Domain.Queries;

namespace E_Order.Domain.Queries
{
  //  public interface IProductQueries : IGenericQueries<ProductViewModel>
    public interface IProductQueries : IGenericQueries<Product>
    {
    }
}