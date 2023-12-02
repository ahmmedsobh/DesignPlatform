using System.ComponentModel.DataAnnotations.Schema;

namespace DesignPlatform.Models
{
    public class PackageSubPackage
    {
        public int Id { get; set; }
        public int PackageId { get; set; }
        public int SubPackageId { get; set; }

        [ForeignKey(nameof(PackageId))]
        public Packagee Package { get; set; }

        [ForeignKey(nameof(SubPackageId))]
        public SubPackage SubPackage { get; set; }
    }
}
