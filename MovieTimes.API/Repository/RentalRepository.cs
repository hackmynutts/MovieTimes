using Microsoft.EntityFrameworkCore;
using MovieTimes.API.Data;
using MovieTimes.API.Models;

namespace MovieTimes.API.Repository
{
    public class RentalRepository : IRentalRepository
    {
        private readonly AppDbContext _context;

        public RentalRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Rental>> GetAllRentals() =>
            await _context.Rentals
                .Include(r => r.User)
                .Select(r => new Rental
                {
                    Id = r.Id,
                    UserId = r.UserId,
                    MovieId = r.MovieId,
                    MovieTitle = r.MovieTitle,
                    PosterUrl = r.PosterUrl,
                    RentalDate = r.RentalDate,
                    User = r.User
                }).ToListAsync();

        public async Task<List<Rental>> GetRentalsByUser(int userId) =>
            await _context.Rentals
                .Where(r => r.UserId == userId)
                .Select(r => new Rental
                {
                    Id = r.Id,
                    UserId = r.UserId,
                    MovieId = r.MovieId,
                    MovieTitle = r.MovieTitle,
                    PosterUrl = r.PosterUrl,
                    RentalDate = r.RentalDate
                }).ToListAsync();

        public async Task<Rental> AddRental(Rental rental)
        {
            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();
            return rental;
        }
    }
}