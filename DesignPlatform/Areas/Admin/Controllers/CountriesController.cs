using DesignPlatform.Areas.Admin.ViewModels.AdminEmployeesViewModels;
using DesignPlatform.Data;
using DesignPlatform.Enums;
using DesignPlatform.Helpers.CurrentUserService;
using DesignPlatform.Helpers;
using DesignPlatform.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DesignPlatform.Areas.Admin.ViewModels.AdminCountryViewModels;
using Microsoft.EntityFrameworkCore;

namespace DesignPlatform.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CountriesController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICurrentUserService currentUserService;

        public CountriesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ICurrentUserService currentUserService)
        {
            this.context = context;
            this.userManager = userManager;
            this.currentUserService = currentUserService;
        }

        #region Index
        public async Task<IActionResult> Index(AdminCountryParamsViewModel model)
        {
            string Lang = "en";


            var items = context.Countries.OrderByDescending(i => i.Date).Where(i=> i.IsActive);


            #region Pagination
            var PaginationData = PaginationHelper.GetPaginationData(items, model.MaxRows, items.Count(), model.PageNumber);
            var PagesCount = PaginationData.PagesCount;
            var NextPage = PaginationData.NextPage;
            var PreviousPage = PaginationData.PreviousPage;
            //items = PaginationData.Data;
            #endregion


            var usersToDispaly = await items.Select(i => new AdminCountryListViewModel()
            {
                Id = i.Id,
                Name = i.Name,
                IsActive = i.IsActive,
            }).ToListAsync();


            var Add = new AdminCountryAddViewModel()
            {

            };

            var Edit = await ItemData(model.Id);

            var ViewModel = new AdminCountryViewModel()
            {
                PageNumber = model.PageNumber,
                PagesCount = PagesCount,
                NextPage = NextPage,
                PreviousPage = PreviousPage,
                Items = usersToDispaly,
                Add = Add,
                Edit = Edit,
                EditModalShow = model.Id != 0,
            };

            return View(ViewModel);
        }



        async Task<AdminCountryEditViewModel> ItemData(int Id)
        {
            var item = await context.Countries.Where(u => u.Id == Id).Select(i => new AdminCountryEditViewModel()
            {
                Id = i.Id,
                Name = i.Name,
            }).FirstOrDefaultAsync();

            if (item == null)
            {
                item = new AdminCountryEditViewModel();
            }


            return item;
        }

        #endregion

        #region Add
        [HttpPost]
        public async Task<IActionResult> Add(AdminCountryAddViewModel model)
        {
            if (ModelState.IsValid)
            {


                var item = new Country()
                {
                    Name = model.Name,
                    IsActive = true,
                    Date = DateTime.Now,
                };

                await context.AddAsync(item);
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));


            }

            return View(model);
        }



        #endregion

        #region EditPost
        [HttpPost]
        public async Task<IActionResult> Edit(AdminCountryEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var item = await context.Countries.Where(u => u.Id == model.Id).FirstOrDefaultAsync();

                if (item == null)
                {
                    return NotFound();
                }

                item.Name = !string.IsNullOrEmpty(model.Name) ? model.Name : item.Name;

                context.Update(item);
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }



        #endregion

        #region ChangeStatus
        public async Task<IActionResult> ChangeStatus(int Id)
        {
            var item = await context.Countries.FirstOrDefaultAsync(s => s.Id == Id);



            if (item == null)
            {
                return Json(new { status = false });
            }

            item.IsActive = !item.IsActive;
            context.Update(item);
            await context.SaveChangesAsync();

            var message = item.IsActive ? "Active" : "In Active";

            return Json(new { status = item.IsActive, message });


        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int Id)
        {
            var item = await context.Countries.FirstOrDefaultAsync(s => s.Id == Id);

            if (item == null)
            {
                return BadRequest();
            }

            item.IsDeleted = true;
            item.IsActive = false;
           
            context.Update(item);
            await context.SaveChangesAsync();



            return RedirectToAction(nameof(Index));


        }
        #endregion
    }
}
