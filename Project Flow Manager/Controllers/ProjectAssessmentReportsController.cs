#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_Flow_Manager.Enums;
using Project_Flow_Manager.Helpers;
using Project_Flow_Manager_Models;
using ProjectFlowManagerModels;

namespace Project_Flow_Manager.Controllers
{
    /// <summary>
    /// TODO
    /// </summary>
    public class ProjectAssessmentReportsController : Controller
    {
        private readonly InnovationManagerContext _context;
        private readonly ProjectFlowAdministrationContext _adminContext;

        public ProjectAssessmentReportsController(InnovationManagerContext context, ProjectFlowAdministrationContext adminContext)
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
            ViewData["Title"] = "Assessments";
            ViewData["ProjectAssessmentReportCount"] = _context.ProjectAssessmentReport
                .Where(i => i.Status.Equals(EnumHelper.GetDisplayName(StatusEnum.New)))
                .Count();

            return View(DatabaseHelper.GetAssessmentReports(_context));
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

            var projectAssessmentReport = DatabaseHelper.GetProjectAssessmentReport(id, _context);

            if (projectAssessmentReport == null)
            {
                return NotFound();
            }

            ViewData["Title"] = string.Concat("Details of ", projectAssessmentReport.Title);
            ViewData["ControllerName"] = "ProjectAssessmentReports";
            ViewData["SubmissionId"] = projectAssessmentReport.Id;
            return View(projectAssessmentReport);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            ViewData["InnovationId"] = new SelectList(_context.Innovation, "Id", "Description");
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectAssessmentReport"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Status,InnovationId")] ProjectAssessmentReport projectAssessmentReport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projectAssessmentReport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InnovationId"] = new SelectList(_context.Innovation, "Id", "Description", projectAssessmentReport.InnovationId);
            return View(projectAssessmentReport);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectAssessmentReport = DatabaseHelper.GetProjectAssessmentReport(id, _context);

            if (projectAssessmentReport == null)
            {
                return NotFound();
            }
            return View(projectAssessmentReport);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="projectAssessmentReport"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Status,InnovationId")] ProjectAssessmentReport projectAssessmentReport)
        {
            if (id != projectAssessmentReport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectAssessmentReport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectAssessmentReportExists(projectAssessmentReport.Id))
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
            return View(projectAssessmentReport);
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

            var projectAssessmentReport = DatabaseHelper.GetProjectAssessmentReport(id, _context);

            if (projectAssessmentReport == null)
            {
                return NotFound();
            }

            return View(projectAssessmentReport);
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
            _context.ProjectAssessmentReport.Remove(DatabaseHelper.GetProjectAssessmentReport(id, _context));
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectAssessmentReportId"></param>
        /// <returns></returns>
        public IActionResult AddRecommendation(int projectAssessmentReportId)
        {
            var projectAssessmentReport = DatabaseHelper.GetProjectAssessmentReport(projectAssessmentReportId, _context);

            if (projectAssessmentReport == null)
            {
                return NotFound();
            }

            Recommendation recommendation = new Recommendation { Effort = new Effort() };

            ViewData["Title"] = string.Concat("New recomendation for ", projectAssessmentReport.Title);
            ViewData["ProjectAssessmentReportId"] = projectAssessmentReport.Id;
            ViewBag.EffortMeasures = _adminContext.EffortMeasure.Any() ? _adminContext.EffortMeasure.Select(s => s.Value).ToList() : new List<string>();

            return View(recommendation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recommendation"></param>
        /// <param name="effort"></param>
        /// <param name="projectAssessmentReportId"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRecommendation([Bind("Id,Details,Effort.Amount,Effort.Measure,CreatedBy,Created,Status")] Recommendation recommendation, Effort effort, int projectAssessmentReportId)
        {
            var projectAssessmentReport = DatabaseHelper.GetProjectAssessmentReport(projectAssessmentReportId, _context);

            if (projectAssessmentReport == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                recommendation.Effort = effort;
                recommendation.Status = EnumHelper.GetDisplayName(StatusEnum.Submitted);
                recommendation.Created = DateTime.Now;
                recommendation.CreatedBy = User.Identity.Name == null ? "Unknown User" : User.Identity.Name;

                projectAssessmentReport.Recommendations.Add(recommendation);
                UpdateAssessmentStatus(projectAssessmentReport);
                _context.ProjectAssessmentReport.Update(projectAssessmentReport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(RecommendationDetails), new { id = projectAssessmentReport.Id });
            }

            ViewData["Title"] = string.Concat("New recomendation for ", projectAssessmentReport.Title);
            ViewData["ProjectAssessmentReportId"] = projectAssessmentReport.Id;
            ViewBag.EffortMeasures = _adminContext.EffortMeasure.Any() ? _adminContext.EffortMeasure.Select(s => s.Value).ToList() : new List<string>();

            return View(recommendation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> RecommendationDetails(int? id)
        {
            var projectAssessmentReport = _context.ProjectAssessmentReport
                .Where(r => r.Recommendations.Any(x => x.Id == id))
                .FirstOrDefault();

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
            ViewData["ControllerName"] = "ProjectAssessmentReports";
            ViewData["SubmissionId"] = projectAssessmentReport.Id;
            return View(recommendation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="projectAssessmentReportId"></param>
        /// <returns></returns>
        public async Task<IActionResult> EditRecommendation(int id, int projectAssessmentReportId)
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
            ViewData["ProjectAssessmentReportId"] = projectAssessmentReportId;
            ViewBag.EffortMeasures = _adminContext.EffortMeasure.Any() ? _adminContext.EffortMeasure.Select(s => s.Value).ToList() : new List<string>();
            return View(recommendation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recommendation"></param>
        /// <param name="effort"></param>
        /// <param name="projectAssessmentReportId"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRecommendation([Bind("Id,Details,Effort.Id,Effort.Amount,Effort.Measure,CreatedBy,Created,Status")] Recommendation recommendation, Effort effort, int projectAssessmentReportId)
        {
            if (ModelState.IsValid)
            {
                _context.Effort.Update(effort);
                _context.Recommendation.Update(recommendation);
                _context.Effort.Update(effort);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = projectAssessmentReportId });
            }

            ViewData["Title"] = "Edit a recommendation";
            ViewData["ProjectAssessmentReportId"] = projectAssessmentReportId;
            ViewBag.EffortMeasures = _adminContext.EffortMeasure.Any() ? _adminContext.EffortMeasure.Select(s => s.Value).ToList() : new List<string>();
            return View(projectAssessmentReportId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="projectAssessmentReportId"></param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteRecommendation(int? id, int projectAssessmentReportId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recommendation = await _context.Recommendation.FirstOrDefaultAsync(m => m.Id == id);

            if (recommendation == null)
            {
                return NotFound();
            }

            ViewData["Title"] = "Confirm Deletion";
            ViewData["ProjectAssessmentReportId"] = projectAssessmentReportId;
            return View(recommendation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="projectAssessmentReportId"></param>
        /// <returns></returns>
        [HttpPost, ActionName("DeleteRecommendation")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRecommendationConfirmed(int id, int projectAssessmentReportId)
        {
            var projectAssessmentReport = DatabaseHelper.GetProjectAssessmentReport(id, _context);

            ViewData["Title"] = string.Concat("Recommendation for ", projectAssessmentReport.Title);

            var recommendation = await _context.Recommendation.FirstOrDefaultAsync(m => m.Id == id);
            _context.Recommendation.Remove(recommendation);

            UpdateAssessmentStatus(projectAssessmentReport);
            _context.Update(projectAssessmentReport);
            await _context.SaveChangesAsync();

            ViewData["ActionMessage"] = "Recommendation has been removed";
            ViewData["ActionResult"] = "success";

            return RedirectToAction(nameof(Details), new { id = projectAssessmentReportId });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="recommendationId"></param>
        /// <returns></returns>
        public async Task<IActionResult> AddProcessStep(int recommendationId)
        {
            var recommendation = _context.Recommendation.Where(i => i.Id == recommendationId).Include(i => i.ProcessSteps).FirstOrDefault();

            ViewData["Title"] = "Add a process step";
            ViewData["RecommendationId"] = recommendationId;

            ProcessStep step = new ProcessStep();

            if (recommendation.ProcessSteps == null)
            {
                step.OrderPosition = 1;
            }
            else
            {
                step.OrderPosition = recommendation.ProcessSteps.Count() + 1;
            }

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
                return RedirectToAction(nameof(RecommendationDetails), new { id = recommendation.Id });
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
                return RedirectToAction(nameof(RecommendationDetails), new { id = recommendationId });
            }

            ViewData["Title"] = "Edit a process step";
            ViewData["RecommendationId"] = recommendationId;
            return View(recommendationId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="recommendationId"></param>
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

            return RedirectToAction(nameof(RecommendationDetails), new { id = recommendationId });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectAssessmentReport"></param>
        private static void UpdateAssessmentStatus(ProjectAssessmentReport projectAssessmentReport)
        {
            projectAssessmentReport.Status = projectAssessmentReport.Recommendations.Count() >= 2 ? EnumHelper.GetDisplayName(StatusEnum.EligibleForDecision) : EnumHelper.GetDisplayName(StatusEnum.AwaitingFurtherRecommendations);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool ProjectAssessmentReportExists(int id)
        {
            return _context.ProjectAssessmentReport.Any(e => e.Id == id);
        }
    }
}
