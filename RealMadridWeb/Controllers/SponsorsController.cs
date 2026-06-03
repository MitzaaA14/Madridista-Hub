using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealMadridWeb.DTOs.Sponsor;
using RealMadridWeb.Services;

namespace RealMadridWeb.Controllers
{
    public class SponsorsController : Controller
    {
        private readonly ISponsorService _sponsorService;

        public SponsorsController(ISponsorService sponsorService)
        {
            _sponsorService = sponsorService;
        }

        public async Task<IActionResult> Index()
        {
            var sponsors = await _sponsorService.GetAllAsync();
            return View(sponsors);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View(new CreateSponsorDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateSponsorDto dto)
        {
            if (ModelState.IsValid)
            {
                await _sponsorService.CreateAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var sponsor = await _sponsorService.GetByIdAsync(id.Value);
            if (sponsor == null) return NotFound();
            return View(sponsor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,LogoUrl,Type")] SponsorDto model)
        {
            if (id != model.Id) return NotFound();
            if (ModelState.IsValid)
            {
                var dto = new UpdateSponsorDto { Name = model.Name, LogoUrl = model.LogoUrl, Type = model.Type };
                var updated = await _sponsorService.UpdateAsync(id, dto);
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
            var sponsor = await _sponsorService.GetByIdAsync(id.Value);
            if (sponsor == null) return NotFound();
            return View(sponsor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _sponsorService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
