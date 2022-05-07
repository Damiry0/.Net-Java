using System.IO;
using System.Threading.Tasks;
using System;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static SpotifyAPI.Web.Scopes;

namespace SpotityStats  
{
    public static class Spotify
    {
        internal const string CredentialsPath = "credentials.json";
        private static readonly string? clientId = "0dfe25d5acc5413ab2376db48064fb41";
        private static readonly EmbedIOAuthServer _server = new EmbedIOAuthServer(new Uri("http://localhost:5000/callback"), 5000);
        private static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self)
        {
            return self?.Select((item, index) => (item, index)) ?? new List<(T, int)>();
        }
        private static void Exiting() => Console.CursorVisible = true;

        internal static async Task<List<TopTracks>> GetTopTracksList()
        {
            var json = await File.ReadAllTextAsync(CredentialsPath);
            var token = JsonConvert.DeserializeObject<PKCETokenResponse>(json);

            var authenticator = new PKCEAuthenticator(clientId!, token!);
            authenticator.TokenRefreshed += (sender, token) => File.WriteAllText(CredentialsPath, JsonConvert.SerializeObject(token));

            var config = SpotifyClientConfig.CreateDefault()
                .WithAuthenticator(authenticator);

            var spotify = new SpotifyClient(config);
            var topTracks = await spotify.PaginateAll(await spotify.Personalization.GetTopTracks(new PersonalizationTopRequest()
            {
                TimeRangeParam = PersonalizationTopRequest.TimeRange.ShortTerm
            }));
            var topTracksList = new List<TopTracks>();
            foreach (var track in topTracks)
            {
            
                topTracksList.Add(new TopTracks()
                {
                    Id = track.TrackNumber,
                    Title = track.Name,
                    Author = track.Artists[0].Name,
                    AlbumCover = track.Album.Images[0].Url,
                });
            }
            _server.Dispose();
            return topTracksList;
        }


        internal static async Task StartAuthentication()
        {
            var (verifier, challenge) = PKCEUtil.GenerateCodes();

            await _server.Start();
            _server.AuthorizationCodeReceived += async (sender, response) =>
            {
                await _server.Stop();
                PKCETokenResponse token = await new OAuthClient().RequestToken(
                    new PKCETokenRequest(clientId!, response.Code, _server.BaseUri, verifier)
                );

                await File.WriteAllTextAsync(CredentialsPath, JsonConvert.SerializeObject(token));
                await GetTopTracksList();
            };

            var request = new LoginRequest(_server.BaseUri, clientId!, LoginRequest.ResponseType.Code)
            {
                CodeChallenge = challenge,
                CodeChallengeMethod = "S256",
                Scope = new List<string> { UserReadEmail, UserReadPrivate, PlaylistReadPrivate, PlaylistReadCollaborative }
            };

            var uri = request.ToUri();
            try
            {
                BrowserUtil.Open(uri);
            }
            catch (Exception)
            {
                Console.WriteLine("Unable to open URL, manually open: {0}", uri);
            }
        }
    }   
}
