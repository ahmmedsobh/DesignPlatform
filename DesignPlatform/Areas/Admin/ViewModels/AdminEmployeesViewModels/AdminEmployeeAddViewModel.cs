using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DesignPlatform.Areas.Admin.ViewModels.AdminEmployeesViewModels
{
    public class AdminEmployeeAddViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]

        [Remote(areaName:"Admin",controller:"Employees",action: "IsEmailExists")]
        public string Email { get; set; }
        [Required]
        [Remote(areaName: "Admin", controller: "Employees", action: "IsPhoneExists")]
        public string Phone { get; set; }
        public string Password { get; set; }
    }
}
