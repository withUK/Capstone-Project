using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_Flow_Manager.Enums;
using Project_Flow_Manager.Helpers;
using Project_Flow_Manager_Models;
using ProjectFlowManagerProject_Flow_Manager.Helpers;

namespace Project_Flow_Manager.Controllers
{
    public class ResourceRequestsController : Controller
    {
        private readonly InnovationManagerContext _context;
        private readonly ProjectFlowAdministrationContext _adminContext;

        public ResourceRequestsController(InnovationManagerContext context, ProjectFlowAdministrationContext adminContext)
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
            ViewData["Title"] = "Resourcing";
            return _context.ResourceRequest != null ? 
                          View(await _context.ResourceRequest
                            .Include(r => r.ProjectAssessmentReport)
                            .Include(r => r.Teams)
                            .Include(r => r.Technologies)
                            .ToListAsync()) :
                          Problem("Entity set 'InnovationManagerContext.ResourceRequest'  is null.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ResourceRequest == null)
            {
                return NotFound();
            }

            var resourceRequest = await _context.ResourceRequest
                .Include(r => r.ProjectAssessmentReport)
                .Include(r => r.Teams)
                .Include(r => r.Technologies)
                .Include(r => r.Comments)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (resourceRequest == null)
            {
                return NotFound();
            }

            ViewData["Title"] = string.Concat("Details of Request Id ", resourceRequest.Id);
            ViewData["ControllerName"] = "ResourceRequests";
            ViewData["SubmissionId"] = resourceRequest.Id;
            return View(resourceRequest);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            ViewData["Title"] = "Add a new request";
            ViewBag.TechnologyOptions = _adminContext.Technology.Any() ? _adminContext.Technology.Select(s => s.Name).ToList() : new List<string>();
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProjectAssessmentId")] ResourceRequest resourceRequest)
        {
            if (ModelState.IsValid)
            {
                resourceRequest.Created = DateTime.Now;
                resourceRequest.CreatedBy = GetCurrentUserName();
                resourceRequest.Status = EnumHelper.GetDisplayName(StatusEnum.New);
                _context.Add(resourceRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Title"] = "Add a new request";
            return View(resourceRequest);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ResourceRequest == null)
            {
                return NotFound();
            }

            var resourceRequest = await _context.ResourceRequest
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resourceRequest == null)
            {
                return NotFound();
            }

            ViewData["Title"] = "Confirm Deletion";
            return View(resourceRequest);
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
            if (_context.ResourceRequest == null)
            {
                return Problem("Entity set 'InnovationManagerContext.ResourceRequest'  is null.");
            }
            var resourceRequest = await _context.ResourceRequest.FindAsync(id);
            if (resourceRequest != null)
            {
                _context.ResourceRequest.Remove(resourceRequest);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddTeamResource(int resourceRequestId)
        {
            var resourceRequest = _context.ResourceRequest.Where(i => i.Id == resourceRequestId).Include(i => i.Teams).FirstOrDefault();

            if (resourceRequest == null)
            {
                return NotFound();
            }

            ViewData["Title"] = string.Concat("Team resource for : ", resourceRequestId);
            ViewData["ResourceRequestId"] = resourceRequest.Id;
            ViewBag.TeamsOptions = _adminContext.Team.Any() ? _adminContext.Team.Select(s => s.Name).ToList() : new List<string>();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTeamResource([Bind("Team,Hours")] TeamResource teamResource, int resourceRequestId)
        {
            var resourceRequest = _context.ResourceRequest.Where(i => i.Id == resourceRequestId).Include(i => i.Teams).FirstOrDefault();

            if (resourceRequest == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                resourceRequest.Teams.Add(teamResource);
                _context.ResourceRequest.Update(resourceRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = resourceRequest.Id });
            }

            ViewData["Title"] = string.Concat("Team resource for Request ID : ", resourceRequestId);
            ViewData["ResourceRequestId"] = resourceRequest.Id;
            
            return View(resourceRequest.Id);
        }

        public async Task<IActionResult> EditTeamResource(int id, int resourceRequestId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.TeamResource.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            ViewData["Title"] = "Edit this team resource";
            ViewData["ResourceRequestId"] = resourceRequestId;
           
            return View(team);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTeamResource([Bind("Id,Team,Hours")] TeamResource teamResource, int resourceRequestId)
        {
            if (ModelState.IsValid)
            {
                _context.TeamResource.Update(teamResource);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = resourceRequestId });
            }

            ViewData["Title"] = "Edit this team resource";
            ViewData["ResourceRequestId"] = resourceRequestId;
            ViewBag.TeamsOptions = _adminContext.Team.Any() ? _adminContext.Team.Select(s => s.Name).ToList() : new List<string>();
            
            return View(resourceRequestId);
        }

        public async Task<IActionResult> DeleteTeamResource(int? id, int resourceRequestId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.TeamResource.FirstOrDefaultAsync(m => m.Id == id);

            if (team == null)
            {
                return NotFound();
            }

            ViewData["Title"] = "Confirm Deletion";
            ViewData["ResourceRequestId"] = resourceRequestId;
            return View(team);
        }

        [HttpPost, ActionName("DeleteTeamResource")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTeamResourceConfirmed(int id, int resourceRequestId)
        {
            var resourceRequest = _context.ResourceRequest.Where(i => i.Id == resourceRequestId).FirstOrDefault();
            ViewData["Title"] = string.Concat("Team resource for : ", resourceRequest.Id);

            var teamResource = await _context.TeamResource.FirstOrDefaultAsync(m => m.Id == id);
            _context.TeamResource.Remove(teamResource);
            await _context.SaveChangesAsync();
            ViewData["ActionMessage"] = "Team resource has been removed";
            ViewData["ActionResult"] = "success";

            return RedirectToAction(nameof(Details), new { id = resourceRequestId });
        }

        public async Task<IActionResult> AddTechnologyResource(int resourceRequestId)
        {
            var resourceRequest = _context.ResourceRequest.Where(i => i.Id == resourceRequestId).Include(i => i.Teams).FirstOrDefault();

            if (resourceRequest == null)
            {
                return NotFound();
            }

            ViewData["Title"] = string.Concat("Technology resource for Request ID : ", resourceRequestId);
            ViewData["ResourceRequestId"] = resourceRequest.Id;
            ViewBag.TechnologyOptions = _adminContext.Technology.Any() ? _adminContext.Technology.Select(s => s.Name).ToList() : new List<string>();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTechnologyResource([Bind("ProductName")] TechnologyResource technologyResource, int resourceRequestId)
        {
            var resourceRequest = _context.ResourceRequest.Where(i => i.Id == resourceRequestId).Include(i => i.Teams).FirstOrDefault();

            if (resourceRequest == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                resourceRequest.Technologies.Add(technologyResource);
                _context.ResourceRequest.Update(resourceRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = resourceRequest.Id });
            }

            ViewData["Title"] = string.Concat("Technology resource for Request ID : ", resourceRequestId);
            ViewData["ResourceRequestId"] = resourceRequest.Id;
            ViewBag.TechnologyOptions = _adminContext.Technology.Any() ? _adminContext.Technology.Select(s => s.Name).ToList() : new List<string>();

            return View(resourceRequest.Id);
        }

        public async Task<IActionResult> DeleteTechnologyResource(int? id, int resourceRequestId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.TechnologyResource.FirstOrDefaultAsync(m => m.Id == id);

            if (team == null)
            {
                return NotFound();
            }

            ViewData["Title"] = "Confirm Deletion";
            ViewData["ResourceRequestId"] = resourceRequestId;
            return View(team);
        }

        [HttpPost, ActionName("DeleteTechnologyResource")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTechnologyResourceConfirmed(int id, int resourceRequestId)
        {
            var resourceRequest = _context.ResourceRequest.Where(i => i.Id == resourceRequestId).FirstOrDefault();
            ViewData["Title"] = string.Concat("Technology resource for Request ID : ", resourceRequest.Id);

            var technologyResource = await _context.TechnologyResource.FirstOrDefaultAsync(m => m.Id == id);
            _context.TechnologyResource.Remove(technologyResource);
            await _context.SaveChangesAsync();
            ViewData["ActionMessage"] = "Technology resource has been removed";
            ViewData["ActionResult"] = "success";

            return RedirectToAction(nameof(Details), new { id = resourceRequestId });
        }

        public IActionResult ConfirmResourceRequest(int? id)
        {
            var resourceRequest = _context.ResourceRequest.Where(i => i.Id == id).FirstOrDefault();
            resourceRequest.Status = EnumHelper.GetDisplayName(StatusEnum.PassedToDevelopement);

            CreateDevOps(id);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CreateDevOps(int? id)
        {
            if (id == null || _context.ResourceRequest == null)
            {
                return NotFound();
            }

            var resourceRequest = _context.ResourceRequest
                .Where(m => m.Id == id)
                .Include(m => m.ProjectAssessmentReport)
                .ThenInclude(p => p.Innovation)
                .FirstOrDefault();

            if (resourceRequest == null)
            {
                return NotFound();
            }

            DevOpsHelper.CreateProject(resourceRequest.ProjectAssessmentReport.Title, resourceRequest.ProjectAssessmentReport.Innovation.Description);
            resourceRequest.EnvironmentsCreated = true;

            _context.ResourceRequest.Update(resourceRequest);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = id });
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool ResourceRequestExists(int id)
        {
          return (_context.ResourceRequest?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
