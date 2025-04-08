using System;
using System.Threading.Tasks;
using AutoMapper;
using Domain_Core.Bus;
using E_Product.Domain.Commands.ProductType;
using E_Product.Domain.Queries;
using E_Product.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Product.Controllers {
    [Authorize]
    [Route ("api/[controller]")]
    [ApiController]
    public class ProductTypeController : ControllerBase {

        private readonly IMediatorHandler _bus;
        private readonly IProductTypeQueries _productTypeQueries;

        public ProductTypeController (IMediatorHandler bus, IProductTypeQueries productTypeQueries) {
            _productTypeQueries = productTypeQueries;
            _bus = bus;
        }
        // GET api/brand
        [HttpGet]
        public async Task<IActionResult> GetProductTypes () 
        {
            try
            {
                var productTypes = await _productTypeQueries.GetAllAsync();
                return Ok(productTypes);
            }
            catch 
            {
                return NotFound();
            }
        }

        // GET api/brand/5
        [HttpGet ("{id}")]
        public async Task<IActionResult> GetById (Guid id) 
        {
            try
            {
                var productType = await _productTypeQueries.GetByIdAsync(id);
                return Ok(productType);
            }
            catch 
            {
                return NotFound();
            }
        }

        // POST api/brand
        [HttpPost]
        public IActionResult Post (ProductTypeDto productType) {
            if (ModelState.IsValid) {
                //_bus.SendCommand(new CreateProductTypeCommand(productType.Code, productType.Name, productType.Description));
                _bus.SendCommand (Mapper.Map<CreateProductTypeCommand> (productType));
                return Ok ();
            }
            return BadRequest ();

        }

        // PUT api/values/5
        [HttpPut ("{id}")]
        public IActionResult Put (Guid id, ProductTypeDto productType) {
            if (ModelState.IsValid) {
                _bus.SendCommand (new UpdateProductTypeCommand (id, productType.Code, productType.Name, productType.Description));
                return Ok ();
            }
            return BadRequest ();
        }

        // DELETE api/values/5
        [HttpDelete ("{id}")]
        public IActionResult Delete (Guid id) {
            if (ModelState.IsValid && id != null) {
                _bus.SendCommand (new RemoveProductTypeCommand (id));
                return Ok ("Objecto removido com sucesso.");
            }
            return BadRequest ();
        }
    }
}