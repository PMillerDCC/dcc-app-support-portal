using AppSupportPortal.Web.Models.AppSupportPortal.Web.Services;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AppSupportPortal.Web.Models;

public class ServerViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}
This matches the typical Server entity in your API.

2. ServersApiService
If you don’t already have this, create:

Code
AppSupportPortal.Web/Services/ServersApiService.cs
csharp
using AppSupportPortal.Web.Models;
using System.Net.Http.Json;

namespace AppSupportPortal.Web.Services
{
    public interface IServersApiService
    {
        Task<IEnumerable<ServerViewModel>> GetAllAsync();
        Task<ServerViewModel?> GetByIdAsync(int id);
        Task<bool> CreateAsync(ServerViewModel model);
        Task<bool> UpdateAsync(ServerViewModel model);
        Task<bool> DeleteAsync(int id);
    }

    public class ServersApiService : IServersApiService
    {
        private readonly HttpClient _http;

        public ServersApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<ServerViewModel>> GetAllAsync()
            => await _http.GetFromJsonAsync<IEnumerable<ServerViewModel>>("api/servers")
               ?? Enumerable.Empty<ServerViewModel>();

        public async Task<ServerViewModel?> GetByIdAsync(int id)
            => await _http.GetFromJsonAsync<ServerViewModel>($"api/servers/{id}");

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

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/servers/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
