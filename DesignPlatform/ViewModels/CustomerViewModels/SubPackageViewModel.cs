namespace DesignPlatform.ViewModels.CustomerViewModels
{
    public class SubPackageViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OfferHeader { get; set; }
        public string OfferDescription { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public bool IsSubscripe { get; set; }
        public decimal Price { get; set; }
        public List<string> Features { get; set; }        

    }
}
