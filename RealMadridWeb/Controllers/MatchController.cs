using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealMadridWeb.Data;
using RealMadridWeb.Models;

namespace RealMadridWeb.Controllers
{
    public class MatchesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MatchesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var matches = await _context.Matches.ToListAsync();
            return View(matches);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Opponent,Date,Stadium")] Match match)
        {
            if (ModelState.IsValid)
            {
                _context.Add(match);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(match);
        }
    }
}