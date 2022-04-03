using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace FetchAPI
{
    public class Movie
    {
        public string Title { get; set; }

        public string Year { get; set; }

        public string Rated { get; set; }

        public string Released { get; set; }

        public string Runtime { get; set; }

        public string Genre { get; set; }

        public string Writer { get; set; }

        public string Actors { get; set; }

        public string Plot { get; set; }

        public string Country { get; set; }

        public string Awards { get; set; }

        public string Poster { get; set; }


        public string imdbRating { get; set; }

        public string imdbVotes { get; set; }

        public string BoxOffice { get; set; }

        public override string ToString()
        {
            return $"Title: {Title} \n" +
                   $"Year: {Year}  \n" +
                   $"Runtime: {Runtime}  \n" +
                   $"Genre:  {Genre}  \n" +
                   $"Country:  {Country}  \n" +
                   $"BoxOffice:  {BoxOffice}  \n" +
                   $"Actors:  {Actors}  \n" ;
        }

       

    }
}
