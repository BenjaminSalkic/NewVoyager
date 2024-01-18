using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewVoyager.Data;
using NewVoyager.Models;
using System.Security.Claims;
using System.Configuration;


namespace NewVoyager.Controllers
{
    public class PlanController : Controller
    {
        private readonly VoyagerContext _context;

        public PlanController(VoyagerContext context)
        {
            _context = context;
        }

        // GET: Plan
        public async Task<IActionResult> Index()
        {
            return View(await _context.Plans.ToListAsync());
        }

        // GET: Plan/Details/5
        public async Task<IActionResult> Details(int? id)
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
            plans.AppUserID = User.FindFirstValue("Id");

            _context.Add(plans);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // If ModelState is not valid, handle accordingly
        var errors = ModelState.Values.SelectMany(v => v.Errors);
        // Log or debug the errors

        return View(plans);
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
        public async Task<IActionResult> Edit(int id, [Bind("PlanID,PlanName,Attendees")] Plans plans)
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
            var plans = await _context.Plans.FindAsync(id);
            if (plans != null)
            {
                _context.Plans.Remove(plans);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlansExists(int id)
        {
            return _context.Plans.Any(e => e.PlanID == id);
        }
    }
}
