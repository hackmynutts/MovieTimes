using MovieTimes.API.Models;

namespace MovieTimes.API.Services
{
    public interface IRentalService
    {
        Task<List<Rental>> GetAllRentals();
        Task<List<Rental>> GetRentalsByUser(int userId);
        Task<Rental> AddRental(Rental rental);
    }
}