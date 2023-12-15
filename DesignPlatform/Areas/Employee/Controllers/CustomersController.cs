using DesignPlatform.Areas.Employee.ViewModels.EmployeeCustomerViewModels;
using DesignPlatform.Areas.Employee.ViewModels.EmployeeProjectsViewModels;
using DesignPlatform.Data;
using DesignPlatform.Enums;
using DesignPlatform.Extensions;
using DesignPlatform.Helpers;
using DesignPlatform.Helpers.CurrentUserService;
using DesignPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DesignPlatform.Areas.Employee.Controllers
{
    [Area("Employee")]
    [AuthorizeRoles(Roles.ProjectManger)]

    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly ICurrentUserService currentUserService;

        public CustomersController(ApplicationDbContext context , ICurrentUserService currentUserService)
        {
            this.context = context;
            this.currentUserService = currentUserService;
        }

        public async Task<IActionResult> Index(EmployeeCustomerParamsViewModel model)
        {
            var UserId = currentUserService.UserId;

            IQueryable<Project> items = context.Projects.Where(i=> i.ProjectManagerId == UserId);

            if (model.FromDate != DateTime.MinValue)
            {
                items = items.Where(i => i.Date >= model.FromDate);
            }

            if (model.ToDate != DateTime.MinValue)
            {
                items = items.Where(i => i.Date <= model.ToDate);
            }

            if(!string.IsNullOrEmpty(model.SearchString))
            {
                items = items.Where(i => (i.Client.FirstName+ i.Client.LastName + i.Phone + i.Client.Country.Name + i.Client.City + i.Client.State.Name + i.Client.Email).Contains(model.SearchString));
            }

            var itemsToDisplay = await items.Select(i => new EmployeeProjectsDetailsViewModel()
            {
                Id = i.Id,
                Phone = i.Phone,
                AppointmentPhone = i.AppointmentPhone,
                ScheduleDate = i.ScheduleDate,
                Date = i.Date,
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


            return View(ViewModel);
        }
    }
}
