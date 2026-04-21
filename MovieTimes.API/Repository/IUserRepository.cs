using MovieTimes.API.DTO;
using MovieTimes.API.Models;

namespace MovieTimes.API.Repository
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsers();
        Task<User> AddUser(UserAddDTO newUser);
    }
}
