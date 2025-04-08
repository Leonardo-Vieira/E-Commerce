using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using E_Client.Data;
using E_Client.Repository;

namespace E_OrderItem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemRepository _orderItemRepo;

        public OrderItemController(IOrderItemRepository orderItemRepo)
        {
            _orderItemRepo = orderItemRepo;
        }
        // GET: api/OrderItem
        [HttpGet]
        public async Task<IActionResult> GetOrderItems()
        {
            if (ModelState.IsValid)
            {
                var orderItems = await _orderItemRepo.GetAll();
                return Ok(orderItems);
            }
            return BadRequest("Algo correu mal");
        }

        // GET: api/OrderItem/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderItems(Guid id)
        {
            if (ModelState.IsValid)
            {
                var order = await _orderItemRepo.GetById(id);
                return Ok(order);
            }
            return BadRequest("Algo correu mal");
        }
    }
}