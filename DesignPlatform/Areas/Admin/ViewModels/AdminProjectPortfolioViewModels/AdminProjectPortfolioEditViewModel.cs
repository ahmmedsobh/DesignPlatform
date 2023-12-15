namespace DesignPlatform.Areas.Admin.ViewModels.AdminProjectPortfolioViewModels
{
    public class AdminProjectPortfolioEditViewModel
    {
        public AdminProjectPortfolioEditViewModel()
        {
            ImagesUrl = new List<string>();
            Projects = new List<AdminProjectPortfolioEditViewModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile ImageFile { get; set; }
        public IFormFile[] ImageFiles { get; set; }
        public string ImageUrl { get; set; }
        public List<string> ImagesUrl { get; set; }
        public string Description { get; set; }

        public List<AdminProjectPortfolioEditViewModel> Projects { get; set; }
    }
}
