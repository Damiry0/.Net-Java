using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace FetchAPI
{
    [Table("Movies")]
    public class Movie
    { 
        [Column("CustomerID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; } 
        [Column("Title")]
        public string Title { get; set; }
        [Column("Year")]

        public string Year { get; set; }
        [Column("Rated")]

        public string Rated { get; set; }
        [Column("Released")]

        public string Released { get; set; }
        [Column("Runtime")]

        public string Runtime { get; set; }
        [Column("Genre")]

        public string Genre { get; set; }
        [Column("Writer")]

        public string Writer { get; set; }
        [Column("Actors")]

        public string Actors { get; set; }
        [Column("Plot")]

        public string Plot { get; set; }
        [Column("Country")]

        public string Country { get; set; }
        [Column("Awards")]

        public string Awards { get; set; }
        [Column("Poster")]

        public string Poster { get; set; }
        [Column("Metascore")]

        public string Metascore { get; set; }
        [Column("imdbRating")]
        public string imdbRating { get; set; }
        [Column("imdbVotes")]

        public string imdbVotes { get; set; }
        [Column("BoxOffice")]

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
