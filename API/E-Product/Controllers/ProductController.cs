using System;
using System.Threading.Tasks;
using AutoMapper;
using Domain_Core.Bus;
using E_Product.Domain.Commands.Product;
using E_Product.Domain.Queries;
using E_Product.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Product.Controllers
{
    [Authorize]
    [Route ("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase {

        private readonly IMediatorHandler _bus;
        private readonly IProductQueries _productQueries;

        public ProductController (IMediatorHandler bus, IProductQueries productQueries) {
            _productQueries = productQueries ?? throw new ArgumentNullException(nameof(productQueries));
            _bus = bus;

        }
        // GET api/brand
        [HttpGet]
        public async Task<IActionResult> GetProducts () {
            try
            {
                var products = await _productQueries.GetAllAsync();
                return Ok (products);
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
                var product = await _productQueries.GetByIdAsync(id);
                return Ok (product);
            }
            catch
            {
                return NotFound();
            }
         
        }

        // POST api/brand
        [HttpPost]
        public IActionResult Post (ProductDto p) {
            if (ModelState.IsValid) {
                //var product = Mapper.Map<Product>(p);

                if (p.Brand.Id != Guid.Empty)

                    _bus.SendCommand (Mapper.Map<CreateProductCommand> (p));
                return Ok ();
            }
            return BadRequest ();
        }

        // PUT api/values/5
        [HttpPut ("{id}")]
        public IActionResult Put (Guid id, ProductDto p) {
            if (ModelState.IsValid) {
                _bus.SendCommand (new UpdateProductCommand (id, p.Code, p.Name, p.Description, p.Status, p.Price, p.Stock, p.Provider.Id, p.Brand.Id, p.ProductType.Id));
                return Ok ();
            }
            return BadRequest ();
        }

        // DELETE api/values/5
        [HttpDelete ("{id}")]
        public IActionResult Delete (Guid id) {
            if (ModelState.IsValid && id != null) {
                _bus.SendCommand (new RemoveProductCommand (id));
                return Ok ("Objeto removido com sucesso.");
            }
            return BadRequest ();
        }
    }
}