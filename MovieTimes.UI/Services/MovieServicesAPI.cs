using MovieTimes.UI.Models;

namespace MovieTimes.UI.Services
{
    public class MovieServicesAPI : IMovieServicesAPI
    {
        private readonly HttpClient _httpClient;

        public MovieServicesAPI(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //listar peliculas
        public async Task<List<MovieDTO>> GetMovies()
        {
            try
            {
                var response = await _httpClient.GetAsync("Movie");
                if (response.IsSuccessStatusCode)
                {
                    var movies = await response.Content.ReadFromJsonAsync<List<MovieDTO>>();
                    return movies ?? new List<MovieDTO>();
                }
                return new List<MovieDTO>();
            }
            catch (Exception ex)
            {
                return new List<MovieDTO>();
            }
        }

        //peliculas por id
        public async Task<MovieDTO?> GetMovieById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"Movie/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var movie = await response.Content.ReadFromJsonAsync<MovieDTO>();
                    return movie;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //agregar pelicula
        public async Task AddMovieAsync(MovieAddDTO movie, CancellationToken cancellation = default)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Movie", movie, cancellation);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Error al agregar la película");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar la película: " + ex.Message);
            }
        }
    }
}
