using Microsoft.EntityFrameworkCore;
using NewMovieAPI.Dto.Movie;
using NewMovieAPI.Models;

namespace NewMovieAPI.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly ApplicationDbContext _context;
        private readonly IGenresService _genresService;

        public MoviesService(ApplicationDbContext context, IGenresService genresService)
        {
            _context = context;
            _genresService = genresService;
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            return await _context.Movies
                         .Include(m => m.Genre)
                         .OrderByDescending(m => m.Rate)
                         .ToListAsync();
        }
        
        public async Task<Movie> GetMovieByIdAsync(int MovieId)
        {
            return await _context.Movies
                .Include(m => m.Genre)
                .SingleOrDefaultAsync(m => m.Id == MovieId);
        }

        public async Task<IEnumerable<Movie>> GetMovieByGenresIdAsync(int GenreId)
        {
            var movies = await _context.Movies
                .Include(m => m.Genre)
                .Where(m => m.GenreId == GenreId)
                .OrderBy(m => m.Genre)
                .ToListAsync();

            return movies;
        }

        public async Task<Movie> CreateMovieAsync(Movie movieDto)
        {
            await _context.Movies.AddAsync(movieDto);

            _context.SaveChanges();

            return movieDto;
        }

        public Movie UpdateMovie(Movie movieDto)
        {
            _context.Movies.Update(movieDto);

            _context.SaveChanges();

            return movieDto;
        }

        public Movie DeleteMovie(Movie movieDto)
        {
            _context.Movies.Remove(movieDto);

            _context.SaveChanges();

            return movieDto;
        }
    }
}
