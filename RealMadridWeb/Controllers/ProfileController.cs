using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealMadridWeb.Models;
using RealMadridWeb.Services;

namespace RealMadridWeb.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(
            IProfileService profileService,
            UserManager<IdentityUser> userManager,
            ILogger<ProfileController> logger)
        {
            _profileService = profileService;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            var vm = new ProfileViewModel
            {
                Email = user.Email ?? string.Empty,
                Watchlist = await _profileService.GetWatchlistAsync(user.Id),
                FavoritePlayers = await _profileService.GetFavoritePlayersAsync(user.Id),
                RecentComments = await _profileService.GetCommentsByUserAsync(user.Id)
            };

            return View(vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToWatchlist(int matchId, string? returnUrl)
        {
            var userId = _userManager.GetUserId(User)!;
            await _profileService.AddToWatchlistAsync(userId, matchId);
            return Redirect(returnUrl ?? Url.Action("Index", "Matches")!);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromWatchlist(int matchId, string? returnUrl)
        {
            var userId = _userManager.GetUserId(User)!;
            await _profileService.RemoveFromWatchlistAsync(userId, matchId);
            return Redirect(returnUrl ?? Url.Action("Index", "Profile")!);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFavoritePlayer(int playerId, string? returnUrl)
        {
            var userId = _userManager.GetUserId(User)!;
            await _profileService.AddFavoritePlayerAsync(userId, playerId);
            return Redirect(returnUrl ?? Url.Action("Index", "Players")!);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFavoritePlayer(int playerId, string? returnUrl)
        {
            var userId = _userManager.GetUserId(User)!;
            await _profileService.RemoveFavoritePlayerAsync(userId, playerId);
            return Redirect(returnUrl ?? Url.Action("Index", "Profile")!);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(int matchId, string content)
        {
            var userId = _userManager.GetUserId(User)!;
            if (string.IsNullOrWhiteSpace(content))
            {
                TempData["CommentError"] = "Comment cannot be empty.";
            }
            else
            {
                await _profileService.AddCommentAsync(userId, matchId, content);
            }
            return RedirectToAction("Details", "Matches", new { id = matchId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteComment(int commentId, int matchId)
        {
            var userId = _userManager.GetUserId(User)!;
            await _profileService.DeleteCommentAsync(userId, commentId);
            return RedirectToAction("Details", "Matches", new { id = matchId });
        }
    }
}
