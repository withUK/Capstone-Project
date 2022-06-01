#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_Flow_Manager.Enums;
using Project_Flow_Manager.Helpers;
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
                .Include(r => r.ProcessSteps)
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
                .Include(r => r.ProcessSteps)
                .Include(r => r.Teams)
                .Include(r => r.Technologies)
                .Include(r => r.Comments)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (recommendation == null)
            {
                return NotFound();
            }

            ViewData["Title"] = string.Concat("Details of recommendation : ", recommendation.Id);
            ViewData["ControllerName"] = "Recommendations";
            ViewData["SubmissionId"] = recommendation.Id;
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
        public async Task<IActionResult> Create([Bind("Id,Details,Effort.Amount,Effort.Measure,CreatedBy,CreatedDate,Status")] Recommendation recommendation, Effort effort)
        {
            if (ModelState.IsValid)
            {
                recommendation.Effort = effort;
                recommendation.Status = EnumHelper.GetDisplayName(StatusEnum.Submitted);
                recommendation.Created = DateTime.Now;
                recommendation.CreatedBy = User.Identity.Name == null ? "Unknown User" : User.Identity.Name;

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
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recommendation = await _context.Recommendation.Include(r => r.Effort).Where(r => r.Id == id).FirstAsync();
            if (recommendation == null)
            {
                return NotFound();
            }

            ViewData["Title"] = "Edit a recommendation";
            ViewBag.EffortMeasures = _adminContext.EffortMeasure.Any() ? _adminContext.EffortMeasure.Select(s => s.Value).ToList() : new List<string>();
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Details,Effort.Id,Effort.Amount,Effort.Measure,CreatedBy,CreatedDate,Status")] Recommendation recommendation, Effort effort)
        {
            if (id != recommendation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Effort.Update(effort);
                    _context.Recommendation.Update(recommendation);
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
            ViewBag.EffortMeasures = _adminContext.EffortMeasure.Any() ? _adminContext.EffortMeasure.Select(s => s.Value).ToList() : new List<string>();
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

            var report = _context.ProjectAssessmentReport.Where(r => r.Recommendations.Contains(recommendation)).FirstOrDefault();
            if (report.Recommendations.Count < 2)
            {
                report.Status = EnumHelper.GetDisplayName(StatusEnum.AwaitingFurtherRecommendations);
            }
            else
            {
                report.Status = EnumHelper.GetDisplayName(StatusEnum.EligibleForDecision);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="recommendationId"></param>
        /// <returns></returns>
        public async Task<IActionResult> AddProcessStep(int recommendationId)
        {
            
            ViewData["Title"] = "Add a process step";
            ViewData["RecommendationId"] = recommendationId;
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="processStep"></param>
        /// <param name="recommendationId"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProcessStep([Bind("Value,OrderPosition")] ProcessStep processStep, int recommendationId)
        {
            var recommendation = _context.Recommendation.Where(i => i.Id == recommendationId).Include(i => i.ProcessSteps).FirstOrDefault();

            if (recommendation == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (recommendation.ProcessSteps == null)
                {
                    recommendation.ProcessSteps = new List<ProcessStep>();
                }

                recommendation.ProcessSteps.Add(processStep);
                _context.Recommendation.Update(recommendation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = recommendation.Id });
            }

            ViewData["Title"] = string.Concat("Process Step for recommendation Id : ", recommendation.Id);
            ViewData["RecommendationId"] = recommendation.Id;
            return View(recommendation.Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="recommendationId"></param>
        /// <returns></returns>
        public async Task<IActionResult> EditProcessStep(int id, int recommendationId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var step = await _context.ProcessStep.FindAsync(id);
            if (step == null)
            {
                return NotFound();
            }

            ViewData["Title"] = "Edit a process step";
            ViewData["RecommendationId"] = recommendationId;
            return View(step);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="processStep"></param>
        /// <param name="recommendationId"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProcessStep([Bind("Id,Value,OrderPosition")] ProcessStep processStep, int recommendationId)
        {
            if (ModelState.IsValid)
            {
                _context.ProcessStep.Update(processStep);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = recommendationId });
            }

            ViewData["Title"] = "Edit a process step";
            ViewData["RecommendationId"] = recommendationId;
            return View(recommendationId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="innovationId"></param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteProcessStep(int? id, int recommendationId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var processStep = await _context.ProcessStep.FirstOrDefaultAsync(m => m.Id == id);

            if (processStep == null)
            {
                return NotFound();
            }

            ViewData["Title"] = "Confirm Deletion";
            ViewData["RecommendationId"] = recommendationId;
            return View(processStep);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="recommendationId"></param>
        /// <returns></returns>
        [HttpPost, ActionName("DeleteProcessStep")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProcessStepConfirmed(int id, int recommendationId)
        {
            var recommendation = _context.Recommendation.Where(i => i.Id == recommendationId).Include(i => i.ProcessSteps).FirstOrDefault();
            ViewData["Title"] = string.Concat("Process Steps for ", recommendation.Id);

            var processStep = await _context.ProcessStep.FirstOrDefaultAsync(m => m.Id == id);
            _context.ProcessStep.Remove(processStep);
            await _context.SaveChangesAsync();
            ViewData["ActionMessage"] = "Process step has been removed";
            ViewData["ActionResult"] = "success";

            return RedirectToAction(nameof(Details), new { id = recommendationId });
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
