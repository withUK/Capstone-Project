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
    public class TagsController : Controller
    {
        private readonly InnovationManagerContext _context;

        public TagsController(InnovationManagerContext context)
        {
            _context = context;
        }

        // GET: Tags
        public async Task<IActionResult> Index()
        {
              return _context.Tag != null ? 
                          View(await _context.Tag.ToListAsync()) :
                          Problem("Entity set 'InnovationManagerContext.Tag'  is null.");
        }

        // GET: Tags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tag == null)
            {
                return NotFound();
            }

            var tag = await _context.Tag
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        // GET: Tags/Create
        public IActionResult Create(string controllerName, int submissionId)
        {
            ViewData["Controller"] = controllerName;
            ViewData["SubmissionId"] = submissionId;
            ViewData["Title"] = "Tags";

            return View();
        }

        // POST: Tags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Value")] Tag tag, string controllerName, int submissionId)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tag);

                switch (controllerName)
                {
                    case "Innovations":
                        var innovation = _context.Innovation.Where(i => i.Id == submissionId)
                            .Include(i => i.Tags)
                            .FirstOrDefault();

                        if (innovation != null)
                        {
                            innovation.Tags.Add(tag);
                            _context.Update(innovation);
                        }
                        break;
                    case "ProjectAssessmentReports":
                        var report = _context.ProjectAssessmentReport.Where(i => i.Id == submissionId)
                            .Include(i => i.Tags)
                            .FirstOrDefault();

                        if (report != null)
                        {
                            report.Tags.Add(tag);
                            _context.Update(report);
                        }
                        break;
                    case "Recommendations":
                        var recommendation = _context.Recommendation.Where(i => i.Id == submissionId)
                            .Include(i => i.Tags)
                            .FirstOrDefault();

                        if (recommendation != null)
                        {
                            recommendation.Tags.Add(tag);
                            _context.Update(recommendation);
                        }
                        break;
                    case "ResourceRequests":
                        var request = _context.ResourceRequest.Where(i => i.Id == submissionId)
                            .Include(i => i.Tags)
                            .FirstOrDefault();

                        if (request != null)
                        {
                            request.Tags.Add(tag);
                            _context.Update(request);
                        }
                        break;
                    default:
                        break;
                }

                await _context.SaveChangesAsync();
                if (controllerName != null)
                {
                    return RedirectToAction(nameof(Details), controllerName, new { id = submissionId });
                }
            }
            return View(tag);
        }

        // GET: Tags/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tag == null)
            {
                return NotFound();
            }

            var tag = await _context.Tag.FindAsync(id);
            if (tag == null)
            {
                return NotFound();
            }
            return View(tag);
        }

        // POST: Tags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Value")] Tag tag)
        {
            if (id != tag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TagExists(tag.Id))
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
            return View(tag);
        }

        // GET: Tags/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tag == null)
            {
                return NotFound();
            }

            var tag = await _context.Tag
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        // POST: Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id, string controller, int submissionId)
        {
            if (_context.Tag == null)
            {
                return Problem("Entity set 'InnovationManagerContext.Tag'  is null.");
            }
            var tag = await _context.Tag.FindAsync(id);
            if (tag != null)
            {
                _context.Tag.Remove(tag);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), controller, new { id = submissionId });
        }

        private bool TagExists(int id)
        {
          return (_context.Tag?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
