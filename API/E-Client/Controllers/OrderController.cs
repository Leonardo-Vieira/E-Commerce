using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Configuration;
using E_Client.Repository;
using Domain_Core.Bus;

namespace E_Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepo;

        public OrderController(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrder()
        {
            if (ModelState.IsValid)
            {
                var orders = await _orderRepo.GetAll();
                return Ok(orders);
            }
            return BadRequest("Algo correu mal");

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (ModelState.IsValid)
            {
                var order = await _orderRepo.GetById(id);
                return Ok(order);
            }
            return BadRequest("Algo correu mal");
        }
    }
}