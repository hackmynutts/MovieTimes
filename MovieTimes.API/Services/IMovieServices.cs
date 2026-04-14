using MovieTimes.API.DTO;
using MovieTimes.API.Models;

namespace MovieTimes.API.Services
{
    public interface IMovieServices
    {
        Task<List<MovieDTO>> GetMovies();
        Task<MovieDTO?> GetMovieById(int id);
        Task<MovieDTO> AddMovie(MovieAddDTO movie);
        Task<MovieDTO?> UpdateMovie(MovieAddDTO movie);
        Task<bool> DeleteMovie(int id);
    }
}
