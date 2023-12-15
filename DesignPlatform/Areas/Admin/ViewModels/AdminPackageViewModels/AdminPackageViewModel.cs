using DesignPlatform.ViewModels.PaginationViewModels;

namespace DesignPlatform.Areas.Admin.ViewModels.AdminPackageViewModels
{
    public class AdminPackageViewModel : ListBaseViewModel
    {
        public IEnumerable<AdminPackageListViewModel> Items { get; set; }
        public AdminPackageAddViewModel Add { get; set; }
        public AdminPackageEditViewModel Edit { get; set; }
        public AdminPackageParamsViewModel Params { get; set; }


    }
}
