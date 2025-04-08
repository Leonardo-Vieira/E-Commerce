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
    public class BrandController : ControllerBase
    {
        private readonly IBrandRepository _repo;

        public BrandController(IBrandRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetBrand()
        {
            if (ModelState.IsValid)
            {
                var brands = await _repo.GetAll();
                return Ok(brands);
            }
            return BadRequest("Algo correu mal");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (ModelState.IsValid)
            {
                var brand = await _repo.GetById(id);
                return Ok(brand);
            }
            return BadRequest("Algo correu mal");
        }
    }
}