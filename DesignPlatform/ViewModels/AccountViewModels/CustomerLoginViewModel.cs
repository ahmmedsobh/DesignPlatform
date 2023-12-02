using DesignPlatform.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DesignPlatform.ViewModels.AccountViewModels
{
    public class CustomerLoginViewModel
    {
        [Required]
        [Remote(controller: "Account", action:nameof(AccountController.LoginEmailValidation))]
        public string Email { get; set; }
        [Required]
        [Remote(controller: "Account", action: nameof(AccountController.LoginPasswordValidation),AdditionalFields = nameof(Email)+","+ nameof(Password))]
        public string Password { get; set; }
    }
}
