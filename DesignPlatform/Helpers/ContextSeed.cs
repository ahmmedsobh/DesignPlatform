using DesignPlatform.Data;
using DesignPlatform.Enums;
using DesignPlatform.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace DesignPlatform.Helpers
{
    public static class ContextSeed
    {
        public static async Task Seed(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext applicationDbContext)
        {
            await CreateRoles(roleManager);
            await CreateBasicAdmin(userManager);
            await CreateBasicSettings(applicationDbContext);
        }

        private static async Task CreateRoles(RoleManager<IdentityRole> roleManager)
        {
            List<IdentityRole> identityRoles = new List<IdentityRole>();
            foreach (Roles role in (Roles[])Roles.GetValues(typeof(Roles)))
            {
                await roleManager.CreateAsync(new IdentityRole() { Name = role.ToString() });
            }
        }

        private static async Task CreateBasicAdmin(UserManager<ApplicationUser> userManager)
        {
            var users = new List<ApplicationUser>()
            {
                new ApplicationUser
                {
                    // ادمن لوحه التحكم
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                    FirstName = "ahmed",
                    LastName = "sobh",
                    //Image = "https://upload.wikimedia.org/wikipedia/commons/7/72/Default-welcomer.png",
                    //IsActive = true,
                    //CreationDate = DateHelper.Date,
                    PhoneNumber = "100100100",
                },


            };

            foreach (ApplicationUser user in users)
            {
                var userFound = await userManager.FindByEmailAsync(user.Email);
                if (userFound == null)
                {
                    var addUserResult = await userManager.CreateAsync(user, "123456");
                    if (addUserResult.Succeeded)
                        await userManager.AddToRoleAsync(user, Roles.Admin.ToString());
                }
            }
        }

        

        private static async Task CreateBasicSettings(ApplicationDbContext context)
        {
            var settings = await context.Settings.FirstOrDefaultAsync();
            if (settings == null)
            {
                settings = new Setting()
                {
                    LogoPath = ""
                };

                await context.AddAsync(settings);
                await context.SaveChangesAsync();

            }
        }
    }
}
