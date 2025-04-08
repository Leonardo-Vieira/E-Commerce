using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using e_order.Data;
using e_order.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace e_order.Repository {
    public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository {
        public OrderItemRepository (DataContext dataContext) : base (dataContext) {

        }

        public IList<OrderItem> GetByOrderId (Guid id) {
             return (from e in _context.OrderItems where e.OrderId == id select e).ToList();
        }
    }
}