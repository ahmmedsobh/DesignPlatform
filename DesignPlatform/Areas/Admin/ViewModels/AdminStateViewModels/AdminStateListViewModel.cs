using DesignPlatform.ViewModels.PaginationViewModels;

namespace DesignPlatform.Areas.Admin.ViewModels.AdminStateViewModels
{
    public class AdminStateListViewModel : ListBaseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string CountryName { get; set; }
    }
}
