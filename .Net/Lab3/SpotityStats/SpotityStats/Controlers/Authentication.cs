using Microsoft.AspNetCore.Mvc;

namespace SpotityStats.Controlers
{
    public class Authentication : Controller
    {
        public async Task<IActionResult> Index()
        {
            await Spotify.StartAuthentication();
            return View();
        }
    }
}