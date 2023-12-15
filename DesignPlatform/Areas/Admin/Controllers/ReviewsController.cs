using DesignPlatform.Areas.Admin.ViewModels.AdminCountryViewModels;
using DesignPlatform.Data;
using DesignPlatform.Helpers.CurrentUserService;
using DesignPlatform.Helpers;
using DesignPlatform.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DesignPlatform.Areas.Admin.ViewModels.AdminReviewViewModels;
using Microsoft.EntityFrameworkCore;
using DesignPlatform.Extensions;
using DesignPlatform.Enums;

namespace DesignPlatform.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthorizeRoles(Roles.Admin)]

    public class ReviewsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly ICurrentUserService currentUserService;

        public ReviewsController(ApplicationDbContext context ,ICurrentUserService currentUserService)
        {
            this.context = context;
            this.currentUserService = currentUserService;
        }

        #region Index
        public async Task<IActionResult> Index(AdminReviewParamsViewModel model)
        {
            string Lang = "en";


            var items = context.Reviews.OrderByDescending(i => i.Date);


            #region Pagination
            var PaginationData = PaginationHelper.GetPaginationData(items, model.MaxRows, items.Count(), model.PageNumber);
            var PagesCount = PaginationData.PagesCount;
            var NextPage = PaginationData.NextPage;
            var PreviousPage = PaginationData.PreviousPage;
            //items = PaginationData.Data;
            #endregion


            var usersToDispaly = await items.Select(i => new AdminReviewListViewModel()
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description,
                IsActive = i.IsActive,
            }).ToListAsync();


            var Add = new AdminReviewAddViewModel()
            {

            };

            var Edit = await ItemData(model.Id);

            var ViewModel = new AdminReviewViewModel()
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



        async Task<AdminReviewEditViewModel> ItemData(int Id)
        {
            var item = await context.Reviews.Where(u => u.Id == Id).Select(i => new AdminReviewEditViewModel()
            {
                Id = i.Id,
                Name = i.Name,
                Description= i.Description,
            }).FirstOrDefaultAsync();

            if (item == null)
            {
                item = new AdminReviewEditViewModel();
            }


            return item;
        }

        #endregion

        #region Add
        [HttpPost]
        public async Task<IActionResult> Add(AdminReviewAddViewModel model)
        {
            if (ModelState.IsValid)
            {


                var item = new Review()
                {
                    Name = model.Name,
                    IsActive = true,
                    Date = DateTime.Now,
                    Description = model.Description
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
        public async Task<IActionResult> Edit(AdminReviewEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var item = await context.Reviews.Where(u => u.Id == model.Id).FirstOrDefaultAsync();

                if (item == null)
                {
                    return NotFound();
                }

                item.Name = !string.IsNullOrEmpty(model.Name) ? model.Name : item.Name;
                item.Description = !string.IsNullOrEmpty(model.Description) ? model.Description : item.Description;

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
            var item = await context.Reviews.FirstOrDefaultAsync(s => s.Id == Id);



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
            var item = await context.Reviews.FirstOrDefaultAsync(s => s.Id == Id);

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
