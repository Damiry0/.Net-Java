using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.EntityFrameworkCore.Sqlite;
//using System.Data.Entity;
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

		public Films Show()
        {
			using (Films context = new Films())
			{
				var movies = context.Movies;

				return new Films() { Movies = movies };
			}
		}

		public Films FindByYear(string year)
        {
			using (Films context = new Films())
			{
				var movies = context.Movies.Where(m => m.Year == year);
                return new Films() { Movies = (DbSet<Movie>)movies };
			}
		}

	}
}
