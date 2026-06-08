using AppSupportPortal.Web.Models;
using System.Net.Http.Json;

public class UserApiService
{
    private readonly HttpClient _client;

    public UserApiService(HttpClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<UserViewModel>> GetAllAsync()
    {
        return await _client.GetFromJsonAsync<IEnumerable<UserViewModel>>("api/users")
               ?? Enumerable.Empty<UserViewModel>();
    }

    public async Task<UserViewModel?> GetByIdAsync(int id)
    {
        return await _client.GetFromJsonAsync<UserViewModel>($"api/users/{id}");
    }

    public async Task CreateAsync(UserViewModel model)
    {
        await _client.PostAsJsonAsync("api/users", model);
    }

    public async Task UpdateAsync(UserViewModel model)
    {
        await _client.PutAsJsonAsync($"api/users/{model.UserId}", model);
    }

    public async Task DeleteAsync(int id)
    {
        await _client.DeleteAsync($"api/users/{id}");
    }
}
