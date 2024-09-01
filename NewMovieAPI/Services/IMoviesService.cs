using NewMovieAPI.Models;

namespace NewMovieAPI.Services
{
    public interface IMoviesService
    {
        Task<IEnumerable<Movie>> GetAllMoviesAsync();

        Task<Movie> GetMovieByIdAsync(int MovieId);
        Task<IEnumerable<Movie>> GetMovieByGenresIdAsync(int GenreId);

        Task<Movie> CreateMovieAsync(Movie movieDto);

        Movie UpdateMovie(Movie movieDto);
        Movie DeleteMovie(Movie movieDto);
    }
}
