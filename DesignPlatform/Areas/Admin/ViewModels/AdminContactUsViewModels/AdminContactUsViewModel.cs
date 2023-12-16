using DesignPlatform.ViewModels.PaginationViewModels;

namespace DesignPlatform.Areas.Admin.ViewModels.AdminContactUsViewModels
{
    public class AdminContactUsViewModel : ListBaseViewModel
    {
        public IEnumerable<AdminContactUsListViewModel> Items { get; set; }
        public AdminContactUsEditViewModel Edit { get; set; }
        public AdminContactUsParamsViewModel Params { get; set; }
    }
}
