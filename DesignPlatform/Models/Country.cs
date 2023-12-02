namespace DesignPlatform.Models
{
    public class Country : BaseEntity
    {
        public virtual ICollection<State> States { get; set; }
        public virtual ICollection<ApplicationUser> Clients { get; set; }
    }
}
