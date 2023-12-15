using DesignPlatform.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace DesignPlatform.Areas.Admin.ViewModels.AdminSubPackagesViewModels
{
    public class AdminSubPackageAddViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]

        public IFormFile ImageFile { get; set; }
        [Required]

        public decimal Price { get; set; }
        [Required]

        public string OfferHeader { get; set; }
        [Required]

        public string OfferDescription { get; set; }
        [Required]

        public string Description { get; set; }

        public List<DropdownViewModel<int>> Features { get; set; }

    }
}
