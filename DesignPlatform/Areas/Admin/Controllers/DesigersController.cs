using DesignPlatform.Areas.Admin.ViewModels.AdminEmployeesViewModels;
using DesignPlatform.Data;
using DesignPlatform.Enums;
using DesignPlatform.Helpers.CurrentUserService;
using DesignPlatform.Helpers;
using DesignPlatform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DesignPlatform.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DesigersController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICurrentUserService currentUserService;

        public DesigersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ICurrentUserService currentUserService)
        {
            this.context = context;
            this.userManager = userManager;
            this.currentUserService = currentUserService;
        }

        #region Index
        public async Task<IActionResult> Index(AdminEmployeeParamsViewModel model)
        {
            string Lang = "en";

            var MainRole = ((int)Roles.Desinger).ToString();

            var users = context.Users.OrderByDescending(i => i.Date).Where(u => u.MainRoles.Contains(MainRole));


            #region Pagination
            var PaginationData = PaginationHelper.GetPaginationData(users, model.MaxRows, users.Count(), model.PageNumber);
            var PagesCount = PaginationData.PagesCount;
            var NextPage = PaginationData.NextPage;
            var PreviousPage = PaginationData.PreviousPage;
            //users = PaginationData.Data;
            #endregion


            var usersToDispaly = await users.Select(u => new AdminEmployeeListViewModel()
            {
                Id = u.Id,
                Name = u.FirstName,
                Email = u.Email,
                Phone = u.PhoneNumber,
                IsActive = u.IsActive,
            }).ToListAsync();


            var Add = new AdminEmployeeAddViewModel()
            {

            };

            var Edit = await ItemData(model.Id);

            var ViewModel = new AdminEmployeeViewModel()
            {
                PageNumber = model.PageNumber,
                PagesCount = PagesCount,
                NextPage = NextPage,
                PreviousPage = PreviousPage,
                Items = usersToDispaly,
                Add = Add,
                Edit = Edit,
                EditModalShow = !string.IsNullOrEmpty(model.Id)
            };

            return View(ViewModel);
        }



        async Task<AdminEmployeeEditViewModel> ItemData(string Id)
        {
            var user = await context.Users.Where(u => u.Id == Id).Select(u => new AdminEmployeeEditViewModel()
            {
                Id = u.Id,
                Name = u.FirstName,
                Email = u.Email,
                Phone = u.PhoneNumber,
            }).FirstOrDefaultAsync();

            if (user == null)
            {
                user = new AdminEmployeeEditViewModel();
            }


            return user;
        }

        #endregion

        #region Add
        [HttpPost]
        public async Task<IActionResult> Add(AdminEmployeeAddViewModel model)
        {
            if (ModelState.IsValid)
            {

                var MainRole = ((int)Roles.Desinger).ToString() + ",";

                var user = new ApplicationUser()
                {
                    FirstName = model.Name,
                    PhoneNumber = model.Phone,
                    Email = model.Email,
                    MainRoles = MainRole,
                    UserName = model.Email,
                    IsActive = true,
                    Date = DateTime.Now,
                };

                var UserResult = await userManager.CreateAsync(user, !string.IsNullOrEmpty(model.Password) ? model.Password : "123456");
                if (UserResult.Succeeded)
                {
                    var RoleResut = await userManager.AddToRoleAsync(user, Roles.Desinger.ToString());
                    return RedirectToAction(nameof(Index));
                }


            }

            return View(model);
        }

        

        #endregion

        #region EditPost
        [HttpPost]
        public async Task<IActionResult> Edit(AdminEmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await context.Users.Where(u => u.Id == model.Id).FirstOrDefaultAsync();

                if (user == null)
                {
                    return NotFound();
                }

                user.FirstName = !string.IsNullOrEmpty(model.Name) ? model.Name : user.FirstName;
                user.PhoneNumber = !string.IsNullOrEmpty(model.Phone) ? model.Phone : user.PhoneNumber;
                user.Email = !string.IsNullOrEmpty(model.Email) ? model.Email : user.Email;
                user.UserName = !string.IsNullOrEmpty(model.Email) ? model.Email : user.UserName;

                context.Update(user);
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        

        #endregion

        #region ChangeStatus
        public async Task<IActionResult> ChangeStatus(string Id)
        {
            var user = await context.Users.FirstOrDefaultAsync(s => s.Id == Id);



            if (user == null)
            {
                return Json(new { status = false });
            }

            user.IsActive = !user.IsActive;
            context.Update(user);
            await context.SaveChangesAsync();

            var message = user.IsActive ? "Active" : "In Active";

            return Json(new { status = user.IsActive, message });


        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(string Id)
        {
            var user = await context.Users.FirstOrDefaultAsync(s => s.Id == Id);

            if (user == null)
            {
                return BadRequest();
            }

            user.IsDeleted = true;
            user.IsActive = false;
            user.Email = user.Email + Guid.NewGuid();
            user.NormalizedEmail = user.NormalizedEmail + Guid.NewGuid();
            user.PhoneNumber = user.PhoneNumber + Guid.NewGuid();
            user.NormalizedUserName = user.NormalizedUserName + Guid.NewGuid();
            user.UserName = user.UserName + Guid.NewGuid();
            context.Update(user);
            await context.SaveChangesAsync();



            return RedirectToAction(nameof(Index));


        }
        #endregion
    }
}
