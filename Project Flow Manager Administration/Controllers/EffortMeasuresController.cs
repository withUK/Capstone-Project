using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Flow_Manager_Models;

namespace Project_Flow_Manager_Administration.Controllers
{
    public class EffortMeasuresController : Controller
    {
        private readonly ProjectFlowAdministrationContext _context;

        public EffortMeasuresController(ProjectFlowAdministrationContext context)
        {
            _context = context;
        }

        // GET: EffortMeasures
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Effort Measures";
            return View(await _context.EffortMeasure.ToListAsync());
        }

        // GET: EffortMeasures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EffortMeasure == null)
            {
                return NotFound();
            }

            var effortMeasure = await _context.EffortMeasure
                .FirstOrDefaultAsync(m => m.Id == id);
            if (effortMeasure == null)
            {
                return NotFound();
            }

            return View(effortMeasure);
        }

        // GET: EffortMeasures/Create
        public IActionResult Create()
        {
            ViewData["Title"] = "Add an new option";
            return View();
        }

        // POST: EffortMeasures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Value")] EffortMeasure effortMeasure)
        {
            if (ModelState.IsValid)
            {
                _context.Add(effortMeasure);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Title"] = "Add an new option";
            return View(effortMeasure);
        }

        // GET: EffortMeasures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EffortMeasure == null)
            {
                return NotFound();
            }

            var effortMeasure = await _context.EffortMeasure.FindAsync(id);
            if (effortMeasure == null)
            {
                return NotFound();
            }
            ViewData["Title"] = "Edit option";
            return View(effortMeasure);
        }

        // POST: EffortMeasures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Value")] EffortMeasure effortMeasure)
        {
            if (id != effortMeasure.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(effortMeasure);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EffortMeasureExists(effortMeasure.Id))
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
            return View(effortMeasure);
        }

        // GET: EffortMeasures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EffortMeasure == null)
            {
                return NotFound();
            }

            var effortMeasure = await _context.EffortMeasure
                .FirstOrDefaultAsync(m => m.Id == id);
            if (effortMeasure == null)
            {
                return NotFound();
            }

            ViewData["Title"] = "Confirm Deletion";
            return View(effortMeasure);
        }

        // POST: EffortMeasures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EffortMeasure == null)
            {
                return Problem("Entity set 'ProjectFlowAdministrationContext.EffortMeasure'  is null.");
            }
            var effortMeasure = await _context.EffortMeasure.FindAsync(id);
            if (effortMeasure != null)
            {
                _context.EffortMeasure.Remove(effortMeasure);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EffortMeasureExists(int id)
        {
            return (_context.EffortMeasure?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
