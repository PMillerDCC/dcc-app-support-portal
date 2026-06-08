using AppSupportPortal.Web.Models;
using System.Net.Http.Json;

public class ServerApiService
{
    private readonly HttpClient _client;

    public ServerApiService(HttpClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<ServerViewModel>> GetAllAsync()
    {
        return await _client.GetFromJsonAsync<IEnumerable<ServerViewModel>>("api/servers")
               ?? Enumerable.Empty<ServerViewModel>();
    }

    public async Task<ServerViewModel?> GetByIdAsync(int id)
    {
        return await _client.GetFromJsonAsync<ServerViewModel>($"api/servers/{id}");
    }

    public async Task CreateAsync(ServerViewModel model)
    {
        await _client.PostAsJsonAsync("api/servers", model);
    }

    public async Task UpdateAsync(ServerViewModel model)
    {
        await _client.PutAsJsonAsync($"api/servers/{model.ServerId}", model);
    }

    public async Task DeleteAsync(int id)
    {
        await _client.DeleteAsync($"api/servers/{id}");
    }
}
