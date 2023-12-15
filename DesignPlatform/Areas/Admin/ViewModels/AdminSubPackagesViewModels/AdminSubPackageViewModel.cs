using DesignPlatform.Areas.Admin.ViewModels.AdminPackageViewModels;
using DesignPlatform.ViewModels.PaginationViewModels;

namespace DesignPlatform.Areas.Admin.ViewModels.AdminSubPackagesViewModels
{
    public class AdminSubPackageViewModel : ListBaseViewModel
    {
        public IEnumerable<AdminSubPackageListViewModel> Items { get; set; }
        public AdminSubPackageAddViewModel Add { get; set; }
        public AdminSubPackageEditViewModel Edit { get; set; }
        public AdminSubPackageParamsViewModel Params { get; set; }
    }
}
