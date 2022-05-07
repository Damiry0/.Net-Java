using Microsoft.AspNetCore.Mvc;

namespace SpotityStats.Controlers
{
    public class SpotifyController : Controller
    {
        [HttpPost]
        public async Task Authenticate()
        {
            await Spotify.StartAuthentication();
        }
    }
}
