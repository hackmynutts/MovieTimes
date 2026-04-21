using MovieTimes.UI.Models;
using System.Text.Json;

namespace MovieTimes.UI.Services
{
    public class UserServicesAPI : IUserServicesAPI
    {
        private readonly HttpClient _httpClient;

        public UserServicesAPI(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //listar usuarios
        public async Task<List<UserDTO>> GetUsers()
        {
            try
            {
                var response = await _httpClient.GetAsync("User");

                if (response.IsSuccessStatusCode)
                {
                    var users = await response.Content.ReadFromJsonAsync<List<UserDTO>>();
                    return users ?? new List<UserDTO>();
                }

                return new List<UserDTO>();
            }
            catch (Exception ex)
            {
                return new List<UserDTO>();
            }
        }

        //agregar usuario
        public async Task AddUserAsync(UserAddDTO user, CancellationToken cancellation = default)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("User", user);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Error al agregar el usuario");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el usuario: " + ex.Message);
            }
        }
    }
}
