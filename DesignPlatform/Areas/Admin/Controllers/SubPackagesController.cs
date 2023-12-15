using DesignPlatform.Data;
using DesignPlatform.Enums;
using DesignPlatform.Helpers.CurrentUserService;
using DesignPlatform.Helpers.UploadHelper;
using DesignPlatform.Helpers;
using DesignPlatform.Models;
using DesignPlatform.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DesignPlatform.Areas.Admin.ViewModels.AdminSubPackagesViewModels;
using Microsoft.EntityFrameworkCore;
using DesignPlatform.Extensions;

namespace DesignPlatform.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthorizeRoles(Roles.Admin)]

    public class SubPackagesController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly ICurrentUserService currentUserService;
        private readonly IUploadHelper uploadHelper;

        public SubPackagesController(ApplicationDbContext context, ICurrentUserService currentUserService, IUploadHelper uploadHelper)
        {
            this.context = context;
            this.currentUserService = currentUserService;
            this.uploadHelper = uploadHelper;
        }

        #region Index
        public async Task<IActionResult> Index(AdminSubPackageParamsViewModel model)
        {
            string Lang = "en";


            var items = context.SubPackages.OrderByDescending(i => i.Date);


            #region Pagination
            var PaginationData = PaginationHelper.GetPaginationData(items, model.MaxRows, items.Count(), model.PageNumber);
            var PagesCount = PaginationData.PagesCount;
            var NextPage = PaginationData.NextPage;
            var PreviousPage = PaginationData.PreviousPage;
            //items = PaginationData.Data;
            #endregion


            var itemsToDispaly = await items.Select(i => new AdminSubPackageListViewModel()
            {
                Id = i.Id,
                Name = i.Name,
                Price = i.Price,
                ImageUrl = AppHost.Url + i.ImagePath,
                IsActive = i.IsActive,
            }).ToListAsync();


            var Add = new AdminSubPackageAddViewModel()
            {
                
            };

            var Edit = await ItemData(model.Id);

            var ViewModel = new AdminSubPackageViewModel()
            {
                PageNumber = model.PageNumber,
                PagesCount = PagesCount,
                NextPage = NextPage,
                PreviousPage = PreviousPage,
                Items = itemsToDispaly,
                Add = Add,
                Edit = Edit,
                EditModalShow = model.Id != 0,
            };

            return View(ViewModel);
        }



        async Task<AdminSubPackageEditViewModel> ItemData(int Id)
        {
            var item = await context.SubPackages.Where(u => u.Id == Id).Select(i => new AdminSubPackageEditViewModel()
            {
                Id = i.Id,
                Name = i.Name,
                Price = i.Price,
                OfferHeader = i.OfferHeader,
                OfferDescription = i.OfferDescription,
                Description = i.Description,
                Features = i.SubPackageFeatures.Select(i => new DropdownViewModel<int>()
                {
                    Value = i.Id,
                    Text = i.Name,
                }).ToList(),

            }).FirstOrDefaultAsync();

            if (item == null)
            {
                item = new AdminSubPackageEditViewModel();
            }

            


            return item;
        }

        #endregion

        #region Add
        [HttpPost]
        public async Task<IActionResult> Add(AdminSubPackageAddViewModel model)
        {
            if (ModelState.IsValid)
            {


                var item = new SubPackage()
                {
                    Name = model.Name,
                    Price = model.Price,
                    IsActive = true,
                    Date = DateTime.Now,
                    ImagePath = uploadHelper.Upload(model.ImageFile, (int)FolderName.Package),
                    OfferHeader = model.OfferHeader,
                    OfferDescription = model.OfferDescription,
                    Description = model.Description,
                    SubPackageFeatures = model.Features.Select(i => new SubPackageFeature()
                    {
                        Name = i.Text,
                    }).ToList(),
                    
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
        public async Task<IActionResult> Edit(AdminSubPackageEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var item = await context.SubPackages.Include(i => i.SubPackageFeatures).Where(u => u.Id == model.Id).FirstOrDefaultAsync();

                if (item == null)
                {
                    return NotFound();
                }

                item.Name = !string.IsNullOrEmpty(model.Name) ? model.Name : item.Name;
                item.Price = model.Price > 0 ? model.Price : item.Price;
                item.ImagePath = model.ImageFile != null ? uploadHelper.Upload(model.ImageFile, (int)FolderName.Package) : item.ImagePath;
                item.OfferHeader = !string.IsNullOrEmpty(model.OfferHeader) ? model.OfferHeader : item.OfferHeader;
                item.OfferDescription = !string.IsNullOrEmpty(model.OfferDescription) ? model.OfferDescription : item.OfferDescription;
                item.Description = !string.IsNullOrEmpty(model.Description) ? model.Description : item.Description;


                item.SubPackageFeatures = model.Features.Select(i => new SubPackageFeature()
                {
                    Name = i.Text,
                }).ToList();

               

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
            var item = await context.SubPackages.FirstOrDefaultAsync(s => s.Id == Id);



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
            var item = await context.SubPackages.FirstOrDefaultAsync(s => s.Id == Id);

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
