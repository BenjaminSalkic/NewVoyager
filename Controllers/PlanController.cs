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
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;





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
                .Where(p => p.AppUserID == user.Id ||  p.Collaborators.Any(c => c.Id == user.Id) )
                .ToListAsync();
                //Dodat da trazi i planove na kojima sidodsan kao kolaborator

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
                .ThenInclude(t => t.Events) // Include events related to each trip
                .Include(p => p.ChatMessages) // Include chat messages related to the plan
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
        public async Task<IActionResult> Create([Bind("PlanID,PlanName,Collaborators")] Plans plans, string CollaboratorID="")
        {
            if (ModelState.IsValid)
            {
                // Get the currently logged-in user
                var user = await _userManager.GetUserAsync(User);

        // Set the Voyager field with the user id
            

                plans.AppUserID = user.Id;
                
                if(CollaboratorID != ""){

                    ApplicationUser collaborator = await _context.Users.FindAsync(CollaboratorID);

                    if(collaborator == null){
                        ModelState.AddModelError("Collaborators", "Collaborator does not exist.");
                        ViewBag.CollaboratorID = "";
                        return View(plans);
                    }else{
                        plans.Collaborators.Add(collaborator);
                    }
                    
                }else{

                    plans.Collaborators.Add(user);
                }   
                
                _context.Add(plans);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(plans);
        }

  // POST: api/Plan/AddCollaborator
        [HttpPost]
        public async Task<IActionResult> AddCollaborator(int planId, string collaboratorId)
        {
            // Get the plan
            Console.WriteLine("******************************************");
            Console.WriteLine(planId);
            var plan = await _context.Plans.FindAsync(planId);
            if (plan == null)
            {
                return NotFound();
            }

            // Get the collaborator
            var collaborator = await _userManager.FindByIdAsync(collaboratorId);
          
            // Add the collaborator to the plan
            try
            {
                plan.Collaborators.Add(collaborator);
                await _context.SaveChangesAsync();
                TempData["Success"] = true;
                TempData["Message"] = "Collaborator added successfully!";
            }
            catch (Exception ex)
            {
                TempData["Success"] = false;
                TempData["Message"] = "Failed to add collaborator: " + ex.Message;
            }
            if (collaborator == null)
            {   
                TempData["Message"] = "Failed to add collaborator: Collaborator does not exist!";
                ViewBag.CollaboratorID = "";
            }else if(plan.Collaborators.Contains(collaborator)){

                TempData["Message"] = "Failed to add collaborator: Collaborator already added!";
                ViewBag.CollaboratorID = "";
            }
            
            //Provjera da li je collaborator vec u listi!!!

            return RedirectToAction("Details", new { id = planId });
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
                
                // POST: Plan/SendMessage
                // POST: Plan/SendMessage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage(int planId, string messageContent)
        {
            var plan = await _context.Plans
                .Include(p => p.ChatMessages)
                .FirstOrDefaultAsync(p => p.PlanID == planId);

            var loggedUser = await _userManager.GetUserAsync(User);

            if (plan == null)
            {
                return NotFound();
            }

            var message = new ChatMessage
            {   
                Content = messageContent,
                Timestamp = DateTime.Now,
                PlanId = planId,
                SenderId = loggedUser.FirstName,
                Sender = loggedUser
            };
            Console.WriteLine("*****************************************************************\n\n\n\n\n***********************************************************************************************************");
            Console.WriteLine(message.Sender.UserName);
            Console.WriteLine(message.SenderId);
            Console.WriteLine("*****************************************************************\n\n\n\n\n***********************************************************************************************************");
            plan.ChatMessages.Add(message);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = planId });
        }
        // POST: Plan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlanID,AppUserID,PlanName,Collaborators")] Plans plans)
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
