using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace FetchAPI
{
	public class Films : DbContext
	{
        public Films()
        {
            Database.EnsureCreated();
        }
		public virtual DbSet<Movie> Movies { get; init; }
		protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite(@"Data Source=Films.db");

		

		public void Add(Movie movie)
        {
            using Films context = new Films();
            context.Movies.Add(movie);
            context.SaveChanges();
        }

		public List<Movie> Show()
		{

			using (Films context = new Films())
			{
				return (from m in context.Movies select m).ToList<Movie>();
			}
		}

		public List<Movie> FindBy(string What, string key)
		{
			using (Films context = new Films())
			{
				switch (What)
				{
					case "Year":
						return (from m in context.Movies where m.Year == key select m).ToList<Movie>();
					case "Rated":
						return (from m in context.Movies where m.Rated == key select m).ToList<Movie>();
					case "Released":
						return (from m in context.Movies where m.Released == key select m).ToList<Movie>();
					case "Runtime":
						return (from m in context.Movies where m.Runtime == key select m).ToList<Movie>();
					case "Genre":
						return (from m in context.Movies where m.Genre == key select m).ToList<Movie>();
					case "Writer":
						return (from m in context.Movies where m.Writer == key select m).ToList<Movie>();
					case "Actors":
						return (from m in context.Movies where m.Actors == key select m).ToList<Movie>();
					case "Country":
						return (from m in context.Movies where m.Country == key select m).ToList<Movie>();
					case "Awards":
						return (from m in context.Movies where m.Awards == key select m).ToList<Movie>();
						default:	
						return null;
				}
			}
		}


		public bool Exist(Movie movie)
        {
            return Movies.Any(x => x.Title == movie.Title);
        }


	}
}
