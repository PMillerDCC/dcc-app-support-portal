using AppSupportPortal.Web.Models;
using System.Net.Http.Json;

namespace AppSupportPortal.Web.Services
{
    public class NotesApiService : INotesApiService
    {
        private readonly HttpClient _http;

        public NotesApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<NoteViewModel>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<IEnumerable<NoteViewModel>>("api/notes")
                   ?? Enumerable.Empty<NoteViewModel>();
        }

        public async Task<IEnumerable<NoteViewModel>> GetByApplicationAsync(int applicationId)
        {
            return await _http.GetFromJsonAsync<IEnumerable<NoteViewModel>>($"api/notes/by-application/{applicationId}")
                   ?? Enumerable.Empty<NoteViewModel>();
        }

        public async Task<NoteViewModel?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<NoteViewModel>($"api/notes/{id}");
        }

        public async Task<bool> CreateAsync(NoteViewModel model)
        {
            var response = await _http.PostAsJsonAsync("api/notes", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(NoteViewModel model)
        {
            var response = await _http.PutAsJsonAsync($"api/notes/{model.Id}", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/notes/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}