using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewVoyager.Data;
using NewVoyager.Models;
using NewVoyager.Filters;

namespace NewVoyager.Controllers_Api
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKeyAuth]
    public class VoyagersApiController : ControllerBase
    {
        private readonly VoyagerContext _context;

        public VoyagersApiController(VoyagerContext context)
        {
            _context = context;
        }

        // GET: api/VoyagersApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Voyager>>> GetVoyagers()
        {
            return await _context.Voyagers.ToListAsync();
        }

        // GET: api/VoyagersApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Voyager>> GetVoyager(int id)
        {
            var voyager = await _context.Voyagers.FindAsync(id);

            if (voyager == null)
            {
                return NotFound();
            }

            return voyager;
        }

        // PUT: api/VoyagersApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVoyager(int id, Voyager voyager)
        {
            if (id != voyager.ID)
            {
                return BadRequest();
            }

            _context.Entry(voyager).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoyagerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/VoyagersApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Voyager>> PostVoyager(Voyager voyager)
        {
            _context.Voyagers.Add(voyager);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVoyager", new { id = voyager.ID }, voyager);
        }

        // DELETE: api/VoyagersApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoyager(int id)
        {
            var voyager = await _context.Voyagers.FindAsync(id);
            if (voyager == null)
            {
                return NotFound();
            }

            _context.Voyagers.Remove(voyager);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VoyagerExists(int id)
        {
            return _context.Voyagers.Any(e => e.ID == id);
        }
    }
}
