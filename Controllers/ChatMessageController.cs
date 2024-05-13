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
    public class ChatMessageController : Controller
    {
        private readonly VoyagerContext _context;

        public ChatMessageController(VoyagerContext context)
        {
            _context = context;
        }

        // GET: ChatMessage
        public async Task<IActionResult> Index()
        {
            var voyagerContext = _context.ChatMessages.Include(c => c.Plan).Include(c => c.Sender);
            return View(await voyagerContext.ToListAsync());
        }

        // GET: ChatMessage/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chatMessage = await _context.ChatMessages
                .Include(c => c.Plan)
                .Include(c => c.Sender)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chatMessage == null)
            {
                return NotFound();
            }

            return View(chatMessage);
        }

        // GET: ChatMessage/Create
        public IActionResult Create()
        {
            ViewData["PlanId"] = new SelectList(_context.Plans, "PlanID", "PlanID");
            ViewData["SenderId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: ChatMessage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Content,Timestamp,PlanId,SenderId")] ChatMessage chatMessage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chatMessage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlanId"] = new SelectList(_context.Plans, "PlanID", "PlanID", chatMessage.PlanId);
            ViewData["SenderId"] = new SelectList(_context.Users, "Id", "Id", chatMessage.SenderId);
            return View(chatMessage);
        }

        // GET: ChatMessage/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chatMessage = await _context.ChatMessages.FindAsync(id);
            if (chatMessage == null)
            {
                return NotFound();
            }
            ViewData["PlanId"] = new SelectList(_context.Plans, "PlanID", "PlanID", chatMessage.PlanId);
            ViewData["SenderId"] = new SelectList(_context.Users, "Id", "Id", chatMessage.SenderId);
            return View(chatMessage);
        }

        // POST: ChatMessage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Content,Timestamp,PlanId,SenderId")] ChatMessage chatMessage)
        {
            if (id != chatMessage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chatMessage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChatMessageExists(chatMessage.Id))
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
            ViewData["PlanId"] = new SelectList(_context.Plans, "PlanID", "PlanID", chatMessage.PlanId);
            ViewData["SenderId"] = new SelectList(_context.Users, "Id", "Id", chatMessage.SenderId);
            return View(chatMessage);
        }

        // GET: ChatMessage/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chatMessage = await _context.ChatMessages
                .Include(c => c.Plan)
                .Include(c => c.Sender)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chatMessage == null)
            {
                return NotFound();
            }

            return View(chatMessage);
        }

        // POST: ChatMessage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chatMessage = await _context.ChatMessages.FindAsync(id);
            if (chatMessage != null)
            {
                _context.ChatMessages.Remove(chatMessage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChatMessageExists(int id)
        {
            return _context.ChatMessages.Any(e => e.Id == id);
        }
    }
}
