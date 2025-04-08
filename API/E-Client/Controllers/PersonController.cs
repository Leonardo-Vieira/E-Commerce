using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using E_Client.Data;
using E_Client.Models;

namespace E_Person.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly DataContext _context;

        public PersonController(DataContext context)
        {
            _context = context;
        }
        // GET: api/Person
        /*[HttpGet]
        public async Task<IActionResult> GetPersons()
        {
            if (ModelState.IsValid)
            {
                var order = await _orderRepo.GetById(id);
                return Ok(order);
            }
            return BadRequest("Algo correu mal");
        }

        // GET: api/Person/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersons(Guid id)
        {
            if (ModelState.IsValid)
            {
                var order = await _orderRepo.GetById(id);
                return Ok(order);
            }
            return BadRequest("Algo correu mal");
        }

        [HttpPost]
        public async Task<IActionResult> Post(Person person)
        {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            return Ok(person);
        }

        // PUT: api/Person/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, Person person)
        {
            var personToEdit = await _context.Persons.FirstOrDefaultAsync(x => x.Id == id);

            personToEdit.Name = person.Name;
            personToEdit.IdentificationNumber = person.IdentificationNumber;
            personToEdit.Place = person.Place;
            personToEdit.PostalCode = person.PostalCode;
            personToEdit.Address = person.Address;
            await _context.SaveChangesAsync();

            return Ok(personToEdit);
        }

        // DELETE: api/Person/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var personToDelete = await _context.Persons.FirstOrDefaultAsync(x => x.Id == id);

            _context.Remove(personToDelete);
            await _context.SaveChangesAsync();

            return Ok(personToDelete);
        } */
    }
}