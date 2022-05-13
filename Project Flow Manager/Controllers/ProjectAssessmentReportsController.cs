#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_Flow_Manager_Models;
using ProjectFlowManagerModels;

namespace Project_Flow_Manager.Controllers
{
    public class ProjectAssessmentReportsController : Controller
    {
        private readonly InnovationManagerContext _context;
        private readonly ProjectFlowAdministrationContext _adminContext;

        public ProjectAssessmentReportsController(InnovationManagerContext context, ProjectFlowAdministrationContext adminContext)
        {
            _context = context;
            _adminContext = adminContext;
        }

        // GET: ProjectAssessmentReports
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Assessments";
            ViewData["InnovationCount"] = _context.Innovation.Count();
            return View(await _context.ProjectAssessmentReport.Include(i => i.Innovation).ToListAsync());
        }

        // GET: ProjectAssessmentReports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectAssessmentReport = await _context.ProjectAssessmentReport
                .Include(p => p.Recommendations)
                .ThenInclude(r => r.Effort)
                .Include(p => p.Innovation)
                .Include(p => p.Innovation.ProcessSteps)
                .Include(p => p.Innovation.Approval)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (projectAssessmentReport == null)
            {
                return NotFound();
            }

            ViewData["Title"] = string.Concat("Details of ", projectAssessmentReport.Title);
            return View(projectAssessmentReport);
        }

        // GET: ProjectAssessmentReports/Create
        public IActionResult Create()
        {
            ViewData["InnovationId"] = new SelectList(_context.Innovation, "Id", "Description");
            return View();
        }

        // POST: ProjectAssessmentReports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: ProjectAssessmentReports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectAssessmentReport = await _context.ProjectAssessmentReport.FindAsync(id);
            if (projectAssessmentReport == null)
            {
                return NotFound();
            }
            ViewData["InnovationId"] = new SelectList(_context.Innovation, "Id", "Description", projectAssessmentReport.InnovationId);
            return View(projectAssessmentReport);
        }

        // POST: ProjectAssessmentReports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            ViewData["InnovationId"] = new SelectList(_context.Innovation, "Id", "Description", projectAssessmentReport.InnovationId);
            return View(projectAssessmentReport);
        }

        // GET: ProjectAssessmentReports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectAssessmentReport = await _context.ProjectAssessmentReport
                .Include(p => p.Innovation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectAssessmentReport == null)
            {
                return NotFound();
            }

            return View(projectAssessmentReport);
        }

        // POST: ProjectAssessmentReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projectAssessmentReport = await _context.ProjectAssessmentReport.FindAsync(id);
            _context.ProjectAssessmentReport.Remove(projectAssessmentReport);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult AddRecommendation(int projectAssessmentReportId)
        {
            var projectAssessmentReport = _context.ProjectAssessmentReport.Where(i => i.Id == projectAssessmentReportId).FirstOrDefault();

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRecommendation([Bind("Id,Details,Effort.Amount,Effort.Measure,CreatedBy,CreatedDate")] Recommendation recommendation, Effort effort, int projectAssessmentReportId)
        {
            var projectAssessmentReport = _context.ProjectAssessmentReport.Where(i => i.Id == projectAssessmentReportId).Include(i => i.Recommendations).FirstOrDefault();

            if (projectAssessmentReport == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                recommendation.Effort = effort;
                recommendation.CreatedDate = DateTime.Now;
                recommendation.CreatedBy = User.Identity.Name == null ? "Unknown User" : User.Identity.Name;

                projectAssessmentReport.Recommendations.Add(recommendation);
                _context.ProjectAssessmentReport.Update(projectAssessmentReport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = projectAssessmentReport.Id });
            }

            ViewData["Title"] = string.Concat("New recomendation for ", projectAssessmentReport.Title);
            ViewData["ProjectAssessmentReportId"] = projectAssessmentReport.Id;
            ViewBag.EffortMeasures = _adminContext.EffortMeasure.Any() ? _adminContext.EffortMeasure.Select(s => s.Value).ToList() : new List<string>();

            return View(recommendation);
        }

        public async Task<IActionResult> RecommendationDetails(int? id)
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

        public async Task<IActionResult> EditRecommendation(int id, int projectAssessmentReportId)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRecommendation([Bind("Id,Details,Effort.Amount,Effort.Measure,CreatedBy,CreatedDate")] Recommendation recommendation, Effort effort, int projectAssessmentReportId)
        {
            if (ModelState.IsValid)
            {
                _context.Recommendation.Update(recommendation);
                _context.Effort.Update(effort);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = projectAssessmentReportId });
            }

            ViewData["Title"] = "Edit a recommendation";
            ViewData["ProjectAssessmentReportId"] = projectAssessmentReportId;
            return View(projectAssessmentReportId);
        }

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

        [HttpPost, ActionName("DeleteRecommendation")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRecommendationConfirmed(int id, int projectAssessmentReportId)
        {
            var projectAssessmentReport = _context.ProjectAssessmentReport.Where(i => i.Id == projectAssessmentReportId).Include(i => i.Recommendations).FirstOrDefault();
            ViewData["Title"] = string.Concat("Recommendation for ", projectAssessmentReport.Title);

            var recommendation = await _context.Recommendation.FirstOrDefaultAsync(m => m.Id == id);
            _context.Recommendation.Remove(recommendation);
            await _context.SaveChangesAsync();
            ViewData["ActionMessage"] = "Recommendation has been removed";
            ViewData["ActionResult"] = "success";

            return RedirectToAction(nameof(Details), new { id = projectAssessmentReportId });
        }

        private bool ProjectAssessmentReportExists(int id)
        {
            return _context.ProjectAssessmentReport.Any(e => e.Id == id);
        }
    }
}
