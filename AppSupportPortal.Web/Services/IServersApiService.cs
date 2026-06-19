using AppSupportPortal.Web.Models;

namespace AppSupportPortal.Web.Services
{
    public interface IServersApiService
    {
        Task<IEnumerable<ServerViewModel>> GetAllAsync();
        Task<ServerViewModel?> GetByIdAsync(int id);
        Task<bool> CreateAsync(ServerViewModel model);
        Task<bool> UpdateAsync(ServerViewModel model);

        // Returns null on success, or an error message on failure
        Task<string?> DeleteAsync(int id);
    }
}