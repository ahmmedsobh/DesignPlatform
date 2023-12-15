using DesignPlatform.Enums;
using DesignPlatform.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DesignPlatform.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthorizeRoles(Roles.Admin)]

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
