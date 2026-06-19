using AppSupportPortal.Web.Models;

namespace AppSupportPortal.Web.Services
{
    public interface IUsersApiService
    {
        Task<IEnumerable<UserViewModel>> GetAllAsync();
        Task<UserViewModel?> GetByIdAsync(int id);
        Task<bool> CreateAsync(UserViewModel model);
        Task<bool> UpdateAsync(UserViewModel model);
        Task<string?> DeleteAsync(int id);
    }
}
