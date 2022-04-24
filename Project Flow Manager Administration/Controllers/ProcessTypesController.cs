#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_Flow_Manager_Models;

namespace Project_Flow_Manager_Administration.Controllers
{
    public class ProcessTypesController : Controller
    {
        private readonly ProjectFlowAdministrationContext _context;

        public ProcessTypesController(ProjectFlowAdministrationContext context)
        {
            _context = context;
        }

        // GET: ProcessTypes
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Process Types";
            return View(await _context.ProcessType.ToListAsync());
        }

        // GET: ProcessTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var processType = await _context.ProcessType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (processType == null)
            {
                return NotFound();
            }

            return View(processType);
        }

        // GET: ProcessTypes/Create
        public IActionResult Create()
        {
            ViewData["Title"] = "Add an new option";
            return View();
        }

        // POST: ProcessTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Value")] ProcessType processType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(processType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(processType);
        }

        // GET: ProcessTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var processType = await _context.ProcessType.FindAsync(id);
            if (processType == null)
            {
                return NotFound();
            }
            return View(processType);
        }

        // POST: ProcessTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Value")] ProcessType processType)
        {
            if (id != processType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(processType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProcessTypeExists(processType.Id))
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
            ViewData["Title"] = "Edit option";
            return View(processType);
        }

        // GET: ProcessTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var processType = await _context.ProcessType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (processType == null)
            {
                return NotFound();
            }

            ViewData["Title"] = "Confirm Deletion";
            return View(processType);
        }

        // POST: ProcessTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var processType = await _context.ProcessType.FindAsync(id);
            _context.ProcessType.Remove(processType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProcessTypeExists(int id)
        {
            return _context.ProcessType.Any(e => e.Id == id);
        }
    }
}
