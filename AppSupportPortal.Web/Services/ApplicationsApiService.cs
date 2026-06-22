using AppSupportPortal.Web.Models;
using AppSupportPortal.Web.Models.Dtos;

namespace AppSupportPortal.Web.Services
{
    public class ApplicationsApiService : IApplicationsApiService
    {
        private readonly HttpClient _http;

        public ApplicationsApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<ApplicationViewModel>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<IEnumerable<ApplicationViewModel>>("api/applications")
                   ?? Enumerable.Empty<ApplicationViewModel>();
        }

        public async Task<ApplicationViewModel?> GetByIdAsync(int id)
        {
            var response = await _http.GetAsync($"api/applications/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<ApplicationViewModel>();
        }

        public async Task<bool> CreateAsync(ApplicationViewModel model)
        {
            var response = await _http.PostAsJsonAsync("api/applications", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(ApplicationViewModel model)
        {
            var dto = new ApplicationUpdateDto
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                ServerId = model.ServerId.Value
            };

            var response = await _http.PutAsJsonAsync($"api/applications/{model.Id}", dto);

            return response.IsSuccessStatusCode;
        }

        public async Task<string?> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/applications/{id}");

            if (response.IsSuccessStatusCode)
                return null; // success

            // read error message if API returns one
            var error = await response.Content.ReadAsStringAsync();
            return string.IsNullOrWhiteSpace(error) ? "Failed to delete application." : error;
        }
    }
}
