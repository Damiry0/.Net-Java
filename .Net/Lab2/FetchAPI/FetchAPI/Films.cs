using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FetchAPI
{
	public sealed class Films : DbContext
	{
        public Films()
        {
            Database.EnsureCreated();
        }
		public DbSet<Movie> Movies { get; init; }
		protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite(@"Data Source=Films.db");

		public void Add(Movie movie)
        {
            using Films context = new Films();
            context.Movies.Add(movie);
            context.SaveChanges();
        }

		public List<Movie> Show()
        {
            using Films context = new Films();
            return (from m in context.Movies select m).ToList();
        }

        public List<MovieSimplified> ShowSimplified()
        {
            using Films context = new Films();
            var fullList=(from m in context.Movies select m).ToList();
            var serialized = JsonConvert.SerializeObject(fullList);
            return JsonConvert.DeserializeObject<List<MovieSimplified>>(serialized);
        }

		public List<Movie> FindBy(string What, string key)
        {
            using Films context = new Films();
            return What switch
            {
                "Year" => (from m in context.Movies where m.Year == key select m).ToList(),
                "Rated" => (from m in context.Movies where m.Rated == key select m).ToList(),
                "Released" => (from m in context.Movies where m.Released == key select m).ToList(),
                "Runtime" => (from m in context.Movies where m.Runtime == key select m).ToList(),
                "Genre" => (from m in context.Movies where m.Genre == key select m).ToList(),
                "Writer" => (from m in context.Movies where m.Writer == key select m).ToList(),
                "Actors" => (from m in context.Movies where m.Actors == key select m).ToList(),
                "Country" => (from m in context.Movies where m.Country == key select m).ToList(),
                "Awards" => (from m in context.Movies where m.Awards == key select m).ToList(),
                _ => null,
            };
        }


		public bool Exist(Movie movie)
        {
            return Movies.Any(x => x.Title == movie.Title);
        }
    }
}
