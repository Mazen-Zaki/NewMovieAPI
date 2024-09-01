namespace NewMovieAPI.Dto.Movie
{
    public class UpdateMovieDto
    {
        public string? Title { get; set; }
        public double? Rate { get; set; }
        public int GenreId { get; set; }
        public IFormFile? Poster { get; set; }
    }
}
