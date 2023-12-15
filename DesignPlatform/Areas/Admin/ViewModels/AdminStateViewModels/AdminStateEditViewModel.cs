using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Options;

namespace DesignPlatform.Areas.Admin.ViewModels.AdminStateViewModels
{
    public class AdminStateEditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }

        public SelectList Countries { get; set; }
    }
}
