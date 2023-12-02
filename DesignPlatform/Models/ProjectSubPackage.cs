using System.ComponentModel.DataAnnotations.Schema;

namespace DesignPlatform.Models
{
    public class ProjectSubPackage
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int SubPackageId { get; set; }

        [ForeignKey(nameof(SubPackageId))]
        public SubPackage SubPackage { get; set; }


        [ForeignKey(nameof(ProjectId))]
        public Project Project { get; set; }

        public virtual ICollection<ProjectSubPackage> ProjectSubPackages { get; set; }

    }
}
