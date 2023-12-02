namespace DesignPlatform.Models
{
    public class Packagee : BaseEntity
    {
        public double Rate { get; set; }
        public string ImagePath { get; set; }
        public decimal FrontYardPrice { get; set; }
        public decimal BackYardPrice { get; set; }
        public decimal FrontBackYardPrice { get; set; }

        public virtual ICollection<PackageFeature> PackageFeatures { get; set; }
        public virtual ICollection<PackageSubPackage> PackageSubPackages { get; set; }
        public virtual ICollection<ProjectPackage> ProjectPackages { get; set; }
    }
}
