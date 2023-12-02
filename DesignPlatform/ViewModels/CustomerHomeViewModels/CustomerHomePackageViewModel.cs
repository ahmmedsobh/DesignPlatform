namespace DesignPlatform.ViewModels.CustomerHomeViewModels
{
    public class CustomerHomePackageViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Rate { get; set; }
        public string Image { get; set; }
        public int ReviewsCount { get; set; }
        public List<string> Features { get; set; }

        public PlaceOrderViewModel PlaceOrder { get; set; }
    }
}
