using Microsoft.AspNetCore.Mvc;
using MovieTimes.UI.Models;
using MovieTimes.UI.Services;
using System.Text.Json;

namespace MovieTimes.UI.Controllers
{
    public class RentalController : Controller
    {
        private readonly IRentalServicesAPI _rentalService;

        public RentalController(IRentalServicesAPI rentalService)
        {
            _rentalService = rentalService;
        }

        public async Task<IActionResult> Index()
        {
            var userJson = TempData.Peek("UserLogged")?.ToString();
            if (string.IsNullOrEmpty(userJson))
                return RedirectToAction("Login", "User");

            var user = JsonSerializer.Deserialize<JsonElement>(userJson);
            var userId = user.GetProperty("Id").GetInt32();
            var userName = user.GetProperty("Nombre").GetString();

            List<RentalDTO> rentals;

           
            if (userName == "ADMIN MASTER")
            {
                rentals = await _rentalService.GetAllRentals();
                ViewBag.IsAdmin = true;
            }
            else
            {
                rentals = await _rentalService.GetRentalsByUser(userId);
                ViewBag.IsAdmin = false;
            }

            ViewBag.UserName = userName;
            return View(rentals);
        }
    }
}