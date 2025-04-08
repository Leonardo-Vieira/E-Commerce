using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using E_Client.Data;
using System;
using Microsoft.Extensions.Configuration;
using E_Client.Repository;
using Domain_Core.Bus;

namespace E_Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepo;

        public ProductController(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            if (ModelState.IsValid)
            {
                var products = await _productRepo.GetAll();
                return Ok(products);
            }
            return BadRequest("Algo correu mal");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (ModelState.IsValid)
            {
                var product = await _productRepo.GetById(id);
                return Ok(product);
            }
            return BadRequest("Algo correu mal");
        }
    }
}