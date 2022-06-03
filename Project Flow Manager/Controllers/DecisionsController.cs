using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Flow_Manager.Enums;
using Project_Flow_Manager.Helpers;
using Project_Flow_Manager_Models;
using ProjectFlowManagerModels;
using System.Security.Claims;

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
            return View(DatabaseHelper.GetReportsForDecision(_context));
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

            var projectAssessmentReport = DatabaseHelper.GetProjectAssessmentReport(id, _context);

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
            var choice = _context.Recommendation.Where(r => r.Id == id).FirstOrDefault();
            var projectAssessmentReport = _context.ProjectAssessmentReport.Where(r => r.Recommendations.Any(x => x.Id == id)).FirstOrDefault();

            //var projectAssessmentReport = choice.ProjectAssessmentReport;
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

            var projectAssessmentReport = DatabaseHelper.GetProjectAssessmentReport(projectAssessmentReportId, _context);

            if (projectAssessmentReport == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                approval.ApprovedOn = DateTime.Now;
                approval.ApprovedBy = GetCurrentUserName();
                approval.Outcome = EnumHelper.GetDisplayName(StatusEnum.Approved);
                approval.Type = EnumHelper.GetDisplayName(ApprovalTypeEnum.ProjectAssessmentReport);

                _context.Approval.Add(approval);

                projectAssessmentReport.Approvals.Add(approval);
                SetDecisionStatus(projectAssessmentReport);

                var approvalCount = projectAssessmentReport.Approvals.Where(a => a.Outcome == EnumHelper.GetDisplayName(StatusEnum.Approved)).Count();
                
                if (approvalCount >= 2)
                {
                    var resouceRequest = new ResourceRequest()
                    {
                        ProjectAssessmentReport = projectAssessmentReport,
                        Status = EnumHelper.GetDisplayName(StatusEnum.AwaitingAllocationOfResource),
                        Created = DateTime.Now,
                        CreatedBy = GetCurrentUserName()
                    };
                    _context.ResourceRequest.Add(resouceRequest);
                }

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
            ViewData["ProjectAssessmentReportId"] = id;

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

            var projectAssessmentReport = DatabaseHelper.GetProjectAssessmentReport(projectAssessmentReportId, _context);

            if (projectAssessmentReport == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                approval.ApprovedOn = DateTime.Now;
                approval.ApprovedBy = GetCurrentUserName();
                approval.Outcome = EnumHelper.GetDisplayName(StatusEnum.Declined);
                approval.Type = EnumHelper.GetDisplayName(ApprovalTypeEnum.ProjectAssessmentReport);

                _context.Approval.Add(approval);

                projectAssessmentReport.Approvals.Add(approval);
                SetDecisionStatus(projectAssessmentReport);

                _context.ProjectAssessmentReport.Update(projectAssessmentReport);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Title"] = "Decline Approval";
            ViewData["ProjectAssessmentReportId"] = projectAssessmentReportId;

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectAssessmentReport"></param>
        private static void SetDecisionStatus(ProjectAssessmentReport? projectAssessmentReport)
        {
            if (projectAssessmentReport.Approvals.Count() >= 2)
            {
                projectAssessmentReport.Status = EnumHelper.GetDisplayName(StatusEnum.AwaitingAllocationOfResource);
            }
            else
            {
                projectAssessmentReport.Status = EnumHelper.GetDisplayName(StatusEnum.AwaitingAdditionalApproval);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetCurrentUserName()
        {
            Claim? claim = User.Claims.FirstOrDefault(x => x.Type.ToString() == "name");
            return claim.Value;
        }
    }
}
