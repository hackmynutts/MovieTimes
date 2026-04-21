using Microsoft.AspNetCore.Mvc;
using MovieTimes.API.DTO;
using MovieTimes.API.Services;

namespace MovieTimes.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsers();
            return Ok(users);
        }
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserAddDTO user)
        {
            if (user == null) return BadRequest();
            var newUser = await _userService.AddUser(user);
            return Ok(newUser);
        }
    }

}
