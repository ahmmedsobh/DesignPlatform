using System.ComponentModel.DataAnnotations.Schema;

namespace DesignPlatform.Models
{
    public class State : BaseEntity
    {
        public int CountryId { get; set; }

        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; }

        public virtual ICollection<ApplicationUser> Clients { get; set; }

    }
}
