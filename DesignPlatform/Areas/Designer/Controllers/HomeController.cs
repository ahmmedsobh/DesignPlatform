using DesignPlatform.Enums;
using DesignPlatform.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DesignPlatform.Areas.Designer.Controllers
{
    [Area("Designer")]
    [AuthorizeRoles(Roles.Desinger)]

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
