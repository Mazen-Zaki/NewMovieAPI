using Microsoft.EntityFrameworkCore;
using NewMovieAPI.Models;

namespace NewMovieAPI.Services
{
    public class GenresService : IGenresService
    {
        private readonly ApplicationDbContext _context;

        public GenresService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genre>> GetAllGenresAsync()
        {
            return await _context.Genres.OrderBy(g => g.Name).ToListAsync();
        }

        public async Task<Genre> CreateGenreAsync(Genre genreDto)
        {
            await _context.Genres.AddAsync(genreDto);
            _context.SaveChanges();

            return genreDto;
        }

        public async Task<Genre> GetGenreByIdAsync(int GenreId)
        {
            return await _context.Genres.SingleOrDefaultAsync(g => g.Id == GenreId);
        }

        public Task<bool> IsValidGenreId(int GenreId)
        {
            return _context.Genres.AnyAsync(g => g.Id == GenreId);
        }

        public Genre UpdateGenreAsync(Genre genreDto)
        {
            _context.Update(genreDto);
            _context.SaveChanges();

            return genreDto;
        }

        public Genre DeleteGenreAsync(Genre genreDto)
        {
            _context.Remove(genreDto);
            _context.SaveChanges();

            return genreDto;
        }
        
    }
}
