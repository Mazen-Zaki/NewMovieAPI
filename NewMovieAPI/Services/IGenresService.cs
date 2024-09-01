using NewMovieAPI.Models;

namespace NewMovieAPI.Services
{
    public interface IGenresService
    {
        Task<IEnumerable<Genre>> GetAllGenresAsync();
        Task<Genre> GetGenreByIdAsync(int GenreId);
        Task<Genre> CreateGenreAsync(Genre genreDto);
        Task<bool> IsValidGenreId(int GenreId);
        Genre UpdateGenreAsync(Genre genreDto);
        Genre DeleteGenreAsync(Genre genreDto);
    }
}
