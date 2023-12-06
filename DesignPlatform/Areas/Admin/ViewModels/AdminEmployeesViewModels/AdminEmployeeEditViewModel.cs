using Microsoft.AspNetCore.Mvc;

namespace DesignPlatform.Areas.Admin.ViewModels.AdminEmployeesViewModels
{
    public class AdminEmployeeEditViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        [Remote(areaName: "Admin", controller: "Employees", action: "IsEmailExistsById",AdditionalFields = $"{nameof(Email)},{nameof(Id)}")]
        public string Email { get; set; }
        [Remote(areaName: "Admin", controller: "Employees", action: "IsPhoneExistsById", AdditionalFields = $"{nameof(Email)},{nameof(Id)}")]
        public string Phone { get; set; }
    }
}
