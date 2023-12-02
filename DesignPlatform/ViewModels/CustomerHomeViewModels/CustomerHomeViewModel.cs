namespace DesignPlatform.ViewModels.CustomerHomeViewModels
{
    public class CustomerHomeViewModel
    {
        public CustomerHomeViewModel()
        {
            Packages = new List<CustomerHomePackageViewModel>();
        }

        public IEnumerable<CustomerHomePackageViewModel> Packages { get; set; }
    }
}
