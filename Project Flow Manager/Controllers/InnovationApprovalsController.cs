using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Flow_Manager.Enums;
using Project_Flow_Manager.Helpers;
using ProjectFlowManagerModels;
using System.Security.Claims;

namespace Project_Flow_Manager.Controllers
{
    /// <summary>
    /// TODO
    /// </summary>
    public class InnovationApprovalsController : Controller
    {
        private readonly InnovationManagerContext _context;
        private readonly ProjectFlowAdministrationContext _adminContext;

        public InnovationApprovalsController(InnovationManagerContext context, ProjectFlowAdministrationContext adminContext)
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
            ViewData["Title"] = "Approvals";
            return View(DatabaseHelper.GetInnovationSubmissionsForApproval(_context));
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

            var innovation = await _context.Innovation
                .Include(m => m.ProcessSteps)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (innovation == null)
            {
                return NotFound();
            }

            ViewData["Title"] = string.Concat("Review ", innovation.Title);
            return View(innovation);
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
            ViewData["InnovationId"] = id;

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="approval"></param>
        /// <param name="innovationId"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Approve")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveConfirmed([Bind("Reason")] Approval approval, int innovationId)
        {
            if (innovationId == null)
            {
                return NotFound();
            }

            approval.ApprovedOn = DateTime.Now;
            approval.ApprovedBy = GetCurrentUserName();
            approval.Outcome = "Approved";
            approval.Type = "Innovation Submission";

            var innovation = _context.Innovation
                .Where(i => i.Id == innovationId)
                .Include(i => i.ProcessSteps)
                .Include(i => i.Technologies)
                .Include(i => i.Approval)
                .FirstOrDefault();

            if (innovation == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Approval.Add(approval);

                innovation.Status = "Approved";
                innovation.Approval = approval;
                _context.Innovation.Update(innovation);

                ProjectAssessmentReport report = new ProjectAssessmentReport { 
                    Title = innovation.Title, 
                    Innovation = innovation, 
                    Status = EnumHelper.GetDisplayName(StatusEnum.New),
                    Created = DateTime.Now,
                    CreatedBy = GetCurrentUserName()
                };
                
                _context.ProjectAssessmentReport.Add(report);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Title"] = "Confirm Approval";
            ViewData["InnovationId"] = innovationId;

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
        /// <param name="innovationId"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Decline")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeclineConfirmed([Bind("Reason")] Approval approval, int innovationId)
        {
            if (innovationId == null)
            {
                return NotFound();
            }

            var innovation = _context.Innovation
                .Where(i => i.Id == innovationId)
                .Include(i => i.ProcessSteps)
                .Include(i => i.Technologies)
                .Include(i => i.Approval)
                .FirstOrDefault();

            if (innovation == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                approval.ApprovedOn = DateTime.Now;
                approval.ApprovedBy = GetCurrentUserName();
                approval.Outcome = "Declined";
                approval.Type = "Innovation Submission";
                _context.Approval.Add(approval);

                innovation.Status = "Declined";
                innovation.Approval = approval;
                _context.Innovation.Update(innovation);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Title"] = "Decline Approval";
            ViewData["InnovationId"] = innovationId;

            return View();
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
