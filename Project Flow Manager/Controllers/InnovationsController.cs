#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_Flow_Manager_Models;

namespace Project_Flow_Manager.Controllers
{
    public class InnovationsController : Controller
    {
        private readonly InnovationManagerContext _context;
        private readonly ProjectFlowAdministrationContext _adminContext;

        public InnovationsController(InnovationManagerContext context, ProjectFlowAdministrationContext adminContext)
        {
            _context = context;
            _adminContext = adminContext;
        }

        // GET: Innovations
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Innovation Submissions";
            return View(await _context.Innovation.ToListAsync());
        }

        // GET: Innovations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var innovation = await _context.Innovation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (innovation == null)
            {
                return NotFound();
            }

            return View(innovation);
        }

        // GET: Innovations/Create
        public IActionResult Create()
        {
            ViewData["Title"] = "Add a new idea";
            ViewBag.StatusOptions = _adminContext.Status.Select(s => s.Value).ToList();
            return View();
        }

        // POST: Innovations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,ProcessDuration,NumberOfPeopleIncluded,ProcessType,Status,RequiredDate")] Innovation innovation)
        {
            innovation.SubmittedDate = DateTime.Now;
            innovation.SubmittedBy = User.Identity.Name == null ? "Unknown User" : User.Identity.Name;

            if (ModelState.IsValid)
            {
                _context.Add(innovation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Title"] = "Add a new idea";
            ViewBag.StatusOptions = _adminContext.Status.Select(s => s.Value).ToList();
            return View(innovation);
        }

        // GET: Innovations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var innovation = await _context.Innovation.FindAsync(id);
            if (innovation == null)
            {
                return NotFound();
            }

            ViewData["Title"] = "Edit an idea";
            ViewBag.StatusOptions = _adminContext.Status.Select(s => s.Value).ToList();
            return View(innovation);
        }

        // POST: Innovations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,SubmittedDate,SubmittedBy,ProcessDuration,NumberOfPeopleIncluded,ProcessType,Status,RequiredDate")] Innovation innovation)
        {
            if (id != innovation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(innovation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InnovationExists(innovation.Id))
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

            ViewData["Title"] = "Edit an idea";
            ViewBag.StatusOptions = _adminContext.Status.Select(s => s.Value).ToList();
            
            return View(innovation);
        }

        // GET: Innovations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var innovation = await _context.Innovation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (innovation == null)
            {
                return NotFound();
            }

            ViewData["Title"] = "Confirm Deletion";
            return View(innovation);
        }

        // POST: Innovations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var innovation = await _context.Innovation.FindAsync(id);
            _context.Innovation.Remove(innovation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InnovationExists(int id)
        {
            return _context.Innovation.Any(e => e.Id == id);
        }
    }
}
