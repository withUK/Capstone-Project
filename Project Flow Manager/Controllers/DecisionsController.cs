using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Flow_Manager.Enums;
using Project_Flow_Manager.Helpers;
using ProjectFlowManagerModels;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Approve(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["Title"] = "Confirm Approval";
            ViewData["ProjectAssessmentReportId"] = id;

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="approval"></param>
        /// <param name="projectAssessmentReportId"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Approve")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveConfirmed([Bind("Reason")] Approval approval, int projectAssessmentReportId)
        {
            if (projectAssessmentReportId == null)
            {
                return NotFound();
            }

            approval.ApprovedOn = DateTime.Now;
            approval.ApprovedBy = GetCurrentUser();
            approval.Outcome = "Approved";
            approval.Type = "Project Assessment Report";

            var projectAssessmentReport = _context.ProjectAssessmentReport
                .Where(i => i.Id == projectAssessmentReportId)
                .Include(i => i.Recommendations)
                .Include(i => i.Attachments)
                .Include(i => i.Comments)
                .FirstOrDefault();

            if (projectAssessmentReport == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Approval.Add(approval);

                projectAssessmentReport.Status = EnumHelper.GetDisplayName(StatusEnum.PassedToDevelopement);
                projectAssessmentReport.Approvals.Add(approval);
                _context.ProjectAssessmentReport.Update(projectAssessmentReport);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Title"] = "Confirm Approval";
            ViewData["ProjectAssessmentReportId"] = projectAssessmentReportId;

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Decline(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["Title"] = "Decline Submission";
            ViewData["InnovationId"] = id;

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="approval"></param>
        /// <param name="projectAssessmentReportId"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Decline")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeclineConfirmed([Bind("Reason")] Approval approval, int projectAssessmentReportId)
        {
            if (projectAssessmentReportId == null)
            {
                return NotFound();
            }

            var projectAssessmentReport = _context.ProjectAssessmentReport
                .Where(i => i.Id == projectAssessmentReportId)
                .Include(i => i.Recommendations)
                .Include(i => i.Attachments)
                .Include(i => i.Comments)
                .FirstOrDefault();

            if (projectAssessmentReport == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                approval.ApprovedOn = DateTime.Now;
                approval.ApprovedBy = User.Identity.Name == null ? "Unknown User" : User.Identity.Name;
                approval.Outcome = "Declined";
                approval.Type = "Innovation Submission";
                _context.Approval.Add(approval);

                projectAssessmentReport.Status = "Declined";
                projectAssessmentReport.Approvals.Add(approval);
                _context.ProjectAssessmentReport.Update(projectAssessmentReport);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Title"] = "Decline Approval";
            ViewData["InnovationId"] = projectAssessmentReportId;

            return View();
        }

        private string GetCurrentUser()
        {
            return User.Identity.Name == null ? "Unknown User" : User.Identity.Name;
        }
    }
}
