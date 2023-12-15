using DesignPlatform.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DesignPlatform.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().HasQueryFilter(c => !c.IsDeleted);
        }

        public DbSet<Image> Images { get; set; }
        public DbSet<Packagee> Packagees { get; set; }
        public DbSet<PackageFeature> PackageFeatures { get; set; }
        public DbSet<PackageSubPackage> PackageSubPackages { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectPackage> ProjectPackages { get; set; }
        public DbSet<ProjectSubPackage> ProjectSubPackages { get; set; }
        public DbSet<SubPackage> SubPackages { get; set; }
        public DbSet<DesignDoc> DesignDocs { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<SubPackageFeature> SubPackageFeatures { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ProjectPortfolio> ProjectPortfolios { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}