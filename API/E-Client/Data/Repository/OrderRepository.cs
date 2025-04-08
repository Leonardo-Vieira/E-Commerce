using E_Client.Data;
using E_Client.Models;

namespace E_Client.Repository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly DataContext _context;

        public OrderRepository(DataContext context)
             : base(context)
        {
            _context = context;
        }
    }
}