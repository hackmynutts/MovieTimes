using MovieTimes.API.Models;

namespace MovieTimes.API.Repository
{
    public interface IRentalRepository
    {
        Task<List<Rental>> GetAllRentals();
        Task<List<Rental>> GetRentalsByUser(int userId);
        Task<Rental> AddRental(Rental rental);
    }
}