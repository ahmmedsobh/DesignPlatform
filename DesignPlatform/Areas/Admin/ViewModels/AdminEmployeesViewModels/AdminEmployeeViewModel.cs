using DesignPlatform.ViewModels.PaginationViewModels;

namespace DesignPlatform.Areas.Admin.ViewModels.AdminEmployeesViewModels
{
    public class AdminEmployeeViewModel : ListBaseViewModel
    {
        public IEnumerable<AdminEmployeeListViewModel> Items { get; set; }
        public AdminEmployeeAddViewModel Add { get; set; }
        public AdminEmployeeEditViewModel Edit { get; set; }
        public AdminEmployeeParamsViewModel Params { get; set; }

    }
}
