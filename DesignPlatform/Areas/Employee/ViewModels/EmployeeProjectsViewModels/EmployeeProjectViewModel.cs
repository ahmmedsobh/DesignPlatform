using Microsoft.AspNetCore.Mvc.Rendering;

namespace DesignPlatform.Areas.Employee.ViewModels.EmployeeProjectsViewModels
{
    public class EmployeeProjectViewModel
    {
        public IEnumerable<EmployeeProjectsDetailsViewModel> Projects { get; set; }

        public SelectList Designers { get; set; }
    }
}
