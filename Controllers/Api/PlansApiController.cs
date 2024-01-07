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
    public class PlansApiController : ControllerBase
    {
        private readonly VoyagerContext _context;

        public PlansApiController(VoyagerContext context)
        {
            _context = context;
        }

        // GET: api/PlansApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plans>>> GetPlans()
        {
            return await _context.Plans.ToListAsync();
        }

        // GET: api/PlansApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Plans>> GetPlans(int id)
        {
            var plans = await _context.Plans.FindAsync(id);

            if (plans == null)
            {
                return NotFound();
            }

            return plans;
        }

        // PUT: api/PlansApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlans(int id, Plans plans)
        {
            if (id != plans.PlanID)
            {
                return BadRequest();
            }

            _context.Entry(plans).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlansExists(id))
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

        // POST: api/PlansApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Plans>> PostPlans(Plans plans)
        {
            _context.Plans.Add(plans);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlans", new { id = plans.PlanID }, plans);
        }

        // DELETE: api/PlansApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlans(int id)
        {
            var plans = await _context.Plans.FindAsync(id);
            if (plans == null)
            {
                return NotFound();
            }

            _context.Plans.Remove(plans);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlansExists(int id)
        {
            return _context.Plans.Any(e => e.PlanID == id);
        }
    }
}
