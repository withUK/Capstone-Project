using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Project_Flow_Manager.Controllers
{
    public class DecisionsController : Controller
    {
        private readonly InnovationManagerContext _context;
        private readonly ProjectFlowAdministrationContext _adminContext;

        public DecisionsController(InnovationManagerContext context, ProjectFlowAdministrationContext adminContext)
        {
            _context = context;
            _adminContext = adminContext;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Decisions";
            return View(await _context.ProjectAssessmentReport.Include(i => i.Innovation).ToListAsync());
        }
    }
}
