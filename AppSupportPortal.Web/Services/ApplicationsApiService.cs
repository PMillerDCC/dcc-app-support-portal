using AppSupportPortal.Web.Models;

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
            return await _http.GetFromJsonAsync<ApplicationViewModel>($"api/applications/{id}");
        }

        public async Task<bool> CreateAsync(ApplicationViewModel model)
        {
            var response = await _http.PostAsJsonAsync("api/applications", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(ApplicationViewModel model)
        {
            var response = await _http.PutAsJsonAsync($"api/applications/{model.Id}", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/applications/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
