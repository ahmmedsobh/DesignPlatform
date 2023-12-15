using System.ComponentModel.DataAnnotations.Schema;

namespace DesignPlatform.Models
{
    public class ProjectPortfolioImage
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }

        public int ProjectPortfolioId { get; set; }

        [ForeignKey(nameof(ProjectPortfolioId))]
        public ProjectPortfolio ProjectPortfolio { get; set; }
    }
}
