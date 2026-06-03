using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealMadridWeb.DTOs.Team;
using RealMadridWeb.Services;

namespace RealMadridWeb.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ITeamService _teamService;

        public TeamsController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        public async Task<IActionResult> Index()
        {
            var teams = await _teamService.GetAllAsync();
            return View(teams);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View(new CreateTeamDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateTeamDto dto)
        {
            if (ModelState.IsValid)
            {
                await _teamService.CreateAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var team = await _teamService.GetByIdAsync(id.Value);
            if (team == null) return NotFound();
            return View(team);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ImageUrl")] TeamDto model)
        {
            if (id != model.Id) return NotFound();
            if (ModelState.IsValid)
            {
                var dto = new UpdateTeamDto { Name = model.Name, ImageUrl = model.ImageUrl };
                var updated = await _teamService.UpdateAsync(id, dto);
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
            var team = await _teamService.GetByIdAsync(id.Value);
            if (team == null) return NotFound();
            return View(team);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _teamService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
