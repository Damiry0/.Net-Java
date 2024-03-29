﻿using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web;
using Newtonsoft.Json;
using static SpotifyAPI.Web.Scopes;

namespace SpotityStats  
{
    public static class Spotify
    {
        private const string CredentialsPath = "credentials.json";
        private static readonly string? clientId = "0dfe25d5acc5413ab2376db48064fb41";
        private static readonly EmbedIOAuthServer _server = new EmbedIOAuthServer(new Uri("http://localhost:5000/callback"), 5000);

        internal static async Task<List<TopTracks>> GetTopTracksList(PersonalizationTopRequest request)
        {
            var spotify = await LoginClient();
            var topTracks = await spotify.PaginateAll(await spotify.Personalization.GetTopTracks(request));
            var topTracksList = new List<TopTracks>();
            foreach (var track in topTracks)
            {
            
                topTracksList.Add(new TopTracks()
                {
                    Id = track.TrackNumber,
                    Title = track.Name,
                    Author = track.Artists[0].Name,
                    AlbumCover = track.Album.Images[0].Url,
                    Url = track.ExternalUrls.SingleOrDefault().Value
                });
            }
            _server.Dispose();
            return topTracksList;
        }

        internal static async Task<List<TopArtist>> GetTopArtistsList(PersonalizationTopRequest request)
        {
            var spotify = await LoginClient();
            var user = await spotify.UserProfile.Current(); // auth works
            var topArtists = await spotify.PaginateAll(await spotify.Personalization.GetTopArtists(request));
            var topArtistsList = new List<TopArtist>();
            foreach (var artist in topArtists)
            {
                topArtistsList.Add(new TopArtist()
                {
                    Author = artist.Name,
                    Id = artist.Popularity,
                    Photo = artist.Images.FirstOrDefault()?.Url,
                    Popularity = artist.Followers.Total.ToString(),
                    ArtistsProfile = artist.ExternalUrls.FirstOrDefault().Value
                });
            }
            _server.Dispose();
            return topArtistsList;
        }

        internal static async Task<List<Tracks>> GetRecentTracksList()
        {
            var spotify = await LoginClient();
            var recentTracks = await spotify.PaginateAll(await spotify.Player.GetRecentlyPlayed());
            var recentTracksList = new List<Tracks>();
            foreach (var track in recentTracks)
            {
            
                recentTracksList.Add(new Tracks()
                {
                    Name = track.Track.Name, 
                    Artist = track.Track.Artists.FirstOrDefault()?.Name,
                    PlayedAt = track.PlayedAt.Date + track.PlayedAt.TimeOfDay
                });
            }
            _server.Dispose();
            return recentTracksList;
        }


        internal static async Task<Dictionary<string,int>> GetTopGenres(PersonalizationTopRequest request)
        {
            var spotify = await LoginClient();
            var topArtists = await spotify.PaginateAll(await spotify.Personalization.GetTopArtists(request));
            var allGenres = new List<string>();
            foreach (var topTrack in topArtists)
            {
                allGenres.AddRange(topTrack.Genres);
            }
            var countedGenres = allGenres.GroupBy(x => x)
                .ToDictionary(x => x.Key, x => x.Count());
            var ordered = countedGenres.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            return ordered;

        }

        private static async Task<SpotifyClient> LoginClient()
        {
            var json = await File.ReadAllTextAsync(CredentialsPath);
            var token = JsonConvert.DeserializeObject<PKCETokenResponse>(json);

            var authenticator = new PKCEAuthenticator(clientId!, token!);
            authenticator.TokenRefreshed += (sender, token) => File.WriteAllText(CredentialsPath, JsonConvert.SerializeObject(token));

            var config = SpotifyClientConfig.CreateDefault()
                .WithAuthenticator(authenticator);

            var spotify = new SpotifyClient(config);
            return spotify;
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
            };

            var request = new LoginRequest(_server.BaseUri, clientId!, LoginRequest.ResponseType.Code)
            {
                CodeChallenge = challenge,
                CodeChallengeMethod = "S256",
                Scope = new List<string> { UserReadEmail, UserReadPrivate, PlaylistReadPrivate, PlaylistReadCollaborative, UserTopRead, UserReadRecentlyPlayed}
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
