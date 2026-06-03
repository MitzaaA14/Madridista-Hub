using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RealMadridWeb.Data;
using RealMadridWeb.DTOs.Match;
using RealMadridWeb.Models;
using RealMadridWeb.Services;

namespace RealMadridWeb.Controllers
{
    public class MatchesController : Controller
    {
        private readonly IMatchService _matchService;
        private readonly ITeamService _teamService;
        private readonly IProfileService _profileService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public MatchesController(
            IMatchService matchService,
            ITeamService teamService,
            IProfileService profileService,
            UserManager<IdentityUser> userManager,
            ApplicationDbContext context)
        {
            _matchService = matchService;
            _teamService = teamService;
            _profileService = profileService;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var matches = await _matchService.GetAllAsync();
            return View(matches);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var teams = await _teamService.GetAllAsync();
            ViewData["TeamId"] = new SelectList(teams, "Id", "Name");
            return View(new CreateMatchDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateMatchDto dto)
        {
            if (ModelState.IsValid)
            {
                await _matchService.CreateAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            var teams = await _teamService.GetAllAsync();
            ViewData["TeamId"] = new SelectList(teams, "Id", "Name", dto.TeamId);
            return View(dto);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var match = await _matchService.GetByIdAsync(id.Value);
            if (match == null) return NotFound();
            var teams = await _teamService.GetAllAsync();
            ViewData["TeamId"] = new SelectList(teams, "Id", "Name", match.TeamId);
            return View(match);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Opponent,League,Stadium,Date,Venue,HomeTeamLogoUrl,AwayTeamLogoUrl,HomeScore,AwayScore,IsFinished,TeamId")] MatchDto model)
        {
            if (id != model.Id) return NotFound();
            if (ModelState.IsValid)
            {
                var dto = new UpdateMatchDto
                {
                    Opponent = model.Opponent,
                    League = model.League,
                    Stadium = model.Stadium,
                    Date = model.Date,
                    Venue = model.Venue,
                    HomeTeamLogoUrl = model.HomeTeamLogoUrl,
                    AwayTeamLogoUrl = model.AwayTeamLogoUrl,
                    HomeScore = model.HomeScore,
                    AwayScore = model.AwayScore,
                    IsFinished = model.IsFinished,
                    TeamId = model.TeamId
                };
                var updated = await _matchService.UpdateAsync(id, dto);
                if (!updated) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            var teams = await _teamService.GetAllAsync();
            ViewData["TeamId"] = new SelectList(teams, "Id", "Name", model.TeamId);
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var match = await _matchService.GetByIdAsync(id.Value);
            if (match == null) return NotFound();
            return View(match);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _matchService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var match = await _matchService.GetByIdAsync(id.Value);
            if (match == null) return NotFound();

            var playerMatches = await _context.PlayerMatches
                .Include(pm => pm.Player)
                .Where(pm => pm.MatchId == id)
                .ToListAsync();

            ViewData["PlayerMatches"] = playerMatches;
            ViewData["PlayerId"] = new SelectList(
                _context.Players.Where(p => p.TeamId == match.TeamId).ToList(), "Id", "Name");

            var comments = await _profileService.GetCommentsByMatchAsync(id.Value);
            ViewData["Comments"] = comments.ToList();

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
    }
}
