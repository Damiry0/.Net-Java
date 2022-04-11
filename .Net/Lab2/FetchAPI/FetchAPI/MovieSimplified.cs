using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FetchAPI
{
    public class MovieSimplified
    {

        public string Title { get; set; }

        public string UserRating { get; set; }

        public string Year { get; set; }

        public string Released { get; set; }

        public string Runtime { get; set; }

        public string Genre { get; set; }

        public string Metascore { get; set; }

        public string imdbRating { get; set; }
    }
}
