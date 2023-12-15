using DesignPlatform.ViewModels.PaginationViewModels;

namespace DesignPlatform.Areas.Admin.ViewModels.AdminPackageViewModels
{
    public class AdminPackageListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal FrontYardPrice { get; set; }
        public decimal BackYardPrice { get; set; }
        public decimal FrontBackYardPrice { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }



    }
}
