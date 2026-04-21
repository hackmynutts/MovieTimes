using MovieTimes.UI.Models;

namespace MovieTimes.UI.Services
{
    public interface IRentalServicesAPI
    {
        Task<List<RentalDTO>> GetAllRentals();
        Task<List<RentalDTO>> GetRentalsByUser(int userId);
        Task AddRentalAsync(RentalDTO rental);
    }
}