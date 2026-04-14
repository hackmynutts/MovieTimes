using MovieTimes.API.DTO;
using MovieTimes.API.Models;
using MovieTimes.API.Repository;

namespace MovieTimes.API.Services
{
    public class MovieServices : IMovieServices
    {
        private readonly IMovieRepository _movieRepository;

        public MovieServices(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        //list
        public async Task<List<MovieDTO>> GetMovies()
        {
            var movies = await _movieRepository.GetMovies();
            return movies.Select(m => new MovieDTO
            {
                id = m.id,
                tmdb_id = m.tmdb_id,
                title = m.title,
                original_title = m.original_title,
                overview = m.overview,
                release_date = m.release_date,
                poster_path = m.poster_path
            }).ToList();
        }

        //get by id
        public async Task<MovieDTO?> GetMovieById(int id)
        {
            var movie = await _movieRepository.GetMovieById(id);
            if (movie == null) return null;
            return new MovieDTO
            {
                id = movie.id,
                tmdb_id = movie.tmdb_id,
                title = movie.title,
                original_title = movie.original_title,
                overview = movie.overview,
                release_date = movie.release_date,
                poster_path = movie.poster_path
            };
        }

        //add
        public async Task<MovieDTO> AddMovie(MovieAddDTO movie)
        {

            var movieEntity = new Movie
            {
                tmdb_id = movie.tmdb_id,
                title = movie.title,
                original_title = movie.original_title,
                overview = movie.overview,
                release_date = movie.release_date,
                poster_path = movie.poster_path
            };
            var newMovie = await _movieRepository.AddMovie(movieEntity);
            return new MovieDTO
            {
                id = newMovie.id,
                tmdb_id = newMovie.tmdb_id,
                title = newMovie.title,
                original_title = newMovie.original_title,
                overview = newMovie.overview,
                release_date = newMovie.release_date,
                poster_path = newMovie.poster_path
            };
        }

        //update
        public async Task<MovieDTO?> UpdateMovie(MovieAddDTO movie)
        {
            var movieEntity = new Movie
            {
                tmdb_id = movie.tmdb_id,
                title = movie.title,
                original_title = movie.original_title,
                overview = movie.overview,
                release_date = movie.release_date,
                poster_path = movie.poster_path
            };
            var newMovie = await _movieRepository.UpdateMovieAsync(movieEntity);
            if (newMovie == null) return null;
            return new MovieDTO
            {
                id = newMovie.id,
                tmdb_id = newMovie.tmdb_id,
                title = newMovie.title,
                original_title = newMovie.original_title,
                overview = newMovie.overview,
                release_date = newMovie.release_date,
                poster_path = newMovie.poster_path
            };
        }

        //delete
        public async Task<bool> DeleteMovie(int id)
        {
            return await _movieRepository.DeleteMovieAsync(id);
        }
    }
}
