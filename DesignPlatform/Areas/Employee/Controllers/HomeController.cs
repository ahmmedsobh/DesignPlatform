using DesignPlatform.Enums;
using DesignPlatform.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DesignPlatform.Areas.Employee.Controllers
{
    [Area("Employee")]
    [AuthorizeRoles(Roles.ProjectManger)]

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        
    }
}
