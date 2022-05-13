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
    public class RecommendationsController : Controller
    {
        private readonly InnovationManagerContext _context;
        private readonly ProjectFlowAdministrationContext _adminContext;

        public RecommendationsController(InnovationManagerContext context, ProjectFlowAdministrationContext adminContext)
        {
            _context = context;
            _adminContext = adminContext;
        }

        // GET: Recommendations
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Recommendations";
            ViewData["AssessmentCount"] = _context.ProjectAssessmentReport.Count();
            return View(await _context.Recommendation
                .Include(r => r.Effort)
                .Include(r => r.ProjectAssessmentReport)
                .ToListAsync());
        }

        // GET: Recommendations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recommendation = await _context.Recommendation
                .Include(_r => _r.Effort)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recommendation == null)
            {
                return NotFound();
            }

            ViewData["Title"] = string.Concat("Details of recommendation : ", recommendation.Id);
            return View(recommendation);
        }

        // GET: Recommendations/Create
        public IActionResult Create()
        {
            ViewData["Title"] = "Add a new recommendation";
            ViewData["AssessmentId"] = new SelectList(_context.ProjectAssessmentReport, "Id", "Title");
            ViewBag.EffortMeasures = _adminContext.EffortMeasure.Any() ? _adminContext.EffortMeasure.Select(s => s.Value).ToList() : new List<string>();
            return View();
        }

        // POST: Recommendations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Details,CreatedBy,CreatedDate")] Recommendation recommendation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recommendation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["Title"] = "Add a new recommendation";
            ViewData["AssessmentId"] = new SelectList(_context.ProjectAssessmentReport, "Id", "Title");
            ViewBag.EffortMeasures = _adminContext.EffortMeasure.Any() ? _adminContext.EffortMeasure.Select(s => s.Value).ToList() : new List<string>();
            return View(recommendation);
        }

        // GET: Recommendations/Edit/5
        public async Task<IActionResult> Edit(int? id, int projectAssessmentReportId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recommendation = await _context.Recommendation.FindAsync(id);
            if (recommendation == null)
            {
                return NotFound();
            }

            ViewData["Title"] = "Edit a recommendation";
            ViewData["ProjectAssessmentReportId"] = projectAssessmentReportId;
            return View(recommendation);
        }

        // POST: Recommendations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Details,CreatedBy,CreatedDate")] Recommendation recommendation)
        {
            if (id != recommendation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recommendation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecommendationExists(recommendation.Id))
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

            ViewData["Title"] = "Edit a recommendation";

            return View(recommendation);
        }

        // GET: Recommendations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recommendation = await _context.Recommendation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recommendation == null)
            {
                return NotFound();
            }

            ViewData["Title"] = "Confirm Deletion";
            return View(recommendation);
        }

        // POST: Recommendations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recommendation = await _context.Recommendation.FindAsync(id);
            _context.Recommendation.Remove(recommendation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecommendationExists(int id)
        {
            return _context.Recommendation.Any(e => e.Id == id);
        }
    }
}
