using DesignPlatform.Models;

namespace DesignPlatform.Helpers.CurrentUserService
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        string GetLang();
        Task<ApplicationUser> GetUser(string userId);
        Task<ApplicationUser> GetUser();
    }
}
