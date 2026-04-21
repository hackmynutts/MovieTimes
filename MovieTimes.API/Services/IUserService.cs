using MovieTimes.API.DTO;
using MovieTimes.API.Models;

namespace MovieTimes.API.Services
{
    public interface IUserService
    {
        Task<List<User>> GetUsers();
        Task<User> AddUser(UserAddDTO newUser);
    }
}
