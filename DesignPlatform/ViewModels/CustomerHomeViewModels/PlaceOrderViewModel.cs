using System.ComponentModel.DataAnnotations;

namespace DesignPlatform.ViewModels.CustomerHomeViewModels
{
    public class PlaceOrderViewModel
    {
        public string Area { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string StreetAddress { get; set; }
        [Required]
        public string ZipCode { get; set; }

        public int PackageId { get; set; }
        public string PackageName { get; set; }
        public string PackageImage { get; set; }

        public decimal FrontYardPrice { get; set; }
        public decimal BackYardPrice { get; set; }
        public decimal FrontBackYardPrice { get; set; }
    }
}
