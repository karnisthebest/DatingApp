using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        //dependency injection from Datacontext class and use in this class as _context 
        private readonly DataContext _context;
        public ValuesController(DataContext context)
        {
            _context = context;

        }
        // GET api/values
        //IActionResult returns HTTP responses to the client 
        [HttpGet]
        public async Task<IActionResult> GetValues()
        {
            //get data from database and transform into list and store in values variable 
            var values = await _context.Values.ToListAsync();
            //Ok is HTTP 200 response
            return Ok(values);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            //First => returns First value that found or else return an exeption
            //FirstOrDefault => returns First value or else return the default value if cannot find one 
            var value = await _context.Values.FirstOrDefaultAsync(x => x.Id == id);
            //Ok is HTTP 200 response 
            return Ok(value);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
