using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RealMadridWeb.Data;
using RealMadridWeb.Models;
using RealMadridWeb.Services;

namespace RealMadridWeb.Controllers
{
    public class PlayersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProfileService _profileService;
        private readonly UserManager<IdentityUser> _userManager;

        public PlayersController(
            ApplicationDbContext context,
            IProfileService profileService,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _profileService = profileService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var players = await _context.Players.Include(p => p.Team).ToListAsync();

            // Dacă userul e logat, preîncarcă favorite în ViewData pentru fiecare player
            if (User.Identity?.IsAuthenticated == true)
            {
                var userId = _userManager.GetUserId(User)!;
                var favs = await _profileService.GetFavoritePlayersAsync(userId);
                var favSet = favs.Select(f => f.PlayerId).ToHashSet();
                foreach (var p in players)
                    ViewData[$"fav_{p.Id}"] = favSet.Contains(p.Id);
            }

            return View(players);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Name,Position,ShirtNumber,ImageUrl,TeamId,Goals,Assists")] Player player)
        {
            if (ModelState.IsValid)
            {
                _context.Add(player);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", player.TeamId);
            return View(player);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var player = await _context.Players.FindAsync(id);
            if (player == null) return NotFound();
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", player.TeamId);
            return View(player);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Position,ShirtNumber,ImageUrl,TeamId,Goals,Assists")] Player player)
        {
            if (id != player.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try { _context.Update(player); await _context.SaveChangesAsync(); }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerExists(player.Id)) return NotFound(); else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", player.TeamId);
            return View(player);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var player = await _context.Players.Include(p => p.Team).FirstOrDefaultAsync(m => m.Id == id);
            if (player == null) return NotFound();
            return View(player);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player != null) _context.Players.Remove(player);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerExists(int id) => _context.Players.Any(e => e.Id == id);
    }
}
