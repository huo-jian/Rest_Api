using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TestTechnique_EONIX_Api.Models;

namespace TestTechnique_EONIX_Api.Controllers
{
    [Route("person")]
    public class PersonController : Controller
    {
        private readonly CurrentContext _context;

        public PersonController(CurrentContext context)
        {
            _context = context;
        }


        // /Person?firstname=erer&lastname=mich
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] String firstname, String lastname)
        {
           
            List<Person> personList = await _context.Persons.ToListAsync();

            if (String.IsNullOrEmpty(firstname) && String.IsNullOrEmpty(lastname))
                return View(await _context.Persons.ToListAsync());
            else
                return View(personList.FindAll(
                (p =>
                    (String.IsNullOrEmpty(firstname) && p.Lastname.ToLower().Contains(lastname.ToLower()))
                    || (String.IsNullOrEmpty(lastname) && p.Firstname.ToLower().Contains(firstname.ToLower()))
                    || (!String.IsNullOrEmpty(firstname) && !String.IsNullOrEmpty(lastname) && p.Firstname.ToLower().Contains(firstname.ToLower()) && p.Lastname.ToLower().Contains(lastname.ToLower()))

                     )));


        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons
                .FirstOrDefaultAsync(m => m.PersonKey == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Person person)
        {
                        
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid? id, [FromBody] Person person)
        { 
            if (id == null)
            {
                return NotFound();
            }
            var personToUpdate = await _context.Persons.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            if(!String.IsNullOrEmpty(person.Firstname))
                personToUpdate.Firstname = person.Firstname;

            if(!String.IsNullOrEmpty(person.Lastname))
                personToUpdate.Lastname = person.Lastname;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

         
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var person = await _context.Persons.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
            return View(person);
        }
        
        private bool PersonExists(Guid id)
        {
            return _context.Persons.Any(e => e.PersonKey == id);
        }
    }
}
