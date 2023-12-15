using System.ComponentModel.DataAnnotations;

namespace DesignPlatform.Areas.Admin.ViewModels.AdminProjectPortfolioViewModels
{
    public class AdminProjectPortfolioAddViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public IFormFile ImageFile { get; set; }

        public IFormFile[] ImageFiles { get; set; }


        [Required]
        public string Description { get; set; }
    }
}
