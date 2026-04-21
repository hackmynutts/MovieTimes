using MovieTimes.API.Models;
using MovieTimes.API.Repository;

namespace MovieTimes.API.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;

        public RentalService(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public async Task<List<Rental>> GetAllRentals() =>
            await _rentalRepository.GetAllRentals();

        public async Task<List<Rental>> GetRentalsByUser(int userId) =>
            await _rentalRepository.GetRentalsByUser(userId);

        public async Task<Rental> AddRental(Rental rental) =>
            await _rentalRepository.AddRental(rental);
    }
}