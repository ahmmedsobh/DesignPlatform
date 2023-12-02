using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DesignPlatform.ViewModels.CustomerViewModels
{
    public class ProjectDetailsViewModel
    {
        

        public int Id { get; set; }
        //[Required]
        public IFormFile[] Images { get; set; }
        //[Required]
        public IFormFile[] Inspiration { get; set; }
        //[Required]
        public IFormFile Design { get; set; }
        public string Notes { get; set; }
        public List<ImageViewModel> ImagesLinks { get; set; }
        public List<ImageViewModel> InspirationLinkes { get; set; }
        public string DesignLink { get; set; }
        public List<SubPackageViewModel> SubPackages { get; set; }
    }
}
