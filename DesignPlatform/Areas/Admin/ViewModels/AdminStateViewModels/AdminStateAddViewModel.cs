using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DesignPlatform.Areas.Admin.ViewModels.AdminStateViewModels
{
    public class AdminStateAddViewModel
    {
        [Required]
        public string Name { get; set; }
        public int CountryId { get; set; }

        public SelectList Countries { get; set; }
    }
}
