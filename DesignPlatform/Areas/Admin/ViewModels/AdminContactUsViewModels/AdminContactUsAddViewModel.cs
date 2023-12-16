using System.ComponentModel.DataAnnotations;

namespace DesignPlatform.Areas.Admin.ViewModels.AdminContactUsViewModels
{
    public class AdminContactUsAddViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Message { get; set; }
        public string SuccessMessage { get; set; }
    }
}
