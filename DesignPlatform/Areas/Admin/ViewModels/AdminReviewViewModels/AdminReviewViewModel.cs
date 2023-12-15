using DesignPlatform.ViewModels.PaginationViewModels;

namespace DesignPlatform.Areas.Admin.ViewModels.AdminReviewViewModels
{
    public class AdminReviewViewModel : ListBaseViewModel
    {
        public IEnumerable<AdminReviewListViewModel> Items { get; set; }
        public AdminReviewAddViewModel Add { get; set; }
        public AdminReviewEditViewModel Edit { get; set; }
        public AdminReviewParamsViewModel Params { get; set; }
    }
}
