using System.ComponentModel.DataAnnotations;

namespace _252936_MVC.Models
{
    public class Movie
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Film musi posiadac tytul")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Film musi posiadac gatunek")]
        public string Genre { get; set; }
        [Required(ErrorMessage = "Film musi posiadac autora")]
        public string Author { get; set; }


    }
}
