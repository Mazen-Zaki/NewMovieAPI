namespace NewMovieAPI.Dto.Movie
{
    public class CreateMovieDto
    {
        public string Title { get; set; }
        public double Rate { get; set; }
        public IFormFile Poster { get; set; }
        public int GenreId { get; set; }
    }
}
