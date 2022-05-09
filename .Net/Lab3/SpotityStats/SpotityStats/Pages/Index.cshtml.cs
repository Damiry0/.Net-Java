using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SpotifyAPI.Web;
using SpotityStats.Controlers;

namespace SpotityStats.Pages
{
    public class IndexModel : PageModel
    {

        public async Task OnPostPrint()
        {
            await Spotify.StartAuthentication();
        }

        public async Task OnPostDelete()
        {
            await Spotify.GetRecentTracksList();
        }

        
    }
}