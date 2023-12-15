using DesignPlatform.ViewModels.PaginationViewModels;

namespace DesignPlatform.Areas.Admin.ViewModels.AdminProjectPortfolioViewModels
{
    public class AdminProjectPortfolioViewModel : ListBaseViewModel
    {
        public IEnumerable<AdminProjectPortfolioListViewModel> Items { get; set; }
        public AdminProjectPortfolioAddViewModel Add { get; set; }
        public AdminProjectPortfolioEditViewModel Edit { get; set; }
        public AdminProjectPortfolioParamsViewModel Params { get; set; }
    }
}
