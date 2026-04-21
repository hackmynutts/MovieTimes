using MovieTimes.API.DTO;
using MovieTimes.API.Models;
using MovieTimes.API.Repository;

namespace MovieTimes.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }

        public async Task<User> AddUser(UserAddDTO newUser)
        {
            return await _userRepository.AddUser(newUser);
        }
    }
}
