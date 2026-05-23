using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealMadridWeb.Data;
using RealMadridWeb.Models;

namespace RealMadridWeb.Controllers
{
    public class SponsorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SponsorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var sponsors = await _context.Sponsors.ToListAsync();
            return View(sponsors);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Type")] Sponsor sponsor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sponsor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sponsor);
        }
    }
}