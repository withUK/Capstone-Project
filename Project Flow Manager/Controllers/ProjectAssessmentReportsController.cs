#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            var innovationManagerContext = _context.ProjectAssessmentReport.Include(p => p.Innovation);
            return View(await innovationManagerContext.ToListAsync());
        }

        // GET: ProjectAssessmentReports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectAssessmentReport = await _context.ProjectAssessmentReport
                .Include(p => p.Innovation)
                .Include(i => i.Innovation.ProcessSteps)
                .Include(i => i.Innovation.Approval)
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

        private bool ProjectAssessmentReportExists(int id)
        {
            return _context.ProjectAssessmentReport.Any(e => e.Id == id);
        }
    }
}
