using DesignPlatform.ViewModels.PaginationViewModels;

namespace DesignPlatform.Areas.Admin.ViewModels.AdminStateViewModels
{
    public class AdminStateViewModel : ListBaseViewModel
    {
        public IEnumerable<AdminStateListViewModel> Items { get; set; }
        public AdminStateAddViewModel Add { get; set; }
        public AdminStateEditViewModel Edit { get; set; }
        public AdminStateParamsViewModel Params { get; set; }
    }
}
