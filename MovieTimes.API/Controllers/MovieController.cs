using Microsoft.AspNetCore.Mvc;
using MovieTimes.API.DTO;
using MovieTimes.API.Services;

namespace MovieTimes.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieServices _moviesBL;
        public MovieController(IMovieServices moviesBL)
        {
            _moviesBL = moviesBL;
        }

        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            var movies = await _moviesBL.GetMovies();
            return Ok(movies);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var movie = await _moviesBL.GetMovieById(id);
            if (movie == null) return NotFound();
            return Ok(movie);
        }
        [HttpPost]
        public async Task<IActionResult> AddMovie([FromBody] MovieAddDTO movie)
        {
            if (movie == null) return BadRequest();

            var newMovie = await _moviesBL.AddMovie(movie);

            return Ok(newMovie);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var result = await _moviesBL.DeleteMovie(id);
            if (!result) return NotFound();
            return NoContent();
        }

    }
}
