using System.ComponentModel.DataAnnotations.Schema;

namespace DesignPlatform.Models
{
    public class ProjectPackage
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int PackageId { get; set; }

        [ForeignKey(nameof(PackageId))]
        public Packagee Package { get; set; }

        
        [ForeignKey(nameof(ProjectId))]
        public Project Project { get; set; }
    }
}
