using DesignPlatform.Areas.Admin.ViewModels.AdminCountryViewModels;
using DesignPlatform.Data;
using DesignPlatform.Helpers.CurrentUserService;
using DesignPlatform.Helpers;
using DesignPlatform.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DesignPlatform.Areas.Admin.ViewModels.AdminPackageViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using DesignPlatform.ViewModels;
using DesignPlatform.Helpers.UploadHelper;
using DesignPlatform.Enums;
using DesignPlatform.Extensions;

namespace DesignPlatform.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthorizeRoles(Roles.Admin)]

    public class PackagesController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly ICurrentUserService currentUserService;
        private readonly IUploadHelper uploadHelper;

        public PackagesController(ApplicationDbContext context,  ICurrentUserService currentUserService,IUploadHelper uploadHelper)
        {
            this.context = context;
            this.currentUserService = currentUserService;
            this.uploadHelper = uploadHelper;
        }

        #region Index
        public async Task<IActionResult> Index(AdminPackageParamsViewModel model)
        {
            string Lang = "en";


            var items = context.Packagees.OrderByDescending(i => i.Date);


            #region Pagination
            var PaginationData = PaginationHelper.GetPaginationData(items, model.MaxRows, items.Count(), model.PageNumber);
            var PagesCount = PaginationData.PagesCount;
            var NextPage = PaginationData.NextPage;
            var PreviousPage = PaginationData.PreviousPage;
            //items = PaginationData.Data;
            #endregion


            var itemsToDispaly = await items.Select(i => new AdminPackageListViewModel()
            {
                Id = i.Id,
                Name = i.Name,
                FrontYardPrice = i.FrontYardPrice,
                BackYardPrice = i.BackYardPrice,
                FrontBackYardPrice = i.FrontBackYardPrice,
                ImageUrl = AppHost.Url + i.ImagePath,
                IsActive = i.IsActive,
            }).ToListAsync();


            var Add = new AdminPackageAddViewModel()
            {
                SubPackages = await SubPackages(new int[0]),
            };

            var Edit = await ItemData(model.Id);

            var ViewModel = new AdminPackageViewModel()
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



        async Task<AdminPackageEditViewModel> ItemData(int Id)
        {
            var item = await context.Packagees.Where(u => u.Id == Id).Select(i => new AdminPackageEditViewModel()
            {
                Id = i.Id,
                Name = i.Name,
                FrontYardPrice  = i.FrontYardPrice,
                BackYardPrice = i.BackYardPrice,
                FrontBackYardPrice = i.FrontBackYardPrice,
                Features = i.PackageFeatures.Select(i=>  new DropdownViewModel<int>()
                {
                    Value = i.Id,
                    Text = i.Name,
                }).ToList(),
                SubPackageIds = i.PackageSubPackages.Select(i=> i.SubPackageId).ToArray()

            }).FirstOrDefaultAsync();

            if (item == null)
            {
                item = new AdminPackageEditViewModel();
            }

            if(item != null)
            {
                item.SubPackages = await SubPackages(item.SubPackageIds);
            }


            return item;
        }

        #endregion

        #region Add
        [HttpPost]
        public async Task<IActionResult> Add(AdminPackageAddViewModel model)
        {
            if (ModelState.IsValid)
            {


                var item = new Packagee()
                {
                    Name = model.Name,
                    FrontYardPrice = model.FrontYardPrice,
                    BackYardPrice = model.BackYardPrice,
                    FrontBackYardPrice = model.FrontBackYardPrice,
                    IsActive = true,
                    Date = DateTime.Now,
                    ImagePath = uploadHelper.Upload(model.ImageFile,(int)FolderName.Package),
                    PackageFeatures = model.Features.Select(i=> new PackageFeature()
                    {
                        Name = i.Text,
                    }).ToList(),
                    PackageSubPackages = model.SubPackageIds.Select(i=> new PackageSubPackage()
                    {
                        SubPackageId = i
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
        public async Task<IActionResult> Edit(AdminPackageEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var item = await context.Packagees.Include(i=> i.PackageFeatures).Include(i=> i.PackageSubPackages).Where(u => u.Id == model.Id).FirstOrDefaultAsync();

                if (item == null)
                {
                    return NotFound();
                }

                item.Name = !string.IsNullOrEmpty(model.Name) ? model.Name : item.Name;
                item.FrontYardPrice = model.FrontYardPrice > 0 ? model.FrontYardPrice : item.FrontYardPrice;
                item.BackYardPrice = model.BackYardPrice > 0 ? model.BackYardPrice : item.BackYardPrice;
                item.FrontBackYardPrice = model.FrontBackYardPrice > 0 ? model.FrontBackYardPrice : item.FrontBackYardPrice;
                item.ImagePath = model.ImageFile != null ? uploadHelper.Upload(model.ImageFile, (int)FolderName.Package) : item.ImagePath;

                item.PackageFeatures = model.Features.Select(i => new PackageFeature()
                {
                    Name = i.Text,
                }).ToList();

                item.PackageSubPackages = model.SubPackageIds.Select(i => new PackageSubPackage()
                {
                    SubPackageId = i
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
            var item = await context.Packagees.FirstOrDefaultAsync(s => s.Id == Id);



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
            var item = await context.Packagees.FirstOrDefaultAsync(s => s.Id == Id);

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
        async Task<MultiSelectList> SubPackages(int[] SelectedValues )
        {
            var packages = await context.SubPackages.Where(i => i.IsActive).Select(i => new DropdownViewModel<int>()
            {
                Value = i.Id,
                Text = i.Name,
            }).ToArrayAsync();

            return new MultiSelectList(packages,nameof(DropdownViewModel<int>.Value), nameof(DropdownViewModel<int>.Text),SelectedValues);
        }
        #endregion
    }
}
