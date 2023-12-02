namespace DesignPlatform.Models
{
    public class SubPackage : BaseEntity
    {
        public string ImagePath { get; set; }
        public decimal Price { get; set; }
        public string OfferHeader { get; set; }
        public string OfferDescription { get; set; }
        public string Description { get; set; }
        public virtual ICollection<PackageSubPackage> PackageSubPackages { get; set; }
        public virtual ICollection<SubPackageFeature> SubPackageFeatures { get; set; }
    }
}
