using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewVoyager.Data;
using Microsoft.Data.SqlClient;
using NewVoyager.Models;
using Microsoft.AspNetCore.Identity;

namespace NewVoyager.Controllers
{
    public class PlanController : Controller
    {
        private readonly VoyagerContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public PlanController(VoyagerContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Plan
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized(); // or return NotFound();
            }

            var userPlans = await _context.Plans
                .Where(p => p.AppUserID == user.Id)
                .ToListAsync();

            return View(userPlans);
        }

        // GET: Plan/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plans = await _context.Plans
            .Include(p => p.Trips) // Include the trips in the query
            .ThenInclude(t => t.Events)
            .FirstOrDefaultAsync(m => m.PlanID == id);
            if (plans == null)
            {
                return NotFound();
            }

            return View(plans);
        }

        // GET: Plan/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Plan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlanID,PlanName,Attendees")] Plans plans)
        {
            if (ModelState.IsValid)
            {
                // Get the currently logged-in user
                var user = await _userManager.GetUserAsync(User);

        // Set the Voyager field with the user id
                Console.WriteLine("USER ID: "+user.Id);
                plans.AppUserID = user.Id;
                _context.Add(plans);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(plans);
        }

        // GET: api/Plan/UserPlans
        [HttpGet]
        [Route("api/Plan/UserPlans")]
        public async Task<IActionResult> GetUserPlans()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized(); // or return NotFound();
            }
            var userPlans = await _context.Plans
                .Where(p => p.AppUserID == user.Id)
                .ToListAsync();

            return Ok(userPlans); // Returns the list of plans as JSON
        }

        // GET: Plan/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plans = await _context.Plans.FindAsync(id);
            if (plans == null)
            {
                return NotFound();
            }
            return View(plans);
        }

        // POST: Plan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlanID,AppUserID,PlanName,Attendees")] Plans plans)
        {
            if (id != plans.PlanID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plans);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlansExists(plans.PlanID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(plans);
        }

        // GET: Plan/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plans = await _context.Plans
                .FirstOrDefaultAsync(m => m.PlanID == id);
            if (plans == null)
            {
                return NotFound();
            }

            return View(plans);
        }

        // POST: Plan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plan = await _context.Plans.FindAsync(id);
            if (plan != null)
            {
                _context.Plans.Remove(plan);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PlansExists(int id)
        {
            return _context.Plans.Any(e => e.PlanID == id);
        }
    }
}
