using DesignPlatform.Data;
using DesignPlatform.Helpers.CurrentUserService;
using DesignPlatform.Helpers;
using DesignPlatform.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DesignPlatform.Areas.Admin.ViewModels.AdminStateViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using DesignPlatform.ViewModels;
using DesignPlatform.Enums;
using DesignPlatform.Extensions;

namespace DesignPlatform.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthorizeRoles(Roles.Admin)]

    public class StatesController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly ICurrentUserService currentUserService;

        public StatesController(ApplicationDbContext context,  ICurrentUserService currentUserService)
        {
            this.context = context;
            this.currentUserService = currentUserService;
        }

        #region Index
        public async Task<IActionResult> Index(AdminStateParamsViewModel model)
        {
            string Lang = "en";


            var items = context.States.OrderByDescending(i => i.Date);


            #region Pagination
            var PaginationData = PaginationHelper.GetPaginationData(items, model.MaxRows, items.Count(), model.PageNumber);
            var PagesCount = PaginationData.PagesCount;
            var NextPage = PaginationData.NextPage;
            var PreviousPage = PaginationData.PreviousPage;
            //items = PaginationData.Data;
            #endregion


            var usersToDispaly = await items.Select(i => new AdminStateListViewModel()
            {
                Id = i.Id,
                Name = i.Name,
                CountryName = i.Country.Name,
                IsActive = i.IsActive,
            }).ToListAsync();


            var Add = new AdminStateAddViewModel()
            {
                Countries = await Countries(),
            };

            var Edit = await ItemData(model.Id);

            var ViewModel = new AdminStateViewModel()
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



        async Task<AdminStateEditViewModel> ItemData(int Id)
        {
            var item = await context.States.Where(u => u.Id == Id).Select(i => new AdminStateEditViewModel()
            {
                Id = i.Id,
                Name = i.Name,
            }).FirstOrDefaultAsync();

            if (item == null)
            {
                item = new AdminStateEditViewModel()
                {
                    
                };
            }

            if(item != null)
            {
                item.Countries = await Countries(Id);
            }


            return item;
        }

        #endregion

        #region Add
        [HttpPost]
        public async Task<IActionResult> Add(AdminStateAddViewModel model)
        {
            if (ModelState.IsValid)
            {


                var item = new State()
                {
                    Name = model.Name,
                    CountryId = model.CountryId,
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
        public async Task<IActionResult> Edit(AdminStateEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var item = await context.States.Where(u => u.Id == model.Id).FirstOrDefaultAsync();

                if (item == null)
                {
                    return NotFound();
                }

                item.Name = !string.IsNullOrEmpty(model.Name) ? model.Name : item.Name;
                item.CountryId = model.CountryId > 0 ? model.CountryId : item.CountryId;

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
            var item = await context.States.FirstOrDefaultAsync(s => s.Id == Id);



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
            var item = await context.States.FirstOrDefaultAsync(s => s.Id == Id);

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

        #region Functions
        async Task<SelectList> Countries(int SelectedValue = 0)
        {
            var items = await context.Countries.Where(i => i.IsActive).Select(i => new DropdownViewModel<int>()
            {
                Value = i.Id,
                Text = i.Name,
            }).ToListAsync();

            return new SelectList(items,nameof(DropdownViewModel<int>.Value), nameof(DropdownViewModel<int>.Text), SelectedValue);
        }
        #endregion
    }
}
