using DesignPlatform.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DesignPlatform.Areas.Admin.ViewModels.AdminPackageViewModels
{
    public class AdminPackageEditViewModel
    {
        public AdminPackageEditViewModel()
        {
            Features = new List<DropdownViewModel<int>>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal FrontYardPrice { get; set; }
        public decimal BackYardPrice { get; set; }
        public decimal FrontBackYardPrice { get; set; }
        public IFormFile ImageFile { get; set; }
        public List<DropdownViewModel<int>> Features { get; set; }

        public int[] SubPackageIds { get; set; }

        public MultiSelectList SubPackages { get; set; }
    }
}
