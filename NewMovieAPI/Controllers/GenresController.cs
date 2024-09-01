using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewMovieAPI.Dto.Genre;
using NewMovieAPI.Models;
using NewMovieAPI.Services;

namespace NewMovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenresService _genresService;

        public GenresController(IGenresService genresService)
        {
            _genresService = genresService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var genres = await _genresService.GetAllGenresAsync();

            return Ok(genres);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int Id)
        {
            var genre = await _genresService.GetGenreByIdAsync(Id);

            if (genre == null)
            {
                return NotFound($"No genre found for this id = {Id}");
            }

            return Ok(genre);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateGenreDto dto)
        {
            var genre = new Genre
            {
                Name = dto.Name
            };

            await _genresService.CreateGenreAsync(genre);

            return Ok(genre);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(byte id, [FromBody] UpdateGenreDto dto)
        {
            var genre = await _genresService.GetGenreByIdAsync(id);

            if (genre == null)
            {
                return NotFound($"No genre found for this id = {id}");
            }

            genre.Name = dto.Name;

            _genresService.UpdateGenreAsync(genre);

            return Ok(genre);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(byte id)
        {
            var genre = await _genresService.GetGenreByIdAsync(id);

            if (genre == null)
            {
                return NotFound($"No genre found for this id = {id}");
            }

            _genresService.DeleteGenreAsync(genre);

            return Ok(genre);
        }
    }
}
