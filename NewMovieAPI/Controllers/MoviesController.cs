using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewMovieAPI.Dto.Movie;
using NewMovieAPI.Models;
using NewMovieAPI.Services;

namespace NewMovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService _moviesService;
        private readonly IGenresService _genresService;
        private readonly IMapper _mapper;

        private new List<String> _allowedExtenstions = new List<string> { ".jpg", ".png"};
        private new long _fileSizeLimit = 1048576;

        public MoviesController(IMoviesService moviesService, IGenresService genresService, IMapper mapper)
        {
            _moviesService = moviesService;
            _genresService = genresService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMoviesAsync()
        {
            var movies = await _moviesService.GetAllMoviesAsync();

            var data = _mapper.Map<IEnumerable<MovieDto>>(movies);

            return Ok(data);
        }

        [HttpGet("{MovieId}")]
        public async Task<IActionResult> GetMovieByIdAsync(int MovieId)
        {
            var movie = await _moviesService.GetMovieByIdAsync(MovieId);

            if (movie == null)
            {
                return NotFound($"there is no movie for that id = {MovieId}");
            }

            var movieData = _mapper.Map<MovieDto>(movie);

            return Ok(movieData);
        }

        [HttpGet("GetMoviesByGenreId")]
        public async Task<IActionResult> GetMoviesByGenreIdAsync(int genreId)
        {
            var movies = await _moviesService.GetMovieByGenresIdAsync(genreId);

            var data = _mapper.Map<IEnumerable<MovieDto>>(movies);

            return Ok(data);
        }



        [HttpPost]
        public async Task<IActionResult> CreateMovieAsync([FromForm] CreateMovieDto createMovieDto)
        {
            if (createMovieDto.Poster.Length > _fileSizeLimit)
            {
                return BadRequest("File size is too large, Max 1MB");
            }

            if (!_allowedExtenstions.Contains(Path.GetExtension(createMovieDto.Poster.FileName)))
            {
                return BadRequest("Invalid file type, allowed (png & jpg)");
            }

            var isValidGenre = await _genresService.IsValidGenreId(createMovieDto.GenreId);

            if (!isValidGenre)
            {
                return BadRequest("Invalid genre id, no id for this id");
            }

            using var dataStream = new MemoryStream();

            await createMovieDto.Poster.CopyToAsync(dataStream);

            var movie = _mapper.Map<Movie>(createMovieDto);
            movie.Poster = dataStream.ToArray();

            await _moviesService.CreateMovieAsync(movie);

            return Ok(movie);
        }

        [HttpPatch("{movieId}")]
        public async Task<IActionResult> UpdateMovieAsync(int movieId, [FromForm] UpdateMovieDto updateMovieDto)
        {
            var movie = await _moviesService.GetMovieByIdAsync(movieId);

            if (movie == null)
            {
                return NotFound("There is no movie for that id");
            }

            if (updateMovieDto.Poster != null)
            {
                if (updateMovieDto.Poster.Length > _fileSizeLimit)
                {
                    return BadRequest("File size is too large, Max 1MB");
                }

                if (!_allowedExtenstions.Contains(Path.GetExtension(updateMovieDto.Poster.FileName)))
                {
                    return BadRequest("Invalid file type, allowed (png & jpg)");
                }

                using var dataStream = new MemoryStream();

                await updateMovieDto.Poster.CopyToAsync(dataStream);

                movie.Poster = dataStream.ToArray();
            }


            var isValidGenre = await _genresService.IsValidGenreId(updateMovieDto.GenreId);

            if (isValidGenre)
            {
                movie.GenreId = (int)updateMovieDto.GenreId;
            }

            if (updateMovieDto.Title != null)
            {
                movie.Title = (string)updateMovieDto.Title;
            }

            if (updateMovieDto.Rate != null)
            {
                movie.Rate = (double)updateMovieDto.Rate;
            }

            
            _moviesService.UpdateMovie(movie);

            return Ok(movie);
        }

        [HttpDelete("{movieId}")]
        public async Task<IActionResult> DeleteMovieAsync(int movieId)
        {
            var movie = await _moviesService.GetMovieByIdAsync(movieId);

            if (movie == null)
            {
                return NotFound("There is no movie for that id");
            }

            _moviesService.DeleteMovie(movie);

            return Ok(movie);
        }



    }
}
