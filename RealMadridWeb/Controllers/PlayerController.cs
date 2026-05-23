using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RealMadridWeb.Data;
using RealMadridWeb.Models;

namespace RealMadridWeb.Controllers
{
    public class PlayersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlayersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var players = await _context.Players.Include(p => p.Team).ToListAsync();
            return View(players);
        }

        public async Task<IActionResult> Create()
        {
            var teams = await _context.Teams.ToListAsync();
            ViewData["TeamId"] = new SelectList(teams, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Position,ShirtNumber,Goals,Assists,TeamId")] Player player)
        {
            if (ModelState.IsValid)
            {
                _context.Add(player);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var teams = await _context.Teams.ToListAsync();
            ViewData["TeamId"] = new SelectList(teams, "Id", "Name", player.TeamId);
            return View(player);
        }
    }
}