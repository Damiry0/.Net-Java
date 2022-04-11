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

		public List<Movie> FindByYear(string year)
		{
			using (Films context = new Films())
			{
				return (from m in context.Movies where m.Year == year select m).ToList<Movie>();
			}
		}


		public bool Exist(Movie movie)
        {
            return Movies.Any(x => x.Title == movie.Title);
        }


	}
}
