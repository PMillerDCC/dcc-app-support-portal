using AppSupportPortal.Web.Models;
using System.Net.Http.Json;

namespace AppSupportPortal.Web.Services;

public class ApplicationApiService
{
    private readonly HttpClient _client;

    public ApplicationApiService(IHttpClientFactory factory)
    {
        _client = factory.CreateClient("ApiClient");
    }

    public async Task<IEnumerable<ApplicationViewModel>> GetAllAsync()
    {
        return await _client.GetFromJsonAsync<IEnumerable<ApplicationViewModel>>("api/applications")
               ?? Enumerable.Empty<ApplicationViewModel>();
    }

    public async Task<ApplicationViewModel?> GetByIdAsync(int id)
    {
        return await _client.GetFromJsonAsync<ApplicationViewModel>($"api/applications/{id}");
    }

    public async Task<bool> CreateAsync(ApplicationViewModel model)
    {
        var response = await _client.PostAsJsonAsync("api/applications", model);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateAsync(int id, ApplicationViewModel model)
    {
        var response = await _client.PutAsJsonAsync($"api/applications/{id}", model);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var response = await _client.DeleteAsync($"api/applications/{id}");
        return response.IsSuccessStatusCode;
    }
}
