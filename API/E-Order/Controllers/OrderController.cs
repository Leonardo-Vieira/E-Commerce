using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain_Core.Bus;
using e_order.Domain.Commands.Order;
using e_order.Domain.ViewModels;
using E_Order.Domain.Models.Dto;
using E_Order.Domain.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_order.Controllers {
    [Authorize]
    [Route ("api/[controller]")]
    [ApiController]
    public class OrderController : Controller {
        private readonly IMediatorHandler _mediator;
        private readonly IOrderQueries _orderQueries;
        private readonly IMapper _mapper;
        public OrderController (IOrderQueries orderQueries, IMediatorHandler mediator, IMapper mapper) {
            _mapper = mapper;
            _orderQueries = orderQueries;
            _mediator = mediator;
        }

        // GET api/order
        [HttpGet]
        public async Task<IActionResult> GetOrders () {
            try 
            {
                var orders = await _orderQueries.GetAllAsync ();

                return Ok ( _mapper.Map<IEnumerable<OrderViewModel>>(orders));
                
            } catch (Exception e) 
            {
                return NotFound (e.Message);
            }
        }

        // GET api/order/5
        [HttpGet ("{id}")]
        public async Task<IActionResult> GetOrder (Guid id) {
            try {
                var order = await _orderQueries.GetByIdAsync (id);

                return Ok ( _mapper.Map<OrderViewModel>(order));
            } catch {
                return NotFound ();
            }

        }

        [HttpGet ("{clientId}/clientId")]
        public async Task<IActionResult> GetOrderByClientId (Guid clientId) {
            try {
                var orders = await _orderQueries.GetByClientId(clientId);

                return Ok ( _mapper.Map<IEnumerable<OrderToClientDto>>(orders));
            } catch (Exception a) {
                return NotFound (a.Message);
            }
        }
        // POST api/order
        [HttpPost]
        public async Task<IActionResult> Post (OrderDto order) {
            if (!ModelState.IsValid) {
                return BadRequest ();
            }

            if (order.OrderItems != null) {
                foreach (var item in order.OrderItems) {
                    if (item.Product == null || 0 > item.Quantity)
                        return BadRequest ();
                }
                var orderCommand = new RegisterNewOrderCommand (order.ClientId, order.OrderItems);
                await _mediator.SendCommand (orderCommand);

                return Ok ();
            }
            return BadRequest ();
        }

        [HttpGet ("{id}/cancel")]
        public async Task<IActionResult> cancel (Guid id) {
            if (!ModelState.IsValid) {
                return BadRequest ();
            }

            if (await _orderQueries.GetByIdAsync (id) == null) {
                return NotFound ("Order not found.");
            }

            var orderCommand = new CancelOrderCommand (id);
            await _mediator.SendCommand (orderCommand);

            return Ok (new {
                responseType = "OrderCancelled",
                    reponse = "Order successfully cancelled",
            });
        }
    }
}