using System;
using System.Threading.Tasks;
using AutoMapper;
using Domain_Core.Bus;
using E_Product.Domain.Commands.Brand;
using E_Product.Domain.Queries;
using E_Product.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Product.Controllers
{
    [Authorize]
    [Route ("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase {

        private readonly IMediatorHandler _bus;
        private readonly IBrandQueries _brandQueries;

        public BrandController (IBrandQueries brandQueries, IMediatorHandler bus) {
            _brandQueries = brandQueries;
            _bus = bus;
        }

        // GET api/brand
        [HttpGet]
        public async Task<IActionResult> GetBrands () 
        {
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

        // GET api/brand/5
        [HttpGet ("{id}")]
        public async Task<IActionResult> GetById (Guid id) 
        {
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

        // POST api/brand
        [HttpPost]
        public IActionResult Post (BrandDto brand) {
            if (ModelState.IsValid) {
                //_bus.SendCommand(new CreateBrandCommand(brand.Code, brand.Name, brand.Description));
                _bus.SendCommand (Mapper.Map<CreateBrandCommand> (brand));
                return Ok ();
            }
            return BadRequest ();

        }

        // PUT api/values/5
        [HttpPut ("{id}")]
        public IActionResult Put (Guid id, BrandDto brand) {
            if (ModelState.IsValid) {
                _bus.SendCommand (new UpdateBrandCommand (id, brand.Code, brand.Name, brand.Description));
                return Ok ();
            }
            return BadRequest ();
        }

        // DELETE api/values/5
        [HttpDelete ("{id}")]
        public IActionResult Delete (Guid id) {
            if (ModelState.IsValid) {
                _bus.SendCommand (new RemoveBrandCommand (id));
                return Ok ();
            }
            return BadRequest ();
        }

    }

}