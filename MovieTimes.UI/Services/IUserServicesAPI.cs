using MovieTimes.UI.Models;

namespace MovieTimes.UI.Services
{
    public interface IUserServicesAPI
    {
        Task<List<UserDTO>> GetUsers();
        Task AddUserAsync(UserAddDTO user, CancellationToken cancellation = default);
    }
}
