using Microsoft.AspNetCore.Mvc;

namespace DesignPlatform.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        
    }
}
