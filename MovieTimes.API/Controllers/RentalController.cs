using Microsoft.AspNetCore.Mvc;
using MovieTimes.API.Models;
using MovieTimes.API.Services;

namespace MovieTimes.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalController : ControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentalController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _rentalService.GetAllRentals());

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId) =>
            Ok(await _rentalService.GetRentalsByUser(userId));

        [HttpPost]
        public async Task<IActionResult> AddRental([FromBody] Rental rental)
        {
            var result = await _rentalService.AddRental(rental);
            return Ok(result);
     
        }
        [HttpGet("flat")]
        public async Task<IActionResult> GetAllFlat()
        {
            var rentals = await _rentalService.GetAllRentals();
            var result = rentals.Select(r => new
            {
                id = r.Id,
                userId = r.UserId,
                userName = r.User != null ? r.User.Nombre + " " + r.User.Apellido : "Desconocido",
                movieId = r.MovieId,
                movieTitle = r.MovieTitle,
                posterUrl = r.PosterUrl,
                rentalDate = r.RentalDate
            });
            return Ok(result);
        }

        [HttpGet("user/{userId}/flat")]
        public async Task<IActionResult> GetByUserFlat(int userId)
        {
            var rentals = await _rentalService.GetRentalsByUser(userId);
            var result = rentals.Select(r => new
            {
                id = r.Id,
                userId = r.UserId,
                userName = "",
                movieId = r.MovieId,
                movieTitle = r.MovieTitle,
                posterUrl = r.PosterUrl,
                rentalDate = r.RentalDate
            });
            return Ok(result);
        }
    }
}