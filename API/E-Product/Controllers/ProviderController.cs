using System;
using System.Threading.Tasks;
using AutoMapper;
using Domain_Core.Bus;
using E_Product.Domain.Commands.Provider;
using E_Product.Domain.Queries;
using E_Product.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Product.Controllers
{
    [Authorize]
    [Route ("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase {

        private readonly IMediatorHandler _bus;
        private readonly IProviderQueries _providerQueries;

        public ProviderController (IMediatorHandler bus, IProviderQueries providerQueries) {
            _providerQueries = providerQueries;
            _bus = bus;
        }
        // GET api/brand
        [HttpGet]
        public async Task<IActionResult> GetProviders () {
           try
           {
               var providers = await _providerQueries.GetAllAsync();
               return Ok(providers);
           }
           catch 
           {
               return NotFound();
           }
        }

        // GET api/brand/5
        [HttpGet ("{id}")]
        public async Task<IActionResult> GetById (Guid id) {
           try
           {
               var provider = await _providerQueries.GetByIdAsync(id);
               return Ok(provider);
           }
           catch
           {
               return NotFound();
           }

        }

        // POST api/brand
        [HttpPost]
        public IActionResult Create (ProviderDto provider) {
            if (ModelState.IsValid) {
                //  _bus.SendCommand(new CreateProviderCommand(provider.Code, 
                //  provider.Name, provider.Description, provider.Phone, provider.PostalCode, provider.Place, provider.IdentificationNumber));
                _bus.SendCommand (Mapper.Map<CreateProviderCommand> (provider));
                return Ok ();
            }
            return BadRequest ();
        }

        // PUT api/values/5
        [HttpPut ("{id}")]
        public IActionResult Update (Guid id, ProviderDto provider) {
            if (ModelState.IsValid) {
                _bus.SendCommand (new UpdateProviderCommand (id, provider.Code,
                    provider.Name, provider.Description, provider.Phone, provider.PostalCode, provider.Place, provider.IdentificationNumber));
                return Ok ();
            }
            return BadRequest ();
        }

        // DELETE api/values/5
        [HttpDelete ("{id}")]
        public IActionResult Delete (Guid id) {
            if (ModelState.IsValid) {
                _bus.SendCommand (new RemoveProviderCommand (id));
                return Ok ("Objeto removido " + id);
            }
            return BadRequest ();
        }
    }
}