using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealMadridWeb.Data;
using RealMadridWeb.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RealMadridWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var nextMatch = await _context.Matches
                .Include(m => m.Team)
                .Where(m => m.Date >= DateTime.Now)
                .OrderBy(m => m.Date)
                .FirstOrDefaultAsync();

            return View(nextMatch);
        }

        public IActionResult LiveStats()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}