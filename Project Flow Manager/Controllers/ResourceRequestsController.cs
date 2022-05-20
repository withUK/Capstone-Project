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
    public class ResourceRequestsController : Controller
    {
        private readonly InnovationManagerContext _context;

        public ResourceRequestsController(InnovationManagerContext context)
        {
            _context = context;
        }

        // GET: ResourceRequests
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Resourcing";
            return _context.ResourceRequest != null ? 
                          View(await _context.ResourceRequest.ToListAsync()) :
                          Problem("Entity set 'InnovationManagerContext.ResourceRequest'  is null.");
        }

        // GET: ResourceRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ResourceRequest == null)
            {
                return NotFound();
            }

            var resourceRequest = await _context.ResourceRequest
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resourceRequest == null)
            {
                return NotFound();
            }

            ViewData["Title"] = string.Concat("Details of request id ", resourceRequest.Id);
            return View(resourceRequest);
        }

        // GET: ResourceRequests/Create
        public IActionResult Create()
        {
            ViewData["Title"] = "Add a new request";
            return View();
        }

        // POST: ResourceRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProjectAssessmentId")] ResourceRequest resourceRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resourceRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Title"] = "Add a new request";
            return View(resourceRequest);
        }

        // GET: ResourceRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ResourceRequest == null)
            {
                return NotFound();
            }

            var resourceRequest = await _context.ResourceRequest.FindAsync(id);
            if (resourceRequest == null)
            {
                return NotFound();
            }
            ViewData["Title"] = "Edit an idea";
            return View(resourceRequest);
        }

        // POST: ResourceRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProjectAssessmentId")] ResourceRequest resourceRequest)
        {
            if (id != resourceRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resourceRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResourceRequestExists(resourceRequest.Id))
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
            return View(resourceRequest);
        }

        // GET: ResourceRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ResourceRequest == null)
            {
                return NotFound();
            }

            var resourceRequest = await _context.ResourceRequest
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resourceRequest == null)
            {
                return NotFound();
            }

            ViewData["Title"] = "Confirm Deletion";
            return View(resourceRequest);
        }

        // POST: ResourceRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ResourceRequest == null)
            {
                return Problem("Entity set 'InnovationManagerContext.ResourceRequest'  is null.");
            }
            var resourceRequest = await _context.ResourceRequest.FindAsync(id);
            if (resourceRequest != null)
            {
                _context.ResourceRequest.Remove(resourceRequest);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResourceRequestExists(int id)
        {
          return (_context.ResourceRequest?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
