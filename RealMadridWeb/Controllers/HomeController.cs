using Microsoft.AspNetCore.Mvc;
using RealMadridWeb.Models;
using RealMadridWeb.Services;
using System.Diagnostics;

namespace RealMadridWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMatchService _matchService;

        public HomeController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        public async Task<IActionResult> Index()
        {
            var upcoming = await _matchService.GetUpcomingAsync();
            var nextMatch = upcoming.FirstOrDefault();
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