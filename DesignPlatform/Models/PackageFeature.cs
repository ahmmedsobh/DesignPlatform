using System.ComponentModel.DataAnnotations.Schema;

namespace DesignPlatform.Models
{
    public class PackageFeature : BaseEntity
    {
        public decimal Price { get; set; }
        public int PackageId { get; set; }

        [ForeignKey(nameof(PackageId))]
        public virtual Packagee Package { get; set; }
    }
}
