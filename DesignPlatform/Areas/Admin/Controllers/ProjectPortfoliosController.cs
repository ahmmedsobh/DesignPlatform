using DesignPlatform.Areas.Admin.ViewModels.AdminCountryViewModels;
using DesignPlatform.Data;
using DesignPlatform.Helpers.CurrentUserService;
using DesignPlatform.Helpers;
using DesignPlatform.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DesignPlatform.Areas.Admin.ViewModels.AdminProjectPortfolioViewModels;
using Microsoft.EntityFrameworkCore;
using DesignPlatform.Helpers.UploadHelper;
using DesignPlatform.Enums;
using DesignPlatform.Extensions;

namespace DesignPlatform.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthorizeRoles(Roles.Admin)]

    public class ProjectPortfoliosController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly ICurrentUserService currentUserService;
        private readonly IUploadHelper uploadHelper;

        public ProjectPortfoliosController(ApplicationDbContext context, ICurrentUserService currentUserService,IUploadHelper uploadHelper)
        {
            this.context = context;
            this.currentUserService = currentUserService;
            this.uploadHelper = uploadHelper;
        }

        #region Index
        public async Task<IActionResult> Index(AdminProjectPortfolioParamsViewModel model)
        {
            string Lang = "en";


            var items = context.ProjectPortfolios.OrderByDescending(i => i.Date);


            #region Pagination
            var PaginationData = PaginationHelper.GetPaginationData(items, model.MaxRows, items.Count(), model.PageNumber);
            var PagesCount = PaginationData.PagesCount;
            var NextPage = PaginationData.NextPage;
            var PreviousPage = PaginationData.PreviousPage;
            //items = PaginationData.Data;
            #endregion


            var usersToDispaly = await items.Select(i => new AdminProjectPortfolioListViewModel()
            {
                Id = i.Id,
                Name = i.Name,
                IsActive = i.IsActive,
                ImagePath = AppHost.Url + i.ImagePath
            }).ToListAsync();


            var Add = new AdminProjectPortfolioAddViewModel()
            {

            };

            var Edit = await ItemData(model.Id);

            var ViewModel = new AdminProjectPortfolioViewModel()
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



        async Task<AdminProjectPortfolioEditViewModel> ItemData(int Id)
        {
            var item = await context.ProjectPortfolios.Where(u => u.Id == Id).Select(i => new AdminProjectPortfolioEditViewModel()
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description

            }).FirstOrDefaultAsync();

            if (item == null)
            {
                item = new AdminProjectPortfolioEditViewModel();
            }


            return item;
        }

        #endregion

        #region Add
        [HttpPost]
        public async Task<IActionResult> Add(AdminProjectPortfolioAddViewModel model)
        {
            if (ModelState.IsValid)
            {


                var item = new ProjectPortfolio()
                {
                    Name = model.Name,
                    IsActive = true,
                    Date = DateTime.Now,
                    ImagePath = uploadHelper.Upload(model.ImageFile,(int)FolderName.ProjectPortfolio),
                    Description = model.Description,
                    Images = model.ImageFiles.Select(i=> new ProjectPortfolioImage()
                    {
                        ImagePath = uploadHelper.Upload(model.ImageFile, (int)FolderName.ProjectPortfolio),
                    }).ToList()
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
        public async Task<IActionResult> Edit(AdminProjectPortfolioEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var item = await context.ProjectPortfolios.Include(i=> i.Images).Where(u => u.Id == model.Id).FirstOrDefaultAsync();

                if (item == null)
                {
                    return NotFound();
                }

                item.Name = !string.IsNullOrEmpty(model.Name) ? model.Name : item.Name;
                item.Description = !string.IsNullOrEmpty(model.Description) ? model.Description : item.Description;
                item.ImagePath = model.ImageFile != null ? uploadHelper.Upload(model.ImageFile,(int)FolderName.ProjectPortfolio) : item.ImagePath;

                if(model.ImageFiles != null)
                {
                    item.Images = model.ImageFiles.Select(i => new ProjectPortfolioImage()
                    {
                        ImagePath = uploadHelper.Upload(model.ImageFile, (int)FolderName.ProjectPortfolio)
                    }).ToList();
                }


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
            var item = await context.ProjectPortfolios.FirstOrDefaultAsync(s => s.Id == Id);



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
            var item = await context.ProjectPortfolios.FirstOrDefaultAsync(s => s.Id == Id);

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
