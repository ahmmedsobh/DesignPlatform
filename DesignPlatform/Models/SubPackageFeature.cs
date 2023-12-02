using System.ComponentModel.DataAnnotations.Schema;

namespace DesignPlatform.Models
{
    public class SubPackageFeature : BaseEntity
    {
        public decimal Price { get; set; }
        public int SubPackageId { get; set; }
        

        [ForeignKey(nameof(SubPackageId))]
        public SubPackage SubPackage { get; set; }

    }
}
