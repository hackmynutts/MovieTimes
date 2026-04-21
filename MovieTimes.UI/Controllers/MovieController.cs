using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTimes.UI.Models;
using MovieTimes.UI.Services;

namespace MovieTimes.UI.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieServicesAPI _movieServicesAPI;
        private readonly IUserServicesAPI _userServicesAPI;
        public MovieController(IMovieServicesAPI movieServicesAPI, IUserServicesAPI userServicesAPI)
        {
            _movieServicesAPI = movieServicesAPI;
            _userServicesAPI = userServicesAPI;
        }
        // GET: MovieController
        public ActionResult Index()
        {
            List<MovieDTO> movies = _movieServicesAPI.GetMovies().Result;
            List<UserDTO> users = _userServicesAPI.GetUsers().Result;
            
            return View(movies);
        }

        // GET: MovieController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            MovieDTO movie = await _movieServicesAPI.GetMovieById(id);
            return View(movie);
        }


        // POST: MovieController/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MovieAddDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest();
                await _movieServicesAPI.AddMovieAsync(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: MovieController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MovieController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MovieController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MovieController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
