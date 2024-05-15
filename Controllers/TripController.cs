using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewVoyager.Data;
using NewVoyager.Models;

namespace NewVoyager.Controllers
{
    public class TripController : Controller
    {
        private readonly VoyagerContext _context;

        public TripController(VoyagerContext context)
        {
            _context = context;
        }

        // GET: Trip
        public async Task<IActionResult> Index()
        {
            return View(await _context.Trips.ToListAsync());
        }

        // GET: Trip/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trips = await _context.Trips
                .Include(t => t.Events) // Include the events in the query
                .FirstOrDefaultAsync(m => m.TripID == id);
            if (trips == null)
            {
                return NotFound();
            }

            return View(trips);
        }

        // GET: Trip/Create
        public IActionResult Create(int planId)
        {
            var trip = new Trips { PlanID = planId }; // Set PlanID for the new trip
            return View(trip);
        }

        // POST: Trip/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TripID,TripName,DateFrom,DateTo,PlanID")] Trips trips)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(trips);
                    await _context.SaveChangesAsync();

                    TempData["TripCreationSuccess"] = "Trip created successfully!";
                }
                catch (Exception ex)
                {
                    TempData["TripCreationError"] = "Error creating trip: " + ex.Message;
                }

                return RedirectToAction(nameof(Details), "Plan", new { id = trips.PlanID }); // Redirect to the details view of the plan
            }
            return View(trips);
        }

        // GET: Trip/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trips = await _context.Trips.FindAsync(id);
            if (trips == null)
            {
                return NotFound();
            }
            return View(trips);
        }

        // POST: Trip/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TripID,PlanID,TripName,DateFrom,DateTo")] Trips trips)
        {
            if (id != trips.TripID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trips);
                    await _context.SaveChangesAsync();

                    TempData["TripEditSuccess"] = "Trip edited successfully!";
                }       
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!TripsExists(trips.TripID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        TempData["TripEditError"] = "Error editing trip: " + ex.Message;

                    }
                    
                }
            }
            return RedirectToAction("Details", "Plan", new { id = trips.PlanID });
        }

        // GET: Trip/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trips = await _context.Trips
                .FirstOrDefaultAsync(m => m.TripID == id);
            if (trips == null)
            {
                return NotFound();
            }

            return View(trips);
        }

        // POST: Trip/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trip = await _context.Trips.FindAsync(id);
            var planId = trip.PlanID;

            try
            {
                _context.Trips.Remove(trip);
                await _context.SaveChangesAsync();

                TempData["TripDeletionSuccess"] = "Trip deleted successfully!";
            }
            catch (Exception ex)
            {
                TempData["TripDeletionError"] = "Error deleting trip: " + ex.Message;
            }

            return RedirectToAction("Details", "Plan", new { id = planId });
        }

        private bool TripsExists(int id)
        {
            return _context.Trips.Any(e => e.TripID == id);
        }
    }
}
