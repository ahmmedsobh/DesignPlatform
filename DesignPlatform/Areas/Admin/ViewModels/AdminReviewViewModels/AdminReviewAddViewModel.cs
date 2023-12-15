using System.ComponentModel.DataAnnotations;

namespace DesignPlatform.Areas.Admin.ViewModels.AdminReviewViewModels
{
    public class AdminReviewAddViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
