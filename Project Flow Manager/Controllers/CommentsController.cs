using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_Flow_Manager.Enums;
using Project_Flow_Manager.Helpers;
using Project_Flow_Manager_Models;
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

        // GET: Comments
        public async Task<IActionResult> Index(SubmissionTypeEnum type)
        {
            return _context.Comment != null ? 
                          View(await _context.Comment.ToListAsync()) :
                          Problem("Entity set 'InnovationManagerContext.Comment'  is null.");
        }

        // GET: Comments/Details/5
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

        // GET: Comments/Create
        public IActionResult Create(string controllerName, int submissionId)
        {
            ViewData["Controller"] = controllerName;
            ViewData["SubmissionId"] = submissionId;
            ViewData["Title"] = "Comments";

            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Value")] Comment comment, string controllerName, int submissionId)
        {
            comment.Created = DateTime.Now;
            comment.CreatedBy = User.Identity.Name == null ? "Unknown User" : User.Identity.Name;

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

        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(int? id, string controller, int submissionId)
        {
            if (id == null || _context.Comment == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Value,Created,CreatedBy")] Comment comment, string controller, int submissionId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                if (controller != null)
                {
                    RedirectToAction(nameof(Details), controller, new { id = submissionId });
                }
            }
            return View(comment);
        }

        // GET: Comments/Delete/5
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

        // POST: Comments/Delete/5
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

        private bool CommentExists(int id)
        {
          return (_context.Comment?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
