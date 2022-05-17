using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Flow_Manager_Models;

namespace Project_Flow_Manager.Controllers
{
    public class DecisionsController : Controller
    {
        private readonly InnovationManagerContext _context;
        private readonly ProjectFlowAdministrationContext _adminContext;

        public DecisionsController(InnovationManagerContext context, ProjectFlowAdministrationContext adminContext)
        {
            _context = context;
            _adminContext = adminContext;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Decisions";
            return View(await _context.ProjectAssessmentReport.Include(i => i.Innovation).ToListAsync());
        }

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
