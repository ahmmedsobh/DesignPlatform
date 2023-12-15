using DesignPlatform.Data;
using DesignPlatform.Enums;
using DesignPlatform.Extensions;
using DesignPlatform.Helpers;
using DesignPlatform.Helpers.CurrentUserService;
using DesignPlatform.Helpers.UploadHelper;
using DesignPlatform.Models;
using DesignPlatform.ViewModels.CustomerViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DesignPlatform.Controllers
{
    [AuthorizeRoles(Roles.Client)]
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IUploadHelper uploadHelper;
        private readonly ICurrentUserService currentUserService;

        public CustomerController(ApplicationDbContext context, IUploadHelper uploadHelper, ICurrentUserService currentUserService)
        {
            this.context = context;
            this.uploadHelper = uploadHelper;
            this.currentUserService = currentUserService;
        }

        public async Task<IActionResult> Dashboard()
        {
            var user = await currentUserService.GetUser();
            IQueryable<Project> items = context.Projects;

            if(user != null)
            {
                items = items.Where(i => i.ClientId == user.Id);
            }

            var itemsToDisplay = await items.Select(i => new CustomerDashboardViewModel()
            {
                Id = i.Id,
                Status = StatusHelper.StatusText(i.Status),
                Date = i.Date.ToShortDateString(),
                Time = i.Date.ToShortTimeString(),
            }).ToListAsync();
            

            return View(itemsToDisplay);
        }
        public async Task<IActionResult> Index(int Id)
        {
            var project = await context.Projects.Where(i => i.Id == Id).Select(i => new ProjectDetailsViewModel() 
            {
                Id = i.Id,
                ImagesLinks = i.Images.Where(i=> i.Type == (int)ImageType.Image).Select(p=> new ImageViewModel() { Id = p.Id , ImgPath = AppHost.Url + p.ImagePath } ).ToList(),
                InspirationLinkes = i.Images.Where(i=> i.Type == (int)ImageType.Inspiration).Select(p=> new ImageViewModel() { Id = p.Id, ImgPath = AppHost.Url + p.ImagePath }).ToList(),
                DesignLink = !string.IsNullOrEmpty(i.DesignImagePath) ? AppHost.Url + i.DesignImagePath : "",
                Notes = i.Notes,
                ProjectMangerId = i.ProjectManagerId,
                SubPackages = context.PackageSubPackages.Where(p=> p.PackageId == i.ProjectPackages.Select(x=> x.PackageId).FirstOrDefault() && !i.ProjectSubPackages.Any(x => x.SubPackageId == p.SubPackageId)).Select(p=> new SubPackageViewModel()
                { 
                    Id = p.SubPackageId,
                    Name = p.SubPackage.Name,
                    OfferHeader = p.SubPackage.OfferHeader,
                    OfferDescription = p.SubPackage.OfferDescription,
                    Image = AppHost.Url + p.SubPackage.ImagePath,
                    Description = p.SubPackage.Description,
                    Features = p.SubPackage.SubPackageFeatures.Select(i=> i.Name).ToList(),
                    IsSubscripe = i.ProjectSubPackages.Any(x=> x.SubPackageId == p.Id),
                    
                }).ToList(),

            }).FirstOrDefaultAsync();

            if(project == null)
            {
                return RedirectToAction(nameof(Dashboard));
            }

            return View(project);
        }
        [HttpPost]
        public async Task<IActionResult> Index(ProjectDetailsViewModel model)
        {
            var project = await context.Projects.Include(i => i.ProjectPackages).Include(i=> i.ProjectSubPackages).Include(i => i.Images).FirstOrDefaultAsync(i => i.Id == model.Id);

            if (ModelState.IsValid)
            {

                if (project == null)
                {
                    return BadRequest();
                }


                IEnumerable<Image> images = new List<Image>();
                IEnumerable<Image> inspiration = new List<Image>();

                if(model.Images != null)
                {
                    images = model.Images.Select(i => new Image()
                    {
                        ImagePath = uploadHelper.Upload(i, (int)FolderName.Project),
                        Type = (int)ImageType.Image,
                    });
                }
                
                if (model.Inspiration != null)
                {
                    inspiration = model.Inspiration.Select(i => new Image()
                    {
                        ImagePath = uploadHelper.Upload(i, (int)FolderName.Project),
                        Type = (int)ImageType.Inspiration,
                    });
                }

                var AllImages = new List<Image>();
                AllImages.AddRange(images);
                AllImages.AddRange(inspiration);

                foreach (var item in AllImages)
                {
                    project.Images.Add(item);
                }

                project.DesignImagePath = model.Design != null ? uploadHelper.Upload(model.Design, (int)FolderName.Project) : project.DesignImagePath;
                project.Notes = !string.IsNullOrEmpty(model.Notes) ? model.Notes : project.Notes;
                project.IsClientUploadResources = true;
               

                context.Update(project);
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Appointment),new {Id = project.Id });

            }

            model.SubPackages = await context.PackageSubPackages.Where(p => p.PackageId == project.ProjectPackages.Select(x => x.PackageId).FirstOrDefault() && project.ProjectSubPackages.Any(x => x.SubPackageId == p.SubPackageId)).Select(p => new SubPackageViewModel()
            {
                Id = p.SubPackageId,
                Name = p.SubPackage.Name,
                OfferHeader = p.SubPackage.OfferHeader,
                OfferDescription = p.SubPackage.OfferDescription,
                Image = AppHost.Url + p.SubPackage.ImagePath,
                Description = p.SubPackage.Description,
                Features = p.SubPackage.SubPackageFeatures.Select(i => i.Name).ToList(),
                IsSubscripe = project.ProjectSubPackages.Any(x => x.SubPackageId == p.Id),

            }).ToListAsync();

            return View(model);
        }
        public async Task<IActionResult> Appointment(int Id)
        {
            var project = await context.Projects.FirstOrDefaultAsync(i => i.Id == Id && i.IsClientUploadResources);

            if (project == null)
            {
                return RedirectToAction(nameof(Index), new { Id = Id });
            }

            var appointment = new AppointmentViewModel()
            {
                Id = Id,
                Phone = project.AppointmentPhone,
                Date = project.ScheduleDate,
            };

            return View(appointment);

        }
        public async Task<IActionResult> AddAppointment(int Id)
        {
            var project = await context.Projects.FirstOrDefaultAsync(i => i.Id == Id && i.IsClientUploadResources);

            if (project == null)
            {
                return RedirectToAction(nameof(Index), new {Id = Id});
            }

            var appointment = new AppointmentViewModel()
            {
                Id = Id,
                Phone = project.AppointmentPhone,
                Date = project.ScheduleDate == DateTime.MaxValue ? DateTime.Now : project.ScheduleDate,
            };

            return View(appointment);

        }
        [HttpPost]
        public async Task<IActionResult> AddAppointment(AppointmentViewModel model)
        {

            if(ModelState.IsValid)
            {
                var project = await context.Projects.FirstOrDefaultAsync(i => i.Id == model.Id && i.IsClientUploadResources);

                if (project == null)
                {
                    return RedirectToAction(nameof(Index));
                }

                project.ScheduleDate = model.Date;
                project.AppointmentPhone = model.Phone;

                context.Update(project);
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(AppointmentDetails), new { Id = model.Id });
            }

            return View(model);
        }
        public async Task<IActionResult> AppointmentDetails(int Id)
        {
            var project = await context.Projects.FirstOrDefaultAsync(i => i.Id == Id && i.IsClientUploadResources);

            if (project == null)
            {
                return RedirectToAction(nameof(Index), new { Id = Id });
            }

            var appointment = new AppointmentViewModel()
            {
                Phone = project.AppointmentPhone,
                Date = project.ScheduleDate,
            };

            return View(appointment);
        }
        public async Task<IActionResult> DeleteImage(int Id)
        {
            var Image = await context.Images.FirstOrDefaultAsync(i => i.Id == Id);

            context.Remove(Image);
            await context.SaveChangesAsync();


            return Json(new
            {
                data = true,
            });
        }
        public async Task<IActionResult> DeleteDesign(int Id)
        {
            var Item = await context.Projects.FirstOrDefaultAsync(i => i.Id == Id);
            Item.DesignImagePath = "";

            context.Update(Item);
            await context.SaveChangesAsync();


            return Json(new
            {
                data = true,
            });
        }
        public async Task<IActionResult> SubPackageDetails(int Id,int ProjectId)
        {
            var item = await context.SubPackages.Where(i => i.Id == Id).Select(p => new SubPackageViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                OfferHeader = p.OfferHeader,
                OfferDescription = p.OfferDescription,
                Image = AppHost.Url + p.ImagePath,
                Description = p.Description,
                Features = p.SubPackageFeatures.Select(i => i.Name).ToList(),
                ProjectId = ProjectId,

            }).FirstOrDefaultAsync();

            return View(item);

        }
        [HttpPost]
        public async Task<IActionResult> ClientSubPackageAdd(int ProjectId,int SubPackageId)
        {
            var package = await context.SubPackages.FirstOrDefaultAsync(i=> i.Id == SubPackageId);
            var project = await context.Projects.FirstOrDefaultAsync(i => i.Id == ProjectId);

            if (package == null || project ==  null)
            {
                return BadRequest();
            }

            var ProjectSubPackage = new ProjectSubPackage() 
            { 
                ProjectId = ProjectId,
                SubPackageId = SubPackageId
            };

            await context.AddAsync(ProjectSubPackage);
            var Result =  await context.SaveChangesAsync() > 0;

            if(Result)
            {
                return RedirectToAction(nameof(Dashboard));
            }


            return BadRequest();

        }

        #region Functions

        

        #endregion

    }
}
