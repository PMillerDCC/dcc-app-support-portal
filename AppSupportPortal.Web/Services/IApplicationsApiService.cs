using AppSupportPortal.Web.Models;

namespace AppSupportPortal.Web.Services
{
    public interface IApplicationsApiService
    {
        Task<IEnumerable<ApplicationViewModel>> GetAllAsync();
        Task<ApplicationViewModel?> GetByIdAsync(int id);
        Task<bool> CreateAsync(ApplicationViewModel model);
        Task<bool> UpdateAsync(ApplicationViewModel model);
        Task<bool> DeleteAsync(int id);
    }
}
