using System.ComponentModel.DataAnnotations.Schema;

namespace NewMovieAPI.Models
{
    public class Movie
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public double Rate { get; set; }
        public byte[] Poster { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
