using DesignPlatform.ViewModels.PaginationViewModels;

namespace DesignPlatform.Areas.Admin.ViewModels.AdminCountryViewModels
{
    public class AdminCountryViewModel : ListBaseViewModel
    {
        public IEnumerable<AdminCountryListViewModel> Items { get; set; }
        public AdminCountryAddViewModel Add { get; set; }
        public AdminCountryEditViewModel Edit { get; set; }
        public AdminCountryParamsViewModel Params { get; set; }
    }
}
