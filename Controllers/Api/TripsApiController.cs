using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewVoyager.Data;
using NewVoyager.Models;

namespace NewVoyager.Controllers_Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsApiController : ControllerBase
    {
        private readonly VoyagerContext _context;

        public TripsApiController(VoyagerContext context)
        {
            _context = context;
        }

        // GET: api/TripsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trips>>> GetTrips()
        {
            return await _context.Trips.ToListAsync();
        }

        // GET: api/TripsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Trips>> GetTrips(int id)
        {
            var trips = await _context.Trips.FindAsync(id);

            if (trips == null)
            {
                return NotFound();
            }

            return trips;
        }

        // PUT: api/TripsApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrips(int id, Trips trips)
        {
            if (id != trips.TripID)
            {
                return BadRequest();
            }

            _context.Entry(trips).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TripsExists(id))
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

        // POST: api/TripsApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Trips>> PostTrips(Trips trips)
        {
            _context.Trips.Add(trips);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrips", new { id = trips.TripID }, trips);
        }

        // DELETE: api/TripsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrips(int id)
        {
            var trips = await _context.Trips.FindAsync(id);
            if (trips == null)
            {
                return NotFound();
            }

            _context.Trips.Remove(trips);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TripsExists(int id)
        {
            return _context.Trips.Any(e => e.TripID == id);
        }
    }
}
