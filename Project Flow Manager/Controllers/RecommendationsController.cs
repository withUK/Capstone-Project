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
    /// <summary>
    /// TODO
    /// </summary>
    public class RecommendationsController : Controller
    {
        private readonly InnovationManagerContext _context;
        private readonly ProjectFlowAdministrationContext _adminContext;

        public RecommendationsController(InnovationManagerContext context, ProjectFlowAdministrationContext adminContext)
        {
            _context = context;
            _adminContext = adminContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Recommendations";
            ViewData["AssessmentCount"] = _context.ProjectAssessmentReport.Count();
            return View(await _context.Recommendation
                .Include(r => r.Effort)
                .Include(r => r.ProjectAssessmentReport)
                .ToListAsync());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            ViewData["Title"] = "Add a new recommendation";
            ViewData["AssessmentId"] = new SelectList(_context.ProjectAssessmentReport, "Id", "Title");
            ViewBag.EffortMeasures = _adminContext.EffortMeasure.Any() ? _adminContext.EffortMeasure.Select(s => s.Value).ToList() : new List<string>();
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recommendation"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="projectAssessmentReportId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="recommendation"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recommendation = await _context.Recommendation.FindAsync(id);
            _context.Recommendation.Remove(recommendation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool RecommendationExists(int id)
        {
            return _context.Recommendation.Any(e => e.Id == id);
        }
    }
}
