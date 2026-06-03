using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RealMadridWeb.DTOs;
using RealMadridWeb.Services;

namespace RealMadridWeb.Controllers
{
    public class PlayersController : Controller
    {
        private readonly IPlayerService _playerService;
        private readonly ITeamService _teamService;
        private readonly IProfileService _profileService;
        private readonly UserManager<IdentityUser> _userManager;

        public PlayersController(
            IPlayerService playerService,
            ITeamService teamService,
            IProfileService profileService,
            UserManager<IdentityUser> userManager)
        {
            _playerService = playerService;
            _teamService = teamService;
            _profileService = profileService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var players = await _playerService.GetAllPlayersAsync();

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
        public async Task<IActionResult> Create()
        {
            var teams = await _teamService.GetAllAsync();
            ViewData["TeamId"] = new SelectList(teams, "Id", "Name");
            return View(new PlayerCreateDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(PlayerCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                await _playerService.CreatePlayerAsync(dto);
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
            var player = await _playerService.GetPlayerByIdAsync(id.Value);
            if (player == null) return NotFound();
            return View(player);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Position,ShirtNumber,ImageUrl,TeamId,Goals,Assists")] PlayerReadDto model)
        {
            if (id != model.Id) return NotFound();
            if (ModelState.IsValid)
            {
                var dto = new PlayerCreateDto
                {
                    Name = model.Name,
                    Position = model.Position,
                    ShirtNumber = model.ShirtNumber,
                    ImageUrl = model.ImageUrl,
                    Goals = model.Goals,
                    Assists = model.Assists,
                    TeamId = model.TeamId
                };
                var updated = await _playerService.UpdatePlayerAsync(id, dto);
                if (!updated) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var player = await _playerService.GetPlayerByIdAsync(id.Value);
            if (player == null) return NotFound();
            return View(player);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _playerService.DeletePlayerAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
