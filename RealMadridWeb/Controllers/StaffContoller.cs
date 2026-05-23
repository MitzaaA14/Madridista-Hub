using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RealMadridWeb.Data;
using RealMadridWeb.Models;

namespace RealMadridWeb.Controllers
{
    public class StaffController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StaffController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var staff = await _context.Staff.Include(s => s.Team).ToListAsync();
            return View(staff);
        }

        public async Task<IActionResult> Create()
        {
            var teams = await _context.Teams.ToListAsync();
            ViewData["TeamId"] = new SelectList(teams, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Role,TeamId")] Staff staffMember)
        {
            if (ModelState.IsValid)
            {
                _context.Add(staffMember);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var teams = await _context.Teams.ToListAsync();
            ViewData["TeamId"] = new SelectList(teams, "Id", "Name", staffMember.TeamId);
            return View(staffMember);
        }
    }
}