
using DesignPlatform.ViewModels;

namespace DesignPlatform.Areas.Admin.ViewModels.AdminSubPackagesViewModels
{
    public class AdminSubPackageEditViewModel
    {
        public AdminSubPackageEditViewModel()
        {
            Features = new List<DropdownViewModel<int>>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile ImageFile { get; set; }
        public decimal Price { get; set; }
        public string OfferHeader { get; set; }
        public string OfferDescription { get; set; }
        public string Description { get; set; }

        public List<DropdownViewModel<int>> Features { get; set; }

    }
}
