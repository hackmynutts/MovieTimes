using Microsoft.AspNetCore.Mvc;
using MovieTimes.UI.Models;
using MovieTimes.UI.Services;

namespace MovieTimes.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserServicesAPI _userServicesAPI;

        public UserController(IUserServicesAPI userServicesAPI)
        {
            _userServicesAPI = userServicesAPI;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (email == "admin@retro.com" && password == "admin123")
            {
                var userInfo = new { Id = 1, Nombre = "ADMIN MASTER" };
                TempData["UserLogged"] = System.Text.Json.JsonSerializer.Serialize(userInfo);
                TempData.Keep("UserLogged");
                return RedirectToAction("Index", "Home");
            }

            var usuarios = await _userServicesAPI.GetUsers();

            var usuarioEncontrado = usuarios.FirstOrDefault(u =>
                u.Email.Trim().ToLower() == email.Trim().ToLower());

            if (usuarioEncontrado != null)
            {
                var userInfo = new { Id = usuarioEncontrado.Id, Nombre = usuarioEncontrado.Nombre };
                TempData["UserLogged"] = System.Text.Json.JsonSerializer.Serialize(userInfo);
                TempData.Keep("UserLogged");
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "CREDENCIALES INVÁLIDAS";
            return View();
        }

        public IActionResult Logout()
        {
            TempData.Remove("UserLogged");
            return RedirectToAction("Login");
        }


        public async Task<IActionResult> Index()
        {
            var userList = await _userServicesAPI.GetUsers();
            return View(userList.OrderBy(u => u.Id));
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [Route("User/Create")]
        public async Task<IActionResult> Create(UserAddDTO model)
        {
            try
            {
                if (model == null)
                {
                    TempData["Error"] = "Los datos del usuario son inválidos.";
                    return RedirectToAction("Index");
                }

                await _userServicesAPI.AddUserAsync(model);

                TempData["Success"] = "Usuario creado correctamente.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "No se pudo crear el usuario: " + ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
