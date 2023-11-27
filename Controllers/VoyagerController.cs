using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewVoyager.Data;
using NewVoyager.Models;
using Microsoft.AspNetCore.Authorization;

namespace NewVoyager.Controllers
{
    public class VoyagerController : Controller
    {
        private readonly VoyagerContext _context;

        public VoyagerController(VoyagerContext context)
        {
            _context = context;
        }

        // GET: Voyager
        public async Task<IActionResult> Index()
        {
            return View(await _context.Voyagers.ToListAsync());
        }

        // GET: Voyager/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voyager = await _context.Voyagers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (voyager == null)
            {
                return NotFound();
            }

            return View(voyager);
        }

        // GET: Voyager/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Voyager/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LastName,FirstName")] Voyager voyager)
        {
            if (ModelState.IsValid)
            {
                _context.Add(voyager);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(voyager);
        }

        // GET: Voyager/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voyager = await _context.Voyagers.FindAsync(id);
            if (voyager == null)
            {
                return NotFound();
            }
            return View(voyager);
        }

        // POST: Voyager/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LastName,FirstName")] Voyager voyager)
        {
            if (id != voyager.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(voyager);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoyagerExists(voyager.ID))
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
            return View(voyager);
        }

        // GET: Voyager/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voyager = await _context.Voyagers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (voyager == null)
            {
                return NotFound();
            }

            return View(voyager);
        }

        // POST: Voyager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var voyager = await _context.Voyagers.FindAsync(id);
            if (voyager != null)
            {
                _context.Voyagers.Remove(voyager);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VoyagerExists(int id)
        {
            return _context.Voyagers.Any(e => e.ID == id);
        }
    }
}
