using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpotifyAPI.Web;
using static SpotifyAPI.Web.Scopes;

namespace SpotityStats
{
    public class Startup
    {
        public Startup(IConfigurationRoot configuration)
        {
            Configuration = configuration;
        }
        public IConfigurationRoot Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHttpContextAccessor();
            services.AddSingleton(SpotifyClientConfig.CreateDefault());
            services.AddScoped<SpotifyClientBuilder>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Spotify", policy =>
                {
                    policy.AuthenticationSchemes.Add("Spotify");
                    policy.RequireAuthenticatedUser();
                });
            });
            services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(50);
                })
                .AddSpotify(options =>
                {
                    options.ClientId = "0dfe25d5acc5413ab2376db48064fb41";
                    options.ClientSecret =":)";
                    options.CallbackPath = "/callback";
                    options.SaveTokens = true;

                    var scopes = new List<string> {
                        UserReadEmail, UserReadPrivate, PlaylistReadPrivate, PlaylistReadCollaborative
                    };
                    options.Scope.Add(string.Join(",", scopes));
                });
            services.AddRazorPages()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeFolder("/", "Spotify");
                });
        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }

    }

}
