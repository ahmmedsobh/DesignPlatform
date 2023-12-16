using DesignPlatform.Areas.Admin.ViewModels.AdminSettingViewModels;
using DesignPlatform.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DesignPlatform.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingsController : Controller
    {
        private readonly ApplicationDbContext context;

        public SettingsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Edit()
        {
            var setting = await context.Settings.Select(i=> new AdminSettingEditViewModel()
            {
                AboutUs = i.AboutUs,
            }).FirstOrDefaultAsync();
          

            return View(setting);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AdminSettingEditViewModel model)
        {
            var setting = await context.Settings.FirstOrDefaultAsync();

            setting.AboutUs = !string.IsNullOrEmpty(model.AboutUs) ? model.AboutUs : setting.AboutUs;

            context.Update(setting);
            await context.SaveChangesAsync();


            return View(model);
        }
    }
}
