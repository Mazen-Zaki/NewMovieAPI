namespace NewMovieAPI.Dto.Movie
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Rate { get; set; }
        public int GenreId { get; set; }
        public string GenreName { get; set; }
        public byte[] Poster { get; set; }
    }
}
