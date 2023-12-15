using DesignPlatform.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DesignPlatform.Areas.Admin.ViewModels.AdminPackageViewModels
{
    public class AdminPackageAddViewModel
    {
        public AdminPackageAddViewModel()
        {
            Features = new List<DropdownViewModel<int>>();
        }

        [Required]
        public string Name { get; set; }
        [Required]

        public decimal FrontYardPrice { get; set; }
        [Required]

        public decimal BackYardPrice { get; set; }
        [Required]

        public decimal FrontBackYardPrice { get; set; }
        [Required]

        public IFormFile ImageFile { get; set; }

        public List<DropdownViewModel<int>> Features { get; set; }
        public int[] SubPackageIds { get; set; }

        public MultiSelectList SubPackages { get; set; }


    }
}
