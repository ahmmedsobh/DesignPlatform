using DesignPlatform.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DesignPlatform.ViewModels.AccountViewModels
{
    public class CustomerChangePasswordViewModel
    {
        [Remote(controller: "Account", action: nameof(AccountController.OldPasswordValidation))]
        public string OldPassword { get; set; }

        [Required]
        [Remote(controller: "Account", action: nameof(AccountController.NewPasswordValidation),AdditionalFields = nameof(OldPassword))]
        public string NewPassword { get; set; }


        [Compare(nameof(NewPassword))]
        public string ConfirmPassword { get; set; }
    }
}
