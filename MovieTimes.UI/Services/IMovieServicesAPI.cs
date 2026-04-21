using MovieTimes.UI.Models;

namespace MovieTimes.UI.Services
{
    public interface IMovieServicesAPI
    {
        Task<List<MovieDTO>> GetMovies();
        Task<MovieDTO?> GetMovieById(int id);
        Task AddMovieAsync(MovieAddDTO movie, CancellationToken cancellation = default);
    }
}
