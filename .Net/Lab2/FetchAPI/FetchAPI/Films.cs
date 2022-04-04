using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Data.Entity;

namespace FetchAPI
{
	public class Films : DbContext
	{
		public virtual DbSet<Movie> Movies { get; set; }	
	}
}
