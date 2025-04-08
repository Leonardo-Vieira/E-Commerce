using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain_Core.Bus;
using e_order.Domain.Models;
using E_Order.Domain.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_order.Controllers {
    [Authorize]
    [Route ("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase {
        private readonly IProductQueries _productQueries;
        private readonly IMapper _mapper;
        public ProductController (IProductQueries productQueries, IMapper mapper) {
            _mapper = mapper;
            _productQueries = productQueries;
        }

        // GET api/product
        [HttpGet]
        public async Task<IActionResult> GetProducts () {
            try {
                var products = await _productQueries.GetAllAsync ();
                return Ok (_mapper.Map<IEnumerable<ProductViewModel>>(products));
            } catch {
                return NotFound ();
            }
        }

        // GET api/product/5
        [HttpGet ("{id}")]
        public async Task<IActionResult> GetProduct (Guid id) {
            try {
                var product = await _productQueries.GetByIdAsync (id);
               return Ok (_mapper.Map<ProductViewModel>(product));
            } catch {
                return NotFound ();
            }
        }
    }
}