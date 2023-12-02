using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesignPlatform.Models
{
	public class ApplicationUser : IdentityUser
	{
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }

        [InverseProperty(nameof(Project.Client))]
        public virtual ICollection<Project> ClientProjects { get; set; }
       
        [InverseProperty(nameof(Project.ProjectManager))]
        public virtual ICollection<Project> ProjectManagerProjects { get; set; }
       
        [InverseProperty(nameof(Project.Desinger))]
        public virtual ICollection<Project> DesignerProjects { get; set; }


        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; }

        [ForeignKey(nameof(StateId))]
        public State State { get; set; }
    }
}
