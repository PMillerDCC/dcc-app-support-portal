using AppSupportPortal.Web.Models;
using System.Net.Http.Json;

namespace AppSupportPortal.Web.Services
{
    public class UsersApiService : IUsersApiService
    {
        private readonly HttpClient _http;

        public UsersApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<IEnumerable<UserViewModel>>("api/users")
                   ?? Enumerable.Empty<UserViewModel>();
        }

        public async Task<UserViewModel?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<UserViewModel>($"api/users/{id}");
        }

        public async Task<bool> CreateAsync(UserViewModel model)
        {
            var response = await _http.PostAsJsonAsync("api/users", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(UserViewModel model)
        {
            var response = await _http.PutAsJsonAsync($"api/users/{model.Id}", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<string?> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/users/{id}");

            if (response.IsSuccessStatusCode)
                return null;

            // Return API error message for display in TempData
            return await response.Content.ReadAsStringAsync();
        }
    }
}
