using DesignPlatform.Areas.Employee.ViewModels.EmployeeProjectsViewModels;
using DesignPlatform.Data;
using DesignPlatform.Enums;
using DesignPlatform.Extensions;
using DesignPlatform.Helpers;
using DesignPlatform.Helpers.CurrentUserService;
using DesignPlatform.Helpers.UploadHelper;
using DesignPlatform.Models;
using DesignPlatform.ViewModels;
using DesignPlatform.ViewModels.CustomerViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DesignPlatform.Areas.Designer.Controllers
{
    [Area("Designer")]
    [AuthorizeRoles(Roles.Desinger)]

    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IUploadHelper uploadHelper;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICurrentUserService currentUserService;

        public ProjectsController(ApplicationDbContext context, IUploadHelper uploadHelper, UserManager<ApplicationUser> userManager, ICurrentUserService currentUserService )
        {
            this.context = context;
            this.uploadHelper = uploadHelper;
            this.userManager = userManager;
            this.currentUserService = currentUserService;
        }

        public async Task<IActionResult> Index(EmployeeProjectParamsViewModel model)
        {
            var UserId = currentUserService.UserId;
            
            IQueryable<Project> items = context.Projects.Where(i=> i.DesignerId == UserId);

            if (!string.IsNullOrEmpty(model.DesignerId))
            {
                items = items.Where(i => i.DesignerId == model.DesignerId);
            }

            var itemsToDisplay = await items.Select(i => new EmployeeProjectsDetailsViewModel()
            {
                Id = i.Id,
                Phone = i.Phone,
                AppointmentPhone = i.AppointmentPhone,
                ScheduleDate = i.ScheduleDate,
                Notes = i.Notes,
                Status = i.Status,
                StatusText = StatusHelper.StatusText(i.Status),
                ClientId = i.ClientId,
                ClientName = i.Client.FirstName + " " + i.Client.LastName,
                FirstName = i.Client.FirstName,
                LastName = i.Client.LastName,
                DesignerName = i.Desinger != null ? i.Desinger.FirstName + " " + i.Desinger.LastName : "not found",
                Email = i.Client.Email,
                Address = i.Client.Address,
                City = i.Client.City,
                ZipCode = i.Client.ZipCode,
                CountryId = i.Client.CountryId,
                CountryName = i.Client.Country != null ? i.Client.Country.Name : "not found",
                StateId = i.Client.StateId,
                StateName = i.Client.State != null ? i.Client.State.Name : "not found",
                SummaryNotes = i.SummaryNotes,
                ScopeAndSizeOfProject = i.ScopeAndSizeOfProject,
                AppointmentStatus = i.AppointmentStatus,
                DesignStatus = i.DesignStatus,
                DesignerNotes = i.DesignerNotes,
                Price = i.Price,
                Area = i.Area,
                PackageName = i.ProjectPackages.Select(i => i.Package.Name).FirstOrDefault(),
            }).ToListAsync();

            var ViewModel = new EmployeeProjectViewModel();

            ViewModel.Projects = itemsToDisplay;
            ViewModel.Designers = await DesignersWithProjectsCount();


            return View(ViewModel);
        }

        

        public async Task<IActionResult> Details(int Id)
        {
            var project = await context.Projects.Where(i => i.Id == Id).Select(i => new EmployeeProjectsDetailsViewModel()
            {
                Id = Id,
                Phone = i.Phone,
                AppointmentPhone = i.AppointmentPhone,
                ScheduleDate = i.ScheduleDate,
                Notes = i.Notes,
                DesignImagePath = AppHost.Url + i.DesignImagePath,
                Status = i.Status,
                StatusText = StatusHelper.StatusText(i.Status),
                ClientId = i.ClientId,
                ClientName = i.Client.FirstName + " " + i.Client.LastName,
                FirstName = i.Client.FirstName,
                LastName = i.Client.LastName,
                Email = i.Client.Email,
                Address = i.Client.Address,
                City = i.Client.City,
                ZipCode = i.Client.ZipCode,
                CountryId = i.Client.CountryId,
                CountryName = i.Client.Country != null ? i.Client.Country.Name : "",
                StateId = i.Client.StateId,
                StateName = i.Client.State != null ? i.Client.State.Name : "",
                YouWantADesignForWahtAreaAnswer = i.YouWantADesignForWahtAreaAnswer,
                YouWantADesignForWahtAreaNotes = i.YouWantADesignForWahtAreaNotes,
                WhatSortOfStyleAnswer = i.WhatSortOfStyleAnswer,
                WhatSortOfStyleNotes = i.WhatSortOfStyleNotes,
                DoYouHaveKidsAnswer = i.DoYouHaveKidsAnswer,
                DoYouHaveKidsNotes = i.DoYouHaveKidsNotes,
                DoyouEntertainALotAnswer = i.DoyouEntertainALotAnswer,
                DoyouEntertainALotNotes = i.DoyouEntertainALotNotes,
                IsThePropertyVisibleOnGoogleMapsAnswer = i.IsThePropertyVisibleOnGoogleMapsAnswer,
                IsThePropertyVisibleOnGoogleMapsNotes = i.IsThePropertyVisibleOnGoogleMapsNotes,
                WhatYourAreLookingToKeepAnswer = i.WhatYourAreLookingToKeepAnswer,
                WhatYourAreLookingToKeepNotes = i.WhatYourAreLookingToKeepNotes,
                WouldYouLikeToAddHardscapeAnswer = i.WouldYouLikeToAddHardscapeAnswer,
                WouldYouLikeToAddHardscapeNotes = i.WouldYouLikeToAddHardscapeNotes,
                RegardingTheAmountOfPlantsAnswer = i.RegardingTheAmountOfPlantsAnswer,
                RegardingTheAmountOfPlantsNotes = i.RegardingTheAmountOfPlantsNotes,
                DoYouWantAWaterFeatureAnswer = i.DoYouWantAWaterFeatureAnswer,
                DoYouWantAWaterFeatureNotes = i.DoYouWantAWaterFeatureNotes,
                DoYouWantABBQAreaAnswer = i.DoYouWantABBQAreaAnswer,
                DoYouWantABBQAreaNotes = i.DoYouWantABBQAreaNotes,
                DoYoWantToHaveAFirePitAreaAnswer = i.DoYoWantToHaveAFirePitAreaAnswer,
                DoYoWantToHaveAFirePitAreaNotes = i.DoYoWantToHaveAFirePitAreaNotes,
                DoYouWantAGrassAreaAnswer = i.DoYouWantAGrassAreaAnswer,
                DoYouWantAGrassAreaNotes = i.DoYouWantAGrassAreaNotes,
                DoYouLikeColorfulOrSimpleGreenAndWhitePlantsAnswer = i.DoYouLikeColorfulOrSimpleGreenAndWhitePlantsAnswer,
                DoYouLikeColorfulOrSimpleGreenAndWhitePlantsNotes = i.DoYouLikeColorfulOrSimpleGreenAndWhitePlantsNotes,
                DoYouWantAnyPrivacyAnswer = i.DoYouWantAnyPrivacyAnswer,
                DoYouWantAnyPrivacyNotes1 = i.DoYouWantAnyPrivacyNotes1,
                DoYouWantAnyPrivacyNotes2 = i.DoYouWantAnyPrivacyNotes2,
                DoYouHaveOrWantAPergolaOrCoveredPatioAreaAnswer = i.DoYouHaveOrWantAPergolaOrCoveredPatioAreaAnswer,
                DoYouHaveOrWantAPergolaOrCoveredPatioAreaNotes = i.DoYouHaveOrWantAPergolaOrCoveredPatioAreaNotes,
                FYWhatAreYouLookingToKeepOrRemoveInYourYardAnswer = i.FYWhatAreYouLookingToKeepOrRemoveInYourYardAnswer,
                FYWhatAreYouLookingToKeepOrRemoveInYourYardNotes = i.FYWhatAreYouLookingToKeepOrRemoveInYourYardNotes,
                FYWouldYouLikeToAddAnyTypeOfHardscapeAnswer = i.FYWouldYouLikeToAddAnyTypeOfHardscapeAnswer,
                FYWouldYouLikeToAddAnyTypeOfHardscapeNotes = i.FYWouldYouLikeToAddAnyTypeOfHardscapeNotes,
                FYRegardingTheAmountOfPlantsAreYouLookingForSomethingMoreAnswer = i.FYRegardingTheAmountOfPlantsAreYouLookingForSomethingMoreAnswer,
                FYRegardingTheAmountOfPlantsAreYouLookingForSomethingMoreNotes = i.FYRegardingTheAmountOfPlantsAreYouLookingForSomethingMoreNotes,
                FYDoYouWantToDoAnythingDifferentWithYourEntranceAnswer = i.FYDoYouWantToDoAnythingDifferentWithYourEntranceAnswer,
                FYDoYouWantToDoAnythingDifferentWithYourEntranceNotes = i.FYDoYouWantToDoAnythingDifferentWithYourEntranceNotes,
                FYDoYouWantAWaterFeatureAnswer = i.FYDoYouWantAWaterFeatureAnswer,
                FYDoYouWantAWaterFeatureNotes = i.FYDoYouWantAWaterFeatureNotes,
                FYDoYouWantAGrassAreaAnswer = i.FYDoYouWantAGrassAreaAnswer,
                FYDoYouWantAGrassAreaNotes = i.FYDoYouWantAGrassAreaNotes,
                FYDoYouWantAnyTallTreesInYourFrontYardAnswer = i.FYDoYouWantAnyTallTreesInYourFrontYardAnswer,
                FYDoYouWantAnyTallTreesInYourFrontYardNotes = i.FYDoYouWantAnyTallTreesInYourFrontYardNotes,
                FYDoYouLikeColorfulOrSimpleGreenAndWhitePlantsAnswer = i.FYDoYouLikeColorfulOrSimpleGreenAndWhitePlantsAnswer,
                FYDoYouLikeColorfulOrSimpleGreenAndWhitePlantsNotes = i.FYDoYouLikeColorfulOrSimpleGreenAndWhitePlantsNotes,
                FYDoYouWantAnyPrivacyAnswer = i.FYDoYouWantAnyPrivacyAnswer,
                FYDoYouWantAnyPrivacyNotes1 = i.FYDoYouWantAnyPrivacyNotes1,
                FYDoYouWantAnyPrivacyNotes2 = i.FYDoYouWantAnyPrivacyNotes2,
                FYDoYouHaveASideYardAnswer = i.FYDoYouHaveASideYardAnswer,
                FYDoYouHaveASideYardNotes = i.FYDoYouHaveASideYardNotes,
                FYWhatIsYourBudgetForYourYardRenovationAnswer = i.FYWhatIsYourBudgetForYourYardRenovationAnswer,
                FYWhatIsYourBudgetForYourYardRenovationNotes = i.FYWhatIsYourBudgetForYourYardRenovationNotes,
                FYWhenDoYouPlanOnStartingYourYardRenovationAnswer = i.FYWhenDoYouPlanOnStartingYourYardRenovationAnswer,
                FYWhenDoYouPlanOnStartingYourYardRenovationNotes = i.FYWhenDoYouPlanOnStartingYourYardRenovationNotes,
                SummaryNotes = i.SummaryNotes,
                ScopeAndSizeOfProject = i.ScopeAndSizeOfProject,
                AppointmentStatus = i.AppointmentStatus,
                DesignStatus = i.DesignStatus,
                DesignerNotes = i.DesignerNotes,
                Price = i.Price,
                Area = i.Area,
                PackageName = i.ProjectPackages.Select(i => i.Package.Name).FirstOrDefault(),
                Images = i.Images.Where(i => i.Type == (int)ImageType.Image).Select(p => AppHost.Url + p.ImagePath).ToList(),
                Inspirations = i.Images.Where(i => i.Type == (int)ImageType.Inspiration).Select(p => AppHost.Url + p.ImagePath).ToList(),
                DesignLinks = i.DesignDocs.Where(i => i.Type == (int)DocType.Design).Select(p => new ImageViewModel() { Id = p.Id, ImgPath = AppHost.Url + p.DocPath }).ToList(),
                DocumentLinks = i.DesignDocs.Where(i => i.Type == (int)DocType.Doc).Select(p => new ImageViewModel() { Id = p.Id, ImgPath = AppHost.Url + p.DocPath }).ToList(),
                ProjectManagerId = i.ProjectManagerId,
                DesignerId = i.DesignerId,
            }).FirstOrDefaultAsync();

            if (project != null)
            {
                project.Designers = await Designers(project.DesignerId);
                project.Employees = await Employees(project.ProjectManagerId);
            }

            return View(project);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeProjectsDetailsViewModel model)
        {
            var project = await context.Projects.Include(i => i.DesignDocs).FirstOrDefaultAsync(i => i.Id == model.Id);

            if (project == null)
            {
                return BadRequest();
            }

            project.YouWantADesignForWahtAreaAnswer = model.YouWantADesignForWahtAreaAnswer;
            project.YouWantADesignForWahtAreaNotes = model.YouWantADesignForWahtAreaNotes;
            project.WhatSortOfStyleAnswer = model.WhatSortOfStyleAnswer;
            project.WhatSortOfStyleNotes = model.WhatSortOfStyleNotes;
            project.DoYouHaveKidsAnswer = model.DoYouHaveKidsAnswer;
            project.DoYouHaveKidsNotes = model.DoYouHaveKidsNotes;
            project.DoyouEntertainALotAnswer = model.DoyouEntertainALotAnswer;
            project.DoyouEntertainALotNotes = model.DoyouEntertainALotNotes;
            project.IsThePropertyVisibleOnGoogleMapsAnswer = model.IsThePropertyVisibleOnGoogleMapsAnswer;
            project.IsThePropertyVisibleOnGoogleMapsNotes = model.IsThePropertyVisibleOnGoogleMapsNotes;
            project.WhatYourAreLookingToKeepAnswer = model.WhatYourAreLookingToKeepAnswer;
            project.WhatYourAreLookingToKeepNotes = model.WhatYourAreLookingToKeepNotes;
            project.WouldYouLikeToAddHardscapeAnswer = model.WouldYouLikeToAddHardscapeAnswer;
            project.WouldYouLikeToAddHardscapeNotes = model.WouldYouLikeToAddHardscapeNotes;
            project.RegardingTheAmountOfPlantsAnswer = model.RegardingTheAmountOfPlantsAnswer;
            project.RegardingTheAmountOfPlantsNotes = model.RegardingTheAmountOfPlantsNotes;
            project.DoYouWantAWaterFeatureAnswer = model.DoYouWantAWaterFeatureAnswer;
            project.DoYouWantAWaterFeatureNotes = model.DoYouWantAWaterFeatureNotes;
            project.DoYouWantABBQAreaAnswer = model.DoYouWantABBQAreaAnswer;
            project.DoYouWantABBQAreaNotes = model.DoYouWantABBQAreaNotes;
            project.DoYoWantToHaveAFirePitAreaAnswer = model.DoYoWantToHaveAFirePitAreaAnswer;
            project.DoYoWantToHaveAFirePitAreaNotes = model.DoYoWantToHaveAFirePitAreaNotes;
            project.DoYouWantAGrassAreaAnswer = model.DoYouWantAGrassAreaAnswer;
            project.DoYouWantAGrassAreaNotes = model.DoYouWantAGrassAreaNotes;
            project.DoYouLikeColorfulOrSimpleGreenAndWhitePlantsAnswer = model.DoYouLikeColorfulOrSimpleGreenAndWhitePlantsAnswer;
            project.DoYouLikeColorfulOrSimpleGreenAndWhitePlantsNotes = model.DoYouLikeColorfulOrSimpleGreenAndWhitePlantsNotes;
            project.DoYouWantAnyPrivacyAnswer = model.DoYouWantAnyPrivacyAnswer;
            project.DoYouWantAnyPrivacyNotes1 = model.DoYouWantAnyPrivacyNotes1;
            project.DoYouWantAnyPrivacyNotes2 = model.DoYouWantAnyPrivacyNotes2;
            project.DoYouHaveOrWantAPergolaOrCoveredPatioAreaAnswer = model.DoYouHaveOrWantAPergolaOrCoveredPatioAreaAnswer;
            project.DoYouHaveOrWantAPergolaOrCoveredPatioAreaNotes = model.DoYouHaveOrWantAPergolaOrCoveredPatioAreaNotes;
            project.FYWhatAreYouLookingToKeepOrRemoveInYourYardAnswer = model.FYWhatAreYouLookingToKeepOrRemoveInYourYardAnswer;
            project.FYWhatAreYouLookingToKeepOrRemoveInYourYardNotes = model.FYWhatAreYouLookingToKeepOrRemoveInYourYardNotes;
            project.FYWouldYouLikeToAddAnyTypeOfHardscapeAnswer = model.FYWouldYouLikeToAddAnyTypeOfHardscapeAnswer;
            project.FYWouldYouLikeToAddAnyTypeOfHardscapeNotes = model.FYWouldYouLikeToAddAnyTypeOfHardscapeNotes;
            project.FYRegardingTheAmountOfPlantsAreYouLookingForSomethingMoreAnswer = model.FYRegardingTheAmountOfPlantsAreYouLookingForSomethingMoreAnswer;
            project.FYRegardingTheAmountOfPlantsAreYouLookingForSomethingMoreNotes = model.FYRegardingTheAmountOfPlantsAreYouLookingForSomethingMoreNotes;
            project.FYDoYouWantToDoAnythingDifferentWithYourEntranceAnswer = model.FYDoYouWantToDoAnythingDifferentWithYourEntranceAnswer;
            project.FYDoYouWantToDoAnythingDifferentWithYourEntranceNotes = model.FYDoYouWantToDoAnythingDifferentWithYourEntranceNotes;
            project.FYDoYouWantAWaterFeatureAnswer = model.FYDoYouWantAWaterFeatureAnswer;
            project.FYDoYouWantAWaterFeatureNotes = model.FYDoYouWantAWaterFeatureNotes;
            project.FYDoYouWantAGrassAreaAnswer = model.FYDoYouWantAGrassAreaAnswer;
            project.FYDoYouWantAGrassAreaNotes = model.FYDoYouWantAGrassAreaNotes;
            project.FYDoYouWantAnyTallTreesInYourFrontYardAnswer = model.FYDoYouWantAnyTallTreesInYourFrontYardAnswer;
            project.FYDoYouWantAnyTallTreesInYourFrontYardNotes = model.FYDoYouWantAnyTallTreesInYourFrontYardNotes;
            project.FYDoYouLikeColorfulOrSimpleGreenAndWhitePlantsAnswer = model.FYDoYouLikeColorfulOrSimpleGreenAndWhitePlantsAnswer;
            project.FYDoYouLikeColorfulOrSimpleGreenAndWhitePlantsNotes = model.FYDoYouLikeColorfulOrSimpleGreenAndWhitePlantsNotes;
            project.FYDoYouWantAnyPrivacyAnswer = model.FYDoYouWantAnyPrivacyAnswer;
            project.FYDoYouWantAnyPrivacyNotes1 = model.FYDoYouWantAnyPrivacyNotes1;
            project.FYDoYouWantAnyPrivacyNotes2 = model.FYDoYouWantAnyPrivacyNotes2;
            project.FYDoYouHaveASideYardAnswer = model.FYDoYouHaveASideYardAnswer;
            project.FYDoYouHaveASideYardNotes = model.FYDoYouHaveASideYardNotes;
            project.FYWhatIsYourBudgetForYourYardRenovationAnswer = model.FYWhatIsYourBudgetForYourYardRenovationAnswer;
            project.FYWhatIsYourBudgetForYourYardRenovationNotes = model.FYWhatIsYourBudgetForYourYardRenovationNotes;
            project.FYWhenDoYouPlanOnStartingYourYardRenovationAnswer = model.FYWhenDoYouPlanOnStartingYourYardRenovationAnswer;
            project.FYWhenDoYouPlanOnStartingYourYardRenovationNotes = model.FYWhenDoYouPlanOnStartingYourYardRenovationNotes;
            project.SummaryNotes = model.SummaryNotes;
            project.ScopeAndSizeOfProject = model.ScopeAndSizeOfProject;
            project.AppointmentStatus = model.AppointmentStatus;
            project.DesignStatus = model.DesignStatus;
            project.DesignerNotes = model.DesignerNotes;
            project.Status = model.Status;
            project.ProjectManagerId = !string.IsNullOrEmpty(model.ProjectManagerId) ? model.ProjectManagerId : project.ProjectManagerId;
            project.DesignerId = !string.IsNullOrEmpty(model.DesignerId) ? model.DesignerId : project.DesignerId;


            if (model.DesignFiles != null)
            {
                var DesignDocs = model.DesignFiles.Select(i => new DesignDoc() { Type = (int)DocType.Design, DocPath = uploadHelper.Upload(i, (int)FolderName.ProjectFile) });
                foreach (var item in DesignDocs)
                {
                    project.DesignDocs.Add(item);
                }
            }

            if (model.DocumentFiles != null)
            {
                var Docs = model.DocumentFiles.Select(i => new DesignDoc() { Type = (int)DocType.Doc, DocPath = uploadHelper.Upload(i, (int)FolderName.ProjectFile) });
                foreach (var item in Docs)
                {
                    project.DesignDocs.Add(item);
                }
            }

            context.Update(project);
            await context.SaveChangesAsync();



            return RedirectToAction(nameof(Details), new { Id = model.Id });


        }

        public async Task<IActionResult> DeleteFile(int Id)
        {
            var File = await context.DesignDocs.FirstOrDefaultAsync(i => i.Id == Id);

            context.Remove(File);
            await context.SaveChangesAsync();


            return Json(new
            {
                data = true,
            });
        }

        #region Functions

        async Task<SelectList> DesignersWithProjectsCount(string SelectedValue = "")
        {
            var DesignerRole = ((int)Roles.Desinger).ToString();

            var designers = await context.Users.Where(i => i.MainRoles.Contains(DesignerRole)).Select(i => new DropdownViewModel<string>()
            {
                Value = i.Id,
                Text = $"{i.FirstName} {i.LastName} - {i.DesignerProjects.Count}"
            }).ToListAsync();

            return new SelectList(designers, nameof(DropdownViewModel<string>.Value), nameof(DropdownViewModel<string>.Text), SelectedValue);

        }

        async Task<SelectList> Designers(string SelectedValue = "")
        {
            var designers = (await userManager.GetUsersInRoleAsync(Roles.Desinger.ToString())).Select(i => new DropdownViewModel<string>()
            {
                Value = i.Id,
                Text = $"{i.FirstName} {i.LastName}"
            });

            return new SelectList(designers, nameof(DropdownViewModel<string>.Value), nameof(DropdownViewModel<string>.Text), SelectedValue);

        }

        async Task<SelectList> Employees(string SelectedValue = "")
        {
            var designers = (await userManager.GetUsersInRoleAsync(Roles.ProjectManger.ToString())).Select(i => new DropdownViewModel<string>()
            {
                Value = i.Id,
                Text = $"{i.FirstName} {i.LastName}"
            });

            return new SelectList(designers, nameof(DropdownViewModel<string>.Value), nameof(DropdownViewModel<string>.Text), SelectedValue);

        }

        #endregion
    }
}
