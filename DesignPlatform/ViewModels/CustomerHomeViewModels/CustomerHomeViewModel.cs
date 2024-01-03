using DesignPlatform.Areas.Admin.ViewModels.AdminProjectPortfolioViewModels;
using DesignPlatform.Areas.Admin.ViewModels.AdminReviewViewModels;
using DesignPlatform.Models;

namespace DesignPlatform.ViewModels.CustomerHomeViewModels
{
    public class CustomerHomeViewModel
    {
        public CustomerHomeViewModel()
        {
            Packages = new List<CustomerHomePackageViewModel>();
        }

        public IEnumerable<CustomerHomePackageViewModel> Packages { get; set; }
        public IEnumerable<AdminProjectPortfolioEditViewModel> Projects { get; set; }
        public IEnumerable<AdminReviewEditViewModel> Reviews { get; set; }

    }
}
