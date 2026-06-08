using AppSupportPortal.Web.Models;
using System.Net.Http.Json;

namespace AppSupportPortal.Web.Services;

public class NoteApiService
{
    private readonly HttpClient _client;

    public NoteApiService(IHttpClientFactory factory)
    {
        _client = factory.CreateClient("ApiClient");
    }

    public async Task<IEnumerable<NoteViewModel>> GetAllAsync()
    {
        return await _client.GetFromJsonAsync<IEnumerable<NoteViewModel>>("api/notes")
               ?? Enumerable.Empty<NoteViewModel>();
    }

    public async Task<bool> CreateAsync(NoteViewModel model)
    {
        var response = await _client.PostAsJsonAsync("api/notes", model);
        return response.IsSuccessStatusCode;
    }
}
