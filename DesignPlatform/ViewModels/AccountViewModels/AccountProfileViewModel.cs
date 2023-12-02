using DesignPlatform.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DesignPlatform.ViewModels.AccountViewModels
{
    public class AccountProfileViewModel
    {
        public AccountProfileViewModel()
        {
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }

        [Remote(controller: "Account", action: nameof(AccountController.EmailExists))]
        public string Email { get; set; }

        [Remote(controller: "Account", action: nameof(AccountController.PhoneExists))]
        public string Phone { get; set; }

        public SelectList Countries { get; set; }
        public SelectList States { get; set; }

        public List<string> MainPackagesPurchased { get; set; }
        public List<string> SubPackagesPurchased { get; set; }
    }
}
