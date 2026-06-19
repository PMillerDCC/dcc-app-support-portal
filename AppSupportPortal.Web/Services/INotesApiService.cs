using AppSupportPortal.Web.Models;

namespace AppSupportPortal.Web.Services
{
    public interface INotesApiService
    {
        Task<IEnumerable<NoteViewModel>> GetAllAsync();
        Task<IEnumerable<NoteViewModel>> GetByApplicationAsync(int applicationId);
        Task<NoteViewModel?> GetByIdAsync(int id);
        Task<bool> CreateAsync(NoteViewModel model);
        Task<bool> UpdateAsync(NoteViewModel model);
        Task<bool> DeleteAsync(int id);
    }
}
