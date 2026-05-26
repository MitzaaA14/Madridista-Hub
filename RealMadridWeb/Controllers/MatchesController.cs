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
    public class MatchesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProfileService _profileService;
        private readonly UserManager<IdentityUser> _userManager;

        public MatchesController(
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
            var matches = _context.Matches.Include(m => m.Team);
            return View(await matches.ToListAsync());
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
        public async Task<IActionResult> Create([Bind("Id,Opponent,League,Stadium,Date,Venue,HomeTeamLogoUrl,AwayTeamLogoUrl,HomeScore,AwayScore,IsFinished,TeamId")] Match match)
        {
            if (ModelState.IsValid)
            {
                _context.Add(match);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", match.TeamId);
            return View(match);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var match = await _context.Matches.FindAsync(id);
            if (match == null) return NotFound();
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", match.TeamId);
            return View(match);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Opponent,League,Stadium,Date,Venue,HomeTeamLogoUrl,AwayTeamLogoUrl,HomeScore,AwayScore,IsFinished,TeamId")] Match match)
        {
            if (id != match.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try { _context.Update(match); await _context.SaveChangesAsync(); }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchExists(match.Id)) return NotFound(); else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", match.TeamId);
            return View(match);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var match = await _context.Matches.Include(m => m.Team).FirstOrDefaultAsync(m => m.Id == id);
            if (match == null) return NotFound();
            return View(match);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match != null) _context.Matches.Remove(match);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var match = await _context.Matches.Include(m => m.Team).FirstOrDefaultAsync(m => m.Id == id);
            if (match == null) return NotFound();

            var playerMatches = await _context.PlayerMatches
                .Include(pm => pm.Player)
                .Where(pm => pm.MatchId == id)
                .ToListAsync();

            ViewData["PlayerMatches"] = playerMatches;
            ViewData["PlayerId"] = new SelectList(
                _context.Players.Where(p => p.TeamId == match.TeamId).ToList(), "Id", "Name");

            // Comentarii
            var comments = await _profileService.GetCommentsByMatchAsync(id.Value);
            ViewData["Comments"] = comments.ToList();

            // Watchlist status + userId curent
            if (User.Identity?.IsAuthenticated == true)
            {
                var userId = _userManager.GetUserId(User)!;
                ViewData["IsWatchlisted"] = await _profileService.IsInWatchlistAsync(userId, id.Value);
                ViewData["CurrentUserId"] = userId;
            }

            return View(match);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddPlayer(int matchId, int playerId, int minutesPlayed, double rating)
        {
            var alreadyExists = await _context.PlayerMatches
                .AnyAsync(pm => pm.MatchId == matchId && pm.PlayerId == playerId);

            if (!alreadyExists)
            {
                _context.PlayerMatches.Add(new PlayerMatch
                {
                    MatchId = matchId, PlayerId = playerId,
                    MinutesPlayed = minutesPlayed, Rating = rating
                });
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Details), new { id = matchId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemovePlayer(int playerMatchId, int matchId)
        {
            var pm = await _context.PlayerMatches.FindAsync(playerMatchId);
            if (pm != null) { _context.PlayerMatches.Remove(pm); await _context.SaveChangesAsync(); }
            return RedirectToAction(nameof(Details), new { id = matchId });
        }

        private bool MatchExists(int id) => _context.Matches.Any(e => e.Id == id);
    }
}
