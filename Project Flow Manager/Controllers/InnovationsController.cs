#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_Flow_Manager_Models;

namespace Project_Flow_Manager.Controllers
{
    public class InnovationsController : Controller
    {
        private readonly InnovationManagerContext _context;
        private readonly ProjectFlowAdministrationContext _adminContext;

        public InnovationsController(InnovationManagerContext context, ProjectFlowAdministrationContext adminContext)
        {
            _context = context;
            _adminContext = adminContext;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Innovation Submissions";
            return View(await _context.Innovation.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
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

            ViewData["Title"] = string.Concat("Details of ", innovation.Title);
            return View(innovation);
        }

        public IActionResult Create()
        {
            ViewData["Title"] = "Add a new idea";
            ViewBag.StatusOptions = _adminContext.Status.Any() ? _adminContext.Status.Select(s => s.Value).ToList() : new List<string>();
            ViewBag.ProcessTypes = _adminContext.ProcessType.Any() ? _adminContext.ProcessType.Select(s => s.Value).ToList() : new List<string>();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,ProcessDuration,NumberOfPeopleIncluded,ProcessType,Status,RequiredDate")] Innovation innovation)
        {
            innovation.SubmittedDate = DateTime.Now;
            innovation.SubmittedBy = User.Identity.Name == null ? "Unknown User" : User.Identity.Name;
            innovation.ProcessSteps = new List<ProcessStep>();

            if (ModelState.IsValid)
            {
                _context.Add(innovation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ProcessSteps), new { innovationId = innovation.Id });
            }
            ViewData["Title"] = "Add a new idea";
            ViewBag.StatusOptions = _adminContext.Status.Select(s => s.Value).ToList();
            return View(innovation.Id);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var innovation = await _context.Innovation.FindAsync(id);
            if (innovation == null)
            {
                return NotFound();
            }

            ViewData["Title"] = "Edit an idea";
            ViewBag.StatusOptions = _adminContext.Status.Select(s => s.Value).ToList();
            return View(innovation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,SubmittedDate,SubmittedBy,ProcessDuration,NumberOfPeopleIncluded,ProcessType,Status,RequiredDate")] Innovation innovation)
        {
            if (id != innovation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(innovation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InnovationExists(innovation.Id))
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

            ViewData["Title"] = "Edit an idea";
            ViewBag.StatusOptions = _adminContext.Status.Select(s => s.Value).ToList();
            
            return View(innovation);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var innovation = _context.Innovation.Where(i => i.Id == id).Include(i => i.ProcessSteps).FirstOrDefault();

            if (innovation == null)
            {
                return NotFound();
            }

            ViewData["Title"] = "Confirm Deletion";
            return View(innovation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var innovation = _context.Innovation.Where(i => i.Id == id).Include(i => i.ProcessSteps).FirstOrDefault();
            _context.Innovation.Remove(innovation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ProcessSteps(int innovationId)
        {
            var innovation = _context.Innovation.Where(i => i.Id == innovationId).Include(i => i.ProcessSteps).FirstOrDefault();
            
            if (innovation == null)
            {
                return NotFound();
            }

            ViewData["Title"] = string.Concat("Process Steps for ", innovation.Title);

            return View(innovation);
        }

        public async Task<IActionResult> AddProcessStep(int innovationId)
        {
            var innovation = _context.Innovation.Where(i => i.Id == innovationId).Include(i => i.ProcessSteps).FirstOrDefault();
            
            if (innovation == null)
            {
                return NotFound();
            }

            ViewData["Title"] = string.Concat("Process Step for ", innovation.Title);
            ViewData["InnovationId"] = innovation.Id;

            ProcessStep step = new ProcessStep();

            if (innovation.ProcessSteps == null)
            {
                step.OrderPosition = 1;
            }
            else
            {
                step.OrderPosition = innovation.ProcessSteps.Count() + 1;
            }

            return View(step);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProcessStep([Bind("Value,OrderPosition")] ProcessStep processStep, int innovationId)
        {
            var innovation = _context.Innovation.Where(i => i.Id == innovationId).Include(i => i.ProcessSteps).FirstOrDefault();
            
            if (innovation == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (innovation.ProcessSteps == null)
                {
                    innovation.ProcessSteps = new List<ProcessStep>();
                }

                innovation.ProcessSteps.Add(processStep);
                _context.Innovation.Update(innovation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ProcessSteps), new { innovationId = innovation.Id });
            }

            ViewData["Title"] = string.Concat("Process Step for ", new { innovationId = innovation.Title });
            return View(innovation.Id);
        }

        public async Task<IActionResult> DeleteProcessStep(int? id, int innovationId)
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
            ViewData["InnovationId"] = innovationId;
            return View(processStep);
        }

        [HttpPost, ActionName("DeleteProcessStep")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProcessStepConfirmed(int id, int innovationId)
        {
            var innovation = _context.Innovation.Where(i => i.Id == innovationId).Include(i => i.ProcessSteps).FirstOrDefault();
            ViewData["Title"] = string.Concat("Process Steps for ", innovation.Title);

            var processStep = await _context.ProcessStep.FirstOrDefaultAsync(m => m.Id == id);
            _context.ProcessStep.Remove(processStep);
            await _context.SaveChangesAsync();
            ViewData["ActionMessage"] = "Process step has been removed";
            ViewData["ActionResult"] = "success";

            return RedirectToAction(nameof(ProcessSteps), new { innovationId = innovationId });
        }

        private bool InnovationExists(int id)
        {
            return _context.Innovation.Any(e => e.Id == id);
        }
    }
}