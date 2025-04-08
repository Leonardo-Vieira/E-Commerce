using System;
using System.Threading.Tasks;
using AutoMapper;
using Domain_Core.Bus;
using E_Order.Domain.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_order.Controllers
{
    [Authorize]
    [Route ("api/[controller]")]
    [ApiController]
    public class ClientController : Controller {
        private readonly IClientQueries _clientQueries;
        public ClientController (IClientQueries clientQueries) {
            _clientQueries = clientQueries;
        }

        // GET api/client
        [HttpGet]
        public async Task<IActionResult> GetClients () {
           try
           {
               var clients = await _clientQueries.GetAllAsync();
               return Ok(clients);
           }
           catch
           {
               return NotFound();
           }
        }

        // GET api/client/id
        [HttpGet ("{id}")]
        public async Task<IActionResult> GetClientById (Guid id) {
           try
           {
               var client = await _clientQueries.GetByIdAsync(id);
               return Ok(client);
           }
           catch
           {
                return NotFound();   
           }
        }
    }
}