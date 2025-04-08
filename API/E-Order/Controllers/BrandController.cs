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
    public class BrandController : Controller {
        private readonly IBrandQueries _brandQueries;
        public BrandController (IBrandQueries brandQueries) {
            _brandQueries = brandQueries;
        }

        // GET api/brand
        [HttpGet]
        public async Task<IActionResult> GetBrands () {
            try
            {
                var brands = await _brandQueries.GetAllAsync();
                return Ok(brands);
            }
            catch
            {
                return NotFound();
            }
        }

        // GET api/brand/id
        [HttpGet ("{id}")]
        public async Task<IActionResult> GetBrandById (Guid id) {
           try
           {
               var brand = await _brandQueries.GetByIdAsync(id);
               return Ok(brand);
           }
           catch 
           {
             return NotFound();   
           }
        }
    }
}