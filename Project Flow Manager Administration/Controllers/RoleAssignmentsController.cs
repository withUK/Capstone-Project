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
    public class RoleAssignmentsController : Controller
    {
        private readonly ProjectFlowAdministrationContext _context;

        public RoleAssignmentsController(ProjectFlowAdministrationContext context)
        {
            _context = context;
        }

        // GET: RoleAssignments
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Role Assignments";
            var projectFlowAdministrationContext = _context.RoleAssignment.Include(r => r.Role);
            return View(await projectFlowAdministrationContext.ToListAsync());
        }

        // GET: RoleAssignments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RoleAssignment == null)
            {
                return NotFound();
            }

            var roleAssignment = await _context.RoleAssignment
                .Include(r => r.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roleAssignment == null)
            {
                return NotFound();
            }

            return View(roleAssignment);
        }

        // GET: RoleAssignments/Create
        public IActionResult Create()
        {
            ViewData["Title"] = "Add a new role assignment";
            ViewData["RoleId"] = new SelectList(_context.Role, "Id", "Name");
            return View();
        }

        // POST: RoleAssignments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,RoleId")] RoleAssignment roleAssignment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roleAssignment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Title"] = "Add a new role assignment";
            ViewData["RoleId"] = new SelectList(_context.Role, "Id", "Name", roleAssignment.RoleId);
            return View(roleAssignment);
        }

        // GET: RoleAssignments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RoleAssignment == null)
            {
                return NotFound();
            }

            var roleAssignment = await _context.RoleAssignment.FindAsync(id);
            if (roleAssignment == null)
            {
                return NotFound();
            }
            ViewData["Title"] = "Edit the role assignment";
            ViewData["RoleId"] = new SelectList(_context.Role, "Id", "Name", roleAssignment.RoleId);
            return View(roleAssignment);
        }

        // POST: RoleAssignments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,RoleId")] RoleAssignment roleAssignment)
        {
            if (id != roleAssignment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roleAssignment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleAssignmentExists(roleAssignment.Id))
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
            ViewData["Title"] = "Edit the role assignment";
            ViewData["RoleId"] = new SelectList(_context.Role, "Id", "Name", roleAssignment.RoleId);
            return View(roleAssignment);
        }

        // GET: RoleAssignments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RoleAssignment == null)
            {
                return NotFound();
            }

            var roleAssignment = await _context.RoleAssignment
                .Include(r => r.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roleAssignment == null)
            {
                return NotFound();
            }

            return View(roleAssignment);
        }

        // POST: RoleAssignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RoleAssignment == null)
            {
                return Problem("Entity set 'ProjectFlowAdministrationContext.RoleAssignment'  is null.");
            }
            var roleAssignment = await _context.RoleAssignment.FindAsync(id);
            if (roleAssignment != null)
            {
                _context.RoleAssignment.Remove(roleAssignment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoleAssignmentExists(int id)
        {
          return (_context.RoleAssignment?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
