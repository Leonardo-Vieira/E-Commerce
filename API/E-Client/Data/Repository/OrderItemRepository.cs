using E_Client.Data;
using E_Client.Models;

namespace E_Client.Repository
{
    public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
    {
        private readonly DataContext _context;

        public OrderItemRepository(DataContext context)
             : base(context)
        {
            _context = context;
        }
    }
}