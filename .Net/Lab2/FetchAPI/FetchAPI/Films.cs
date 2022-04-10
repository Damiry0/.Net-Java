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
		public virtual DbSet<Movie> Movies { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite(@"Data Source=Univ.db");

		

		public void Add(Movie movie)
        {
			using (Films context = new Films()) {
				context.Movies.Add(movie);
				context.SaveChanges();
			}
		}

		public static Films Show()
        {
			using (Films context = new Films())
			{
				var movies = context.Movies;
				/*(Movies mv in movies)
				{
					Console.WriteLine("ID: {0}, Title: {1}", mv.Id, mv.Title);
				}*/
				return new Films() { Movies = movies };
			}
		}

		public static Films FindByYear(string year)
        {
			using (Films context = new Films())
			{
				var movies = context.Movies.Where(m => m.Year == year);
                /*(Movies mv in movies)
				{
					Console.WriteLine("ID: {0}, Title: {1}", mv.Id, mv.Title);
				}*/
                return new Films() { Movies = (DbSet<Movie>)movies };
			}
		}

	}
}
