using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Flow_Manager.Enums;
using ProjectFlowManagerModels;

namespace Project_Flow_Manager.Controllers
{
    public class CommentsController : Controller
    {
        private readonly InnovationManagerContext _context;

        public CommentsController(InnovationManagerContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(SubmissionTypeEnum type)
        {
            return _context.Comment != null ? 
                          View(await _context.Comment.ToListAsync()) :
                          Problem("Entity set 'InnovationManagerContext.Comment'  is null.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Comment == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="submissionId"></param>
        /// <returns></returns>
        public IActionResult Create(string controllerName, int submissionId)
        {
            ViewData["Controller"] = controllerName;
            ViewData["SubmissionId"] = submissionId;
            ViewData["Title"] = "Comments";

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comment"></param>
        /// <param name="controllerName"></param>
        /// <param name="submissionId"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Value")] Comment comment, string controllerName, int submissionId)
        {
            comment.Created = DateTime.Now;
            comment.CreatedBy = GetCurrentUserName();

            if (ModelState.IsValid)
            {
                _context.Add(comment);

                switch (controllerName)
                {
                    case "Innovations":
                        var innovation = _context.Innovation.Where(i => i.Id == submissionId)
                            .Include(i => i.Comments)
                            .FirstOrDefault();

                        if (innovation != null)
                        {
                            innovation.Comments.Add(comment);
                            _context.Update(innovation);
                        }
                        break;
                    case "ProjectAssessmentReports":
                        var report = _context.ProjectAssessmentReport.Where(i => i.Id == submissionId)
                            .Include(i => i.Comments)
                            .FirstOrDefault();

                        if (report != null)
                        {
                            report.Comments.Add(comment);
                            _context.Update(report);
                        }
                        break;
                    case "Recommendations":
                        var recommendation = _context.Recommendation.Where(i => i.Id == submissionId)
                            .Include(i => i.Comments)
                            .FirstOrDefault();

                        if (recommendation != null)
                        {
                            recommendation.Comments.Add(comment);
                            _context.Update(recommendation);
                        }
                        break;
                    case "ResourceRequests":
                        var request = _context.ResourceRequest.Where(i => i.Id == submissionId)
                            .Include(i => i.Comments)
                            .FirstOrDefault();

                        if (request != null)
                        {
                            request.Comments.Add(comment);
                            _context.Update(request);
                        }
                        break;
                    default:
                        break;
                }

                await _context.SaveChangesAsync();
                if (controllerName != null)
                {
                    return RedirectToAction(nameof(Details), controllerName, new { id = submissionId });
                }
            }
            return View(comment);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="controller"></param>
        /// <param name="submissionId"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int? id, string controller, int submissionId)
        {
            if (id == null || _context.Comment == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="controller"></param>
        /// <param name="submissionId"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id, string controller, int submissionId)
        {
            if (_context.Comment == null)
            {
                return Problem("Entity set 'InnovationManagerContext.Comment'  is null.");
            }

            var comment = await _context.Comment.FirstOrDefaultAsync(c => c.Id == id);
            if (comment != null)
            {
                _context.Comment.Remove(comment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), controller, new { id = submissionId });
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

        private bool CommentExists(int id)
        {
          return (_context.Comment?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
