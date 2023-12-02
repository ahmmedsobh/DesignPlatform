using DesignPlatform.Data;
using DesignPlatform.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DesignPlatform.Helpers.CurrentUserService
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext context;
        public int MyProperty { get; set; }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            this.context = context;
        }

        public string UserId
        {
            get
            {
                var id = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
                if (string.IsNullOrEmpty(id))
                {
                    id = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
                }

                return id;
            }
        }


        public string GetLang()
        {
            var lang = _httpContextAccessor.HttpContext?.Request.Headers["Lang"];
            return !string.IsNullOrEmpty(lang) ? lang : "en";
        }

        public async Task<ApplicationUser> GetUser(string userId)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            return user;
        }

        public async Task<ApplicationUser> GetUser()
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == UserId);
            return user;
        }
    }
}
