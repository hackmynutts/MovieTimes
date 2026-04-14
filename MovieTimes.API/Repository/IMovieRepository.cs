using MovieTimes.API.Models;

namespace MovieTimes.API.Repository
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetMovies();
        Task<Movie?> GetMovieById(int id);
        Task<Movie> AddMovie(Movie movie);
        Task<Movie?> UpdateMovieAsync(Movie movie);
        Task<bool> DeleteMovieAsync(int id);
    }
}
