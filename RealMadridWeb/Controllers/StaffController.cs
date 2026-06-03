using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RealMadridWeb.DTOs.Staff;
using RealMadridWeb.Services;

namespace RealMadridWeb.Controllers
{
    public class StaffController : Controller
    {
        private readonly IStaffService _staffService;
        private readonly ITeamService _teamService;

        public StaffController(IStaffService staffService, ITeamService teamService)
        {
            _staffService = staffService;
            _teamService = teamService;
        }

        public async Task<IActionResult> Index()
        {
            var staff = await _staffService.GetAllAsync();
            return View(staff);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var teams = await _teamService.GetAllAsync();
            ViewData["TeamId"] = new SelectList(teams, "Id", "Name");
            return View(new CreateStaffDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateStaffDto dto)
        {
            if (ModelState.IsValid)
            {
                await _staffService.CreateAsync(dto);
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
            var staff = await _staffService.GetByIdAsync(id.Value);
            if (staff == null) return NotFound();
            var teams = await _teamService.GetAllAsync();
            ViewData["TeamId"] = new SelectList(teams, "Id", "Name", staff.TeamId);
            return View(staff);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ImageUrl,Role,TeamId")] StaffDto model)
        {
            if (id != model.Id) return NotFound();
            if (ModelState.IsValid)
            {
                var dto = new UpdateStaffDto
                {
                    Name = model.Name,
                    ImageUrl = model.ImageUrl,
                    Role = model.Role,
                    TeamId = model.TeamId
                };
                var updated = await _staffService.UpdateAsync(id, dto);
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
            var staff = await _staffService.GetByIdAsync(id.Value);
            if (staff == null) return NotFound();
            return View(staff);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _staffService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
