using MovieTimes.UI.Models;

namespace MovieTimes.UI.Services
{
    public class RentalServicesAPI : IRentalServicesAPI
    {
        private readonly HttpClient _httpClient;

        public RentalServicesAPI(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Todas las reservas (admin)
        public async Task<List<RentalDTO>> GetAllRentals()
        {
            try
            {
                var response = await _httpClient.GetAsync("Rental/flat");
                if (response.IsSuccessStatusCode)
                {
                    var rentals = await response.Content.ReadFromJsonAsync<List<RentalDTO>>();
                    return rentals ?? new List<RentalDTO>();
                }
                return new List<RentalDTO>();
            }
            catch
            {
                return new List<RentalDTO>();
            }
        }

        // Reservas por usuario (usuario regular)
        public async Task<List<RentalDTO>> GetRentalsByUser(int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"Rental/user/{userId}/flat");
                if (response.IsSuccessStatusCode)
                {
                    var rentals = await response.Content.ReadFromJsonAsync<List<RentalDTO>>();
                    return rentals ?? new List<RentalDTO>();
                }
                return new List<RentalDTO>();
            }
            catch
            {
                return new List<RentalDTO>();
            }
        }

        // Agregar reserva
        public async Task AddRentalAsync(RentalDTO rental)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Rental", rental);
                if (!response.IsSuccessStatusCode)
                    throw new Exception("Error al guardar la reserva");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar la reserva: " + ex.Message);
            }
        }
    }
}