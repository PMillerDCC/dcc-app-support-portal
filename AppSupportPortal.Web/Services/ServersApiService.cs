using AppSupportPortal.Web.Models;
using System.Net.Http.Json;

namespace AppSupportPortal.Web.Services
{
    public class ServersApiService : IServersApiService
    {
        private readonly HttpClient _http;

        public ServersApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<ServerViewModel>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<IEnumerable<ServerViewModel>>("api/servers")
                   ?? Enumerable.Empty<ServerViewModel>();
        }

        public async Task<ServerViewModel?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<ServerViewModel>($"api/servers/{id}");
        }

        public async Task<bool> CreateAsync(ServerViewModel model)
        {
            var response = await _http.PostAsJsonAsync("api/servers", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(ServerViewModel model)
        {
            var response = await _http.PutAsJsonAsync($"api/servers/{model.Id}", model);
            return response.IsSuccessStatusCode;
        }

        // Returns null on success, or an error message on failure
        public async Task<string?> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/servers/{id}");

            if (response.IsSuccessStatusCode)
                return null; // success

            var error = await response.Content.ReadAsStringAsync();
            return string.IsNullOrWhiteSpace(error) ? "Failed to delete server." : error;
        }
    }
}
