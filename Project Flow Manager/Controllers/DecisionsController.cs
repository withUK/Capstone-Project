﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Project_Flow_Manager.Controllers
{
    /// <summary>
    /// TODO
    /// </summary>
    public class DecisionsController : Controller
    {
        private readonly InnovationManagerContext _context;
        private readonly ProjectFlowAdministrationContext _adminContext;

        public DecisionsController(InnovationManagerContext context, ProjectFlowAdministrationContext adminContext)
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
            ViewData["Title"] = "Decisions";
            var projectAssessmentReports = _context.ProjectAssessmentReport.Where(p => p.Status.Equals("Eligible for descision")).ToList();
            return View(projectAssessmentReports);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Review(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectAssessmentReport = await _context.ProjectAssessmentReport
                .Include(m => m.Innovation)
                .Include(m => m.Recommendations)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (projectAssessmentReport == null)
            {
                return NotFound();
            }

            ViewData["Title"] = string.Concat("Review project ", projectAssessmentReport.Title);
            return View(projectAssessmentReport);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> SelectRecommendation(int id)
        {
            var choice = _context.Recommendation.Where(r => r.Id == id).Include(r => r.ProjectAssessmentReport).FirstOrDefault();

            var projectAssessmentReport = choice.ProjectAssessmentReport;
            projectAssessmentReport.ChosenRecommendationId = id;

            _context.ProjectAssessmentReport.Update(projectAssessmentReport);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Review), new { id = projectAssessmentReport.Id });
        }
    }
}
