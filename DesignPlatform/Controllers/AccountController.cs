using DesignPlatform.Data;
using DesignPlatform.Enums;
using DesignPlatform.Extensions;
using DesignPlatform.Helpers.CurrentUserService;
using DesignPlatform.Models;
using DesignPlatform.ViewModels;
using DesignPlatform.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace DesignPlatform.Controllers
{
    [AuthorizeRoles(Roles.Client)]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly ICurrentUserService currentUserService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(ApplicationDbContext context , ICurrentUserService currentUserService , UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            this.context = context;
            this.currentUserService = currentUserService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> Profile()
        {
            var userId = currentUserService.UserId;
            var user = await context.Users.Where(i => i.Id == userId).Select(i => new AccountProfileViewModel()
            {
                FirstName = i.FirstName,
                LastName = i.LastName,
                Address = i.Address,
                City = i.City,
                ZipCode = i.ZipCode,
                Email = i.Email,
                Phone = i.PhoneNumber,
                CountryId = i.CountryId == null ? 0 : (int)i.CountryId,
                StateId = i.StateId == null ? 0 : (int)i.StateId,
                MainPackagesPurchased = context.ProjectPackages.Where(pp=> i.ClientProjects.Any(cp=> cp.Id == pp.ProjectId)).Select(i=> i.Package.Name).ToList(),
                SubPackagesPurchased = context.ProjectSubPackages.Where(pp=> i.ClientProjects.Any(cp=> cp.Id == pp.ProjectId)).Select(i=> i.SubPackage.Name).ToList(),

            }).FirstOrDefaultAsync();

            if(user != null)
            {
                user.Countries = await Countries(user.CountryId);
                user.States = await States(user.CountryId);
            }


            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Profile(AccountProfileViewModel model)
        {
            var userId = currentUserService.UserId;
            var user = await context.Users.Include(i => i.ClientProjects).FirstOrDefaultAsync(i => i.Id == userId);

            if (ModelState.IsValid)
            {

                if (user == null)
                {
                    return BadRequest();
                }

                user.FirstName = !string.IsNullOrEmpty(model.FirstName) ? model.FirstName : user.FirstName;
                user.LastName = !string.IsNullOrEmpty(model.LastName) ? model.LastName : user.LastName;
                user.PhoneNumber = !string.IsNullOrEmpty(model.Phone) ? model.Phone : user.PhoneNumber;
                user.Email = !string.IsNullOrEmpty(model.Email) ? model.Email : user.Email;
                user.Address = !string.IsNullOrEmpty(model.Address) ? model.Address : user.Address;
                user.City = !string.IsNullOrEmpty(model.City) ? model.City : user.City;
                user.ZipCode = !string.IsNullOrEmpty(model.ZipCode) ? model.ZipCode : user.ZipCode;
                user.CountryId = model.CountryId != 0 ? model.CountryId : user.CountryId;
                user.StateId = model.StateId != 0 ? model.StateId : user.StateId;

                context.Update(user);
                var Result = await context.SaveChangesAsync() > 0;
            }

            var ClientProjects = user.ClientProjects.Select(i => i.Id).ToArray();

            if (user != null)
            {
                model.MainPackagesPurchased = context.ProjectPackages.Where(pp => ClientProjects.Contains(pp.ProjectId)).Select(i => i.Package.Name).ToList();
                model.SubPackagesPurchased = context.ProjectSubPackages.Where(pp => ClientProjects.Contains(pp.ProjectId)).Select(i => i.SubPackage.Name).ToList();
                model.Countries = await Countries(user.CountryId != null ?(int) user.CountryId :0);
                model.States = await States(user.StateId != null ? (int)user.StateId : 0);
            }




            return View(model);
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(CustomerChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await currentUserService.GetUser();

                if(user == null)
                {
                    return BadRequest();
                }

                bool Result = false;

                if(string.IsNullOrEmpty(model.OldPassword))
                {
                    Result = (await userManager.ChangePasswordAsync(user, "123456", model.NewPassword)).Succeeded;
                    if(Result)
                    {
                        return RedirectToAction(nameof(Profile));
                    }
                }

                Result = (await userManager.ChangePasswordAsync(user,model.OldPassword,model.NewPassword)).Succeeded;

                if (Result)
                {
                    return RedirectToAction(nameof(Profile));
                }
            }

            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(CustomerLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await context.Users.FirstOrDefaultAsync(i => i.Email == model.Email);

                if(user == null)
                {
                    return BadRequest();
                }

                var result = await signInManager.PasswordSignInAsync(user,model.Password,true,true);

                if(result.Succeeded)
                {
                    return Redirect("/customer/dashboard");
                }


            }

            return View(model);
        }



        #region Functions
        async Task<SelectList> Countries(int SelectedValue = 0)
        {
            var countries = await context.Countries.Where(i => i.IsActive).Select(i => new DropdownViewModel<int>()
            {
                Value = i.Id,
                Text = i.Name
            }).ToListAsync();

            return new SelectList(countries, nameof(DropdownViewModel<int>.Value), nameof(DropdownViewModel<int>.Text), SelectedValue);
        }

        async Task<SelectList> States(int SelectedValue = 0)
        {
            var states = await context.States.Where(i => i.IsActive).Select(i => new DropdownViewModel<int>()
            {
                Value = i.Id,
                Text = i.Name
            }).ToListAsync();

            return new SelectList(states, nameof(DropdownViewModel<int>.Value), nameof(DropdownViewModel<int>.Text), SelectedValue);
        }

        public async Task<IActionResult> OldPasswordValidation(string OldPassword)
        {
            var user = await currentUserService.GetUser();
            bool Result = false;

            if (string.IsNullOrEmpty(OldPassword))
            {
                Result = await userManager.CheckPasswordAsync(user,"123456");

                if(!Result)
                {
                    return Json("Old password required");
                }
            }

            Result = await userManager.CheckPasswordAsync(user, OldPassword);

            if (!Result)
            {
                return Json("Old password not correct");
            }

            return Json(true);

        }

        public async Task<IActionResult> NewPasswordValidation(string OldPassword)
        {
            var user = await currentUserService.GetUser();
            bool Result = false;

            if (string.IsNullOrEmpty(OldPassword))
            {
                Result = await userManager.CheckPasswordAsync(user, "123456");

                if (!Result)
                {
                    return Json("Old password required");
                }
            }

            return Json(true);

        }

        public async Task<IActionResult> LoginEmailValidation(string Email)
        {
            var user = await context.Users.FirstOrDefaultAsync(i=> i.Email == Email);

            if(user == null)
            {
                return Json("This email not found");
            }

            return Json(true);

        }

        public async Task<IActionResult> LoginPasswordValidation(string Email,string Password)
        {
            var user = await context.Users.FirstOrDefaultAsync(i => i.Email == Email);

            if (user == null)
            {
                return Json("This email not found");
            }

            var IsPasswordCorrect = await userManager.CheckPasswordAsync(user, Password);

            if(!IsPasswordCorrect)
            {
                return Json("Password not correct");
            }

            return Json(true);

        }

        public async Task<IActionResult> EmailExists(string Email)
        {
            var userId = currentUserService.UserId;
            var user = await context.Users.FirstOrDefaultAsync(i => i.Email == Email && i.Id != userId);

            if (user != null)
            {
                return Json("This email exists");
            }

            return Json(true);

        }

        public async Task<IActionResult> PhoneExists(string Phone)
        {
            var userId = currentUserService.UserId;
            var user = await context.Users.FirstOrDefaultAsync(i => i.PhoneNumber == Phone && i.Id != userId);

            if (user != null)
            {
                return Json("This phone exists");
            }

            return Json(true);

        }

        #endregion
    }
}
