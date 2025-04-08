using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using E_Client.Data;
using System;
using Microsoft.Extensions.Configuration;
using E_Client.Repository;
using Domain_Core.Bus;
using E_Client.Domain.Interface;

namespace E_Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductTypeRepository _repo;

        public ProductTypeController(IProductTypeRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductTypes()
        {
            if (ModelState.IsValid)
            {
                var productTypes = await _repo.GetAll();
                return Ok(productTypes);
            }
            return BadRequest("Algo correu mal");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (ModelState.IsValid)
            {
                var productType = await _repo.GetById(id);
                return Ok(productType);
            }
            return BadRequest("Algo correu mal");
        }
    }
}