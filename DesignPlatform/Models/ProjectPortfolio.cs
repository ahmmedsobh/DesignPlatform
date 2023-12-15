namespace DesignPlatform.Models
{
    public class ProjectPortfolio : BaseEntity
    {
        public string ImagePath { get; set; }

        public string Description { get; set; }

        public virtual ICollection<ProjectPortfolioImage> Images { get; set; }
    }
}
