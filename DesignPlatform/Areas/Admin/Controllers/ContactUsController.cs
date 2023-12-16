using DesignPlatform.Areas.Admin.ViewModels.AdminCountryViewModels;
using DesignPlatform.Data;
using DesignPlatform.Enums;
using DesignPlatform.Extensions;
using DesignPlatform.Helpers.CurrentUserService;
using DesignPlatform.Helpers;
using DesignPlatform.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DesignPlatform.Areas.Admin.ViewModels.AdminContactUsViewModels;
using Microsoft.EntityFrameworkCore;

namespace DesignPlatform.Areas.Admin.Controllers
{

    [Area("Admin")]
    [AuthorizeRoles(Roles.Admin)]
    public class ContactUsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly ICurrentUserService currentUserService;

        public ContactUsController(ApplicationDbContext context, ICurrentUserService currentUserService)
        {
            this.context = context;
            this.currentUserService = currentUserService;
        }

        #region Index
        public async Task<IActionResult> Index(AdminContactUsParamsViewModel model)
        {
            string Lang = "en";


            var items = context.ContactUs.OrderByDescending(i => i.Date);


            #region Pagination
            var PaginationData = PaginationHelper.GetPaginationData(items, model.MaxRows, items.Count(), model.PageNumber);
            var PagesCount = PaginationData.PagesCount;
            var NextPage = PaginationData.NextPage;
            var PreviousPage = PaginationData.PreviousPage;
            //items = PaginationData.Data;
            #endregion


            var usersToDispaly = await items.Select(i => new AdminContactUsListViewModel()
            {
                Id = i.Id,
                Name = i.Name,
                Email = i.Email,
                Phone = i.Phone
            }).ToListAsync();


           

            var Edit = await ItemData(model.Id);

            var ViewModel = new AdminContactUsViewModel()
            {
                PageNumber = model.PageNumber,
                PagesCount = PagesCount,
                NextPage = NextPage,
                PreviousPage = PreviousPage,
                Items = usersToDispaly,
                Edit = Edit,
                EditModalShow = model.Id != 0,
            };

            return View(ViewModel);
        }



        async Task<AdminContactUsEditViewModel> ItemData(int Id)
        {
            var item = await context.ContactUs.Where(u => u.Id == Id).Select(i => new AdminContactUsEditViewModel()
            {
                Id = i.Id,
                Name = i.Name,
                Email = i.Email,
                Phone = i.Phone,
                Message = i.Message,

            }).FirstOrDefaultAsync();

            if (item == null)
            {
                item = new AdminContactUsEditViewModel();
            }


            return item;
        }

        #endregion

      

     

        #region Delete
        public async Task<IActionResult> Delete(int Id)
        {
            var item = await context.ContactUs.FirstOrDefaultAsync(s => s.Id == Id);

            if (item == null)
            {
                return BadRequest();
            }

            item.IsDeleted = true;

            context.Update(item);
            await context.SaveChangesAsync();



            return RedirectToAction(nameof(Index));


        }
        #endregion
    }
}
