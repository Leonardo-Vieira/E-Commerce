using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using e_order.Domain.Models;
using e_order.Domain.ViewModels;
using E_Order.Domain.Models.Dto;

namespace E_Order.Domain.Queries
{
    public interface IOrderQueries : IGenericQueries<Order>
    {
          Task<IEnumerable<Order>> GetByClientId(Guid clientId);
    }
}