using DesignPlatform.Data;
using DesignPlatform.Enums;
using DesignPlatform.Extensions;
using DesignPlatform.Helpers.CurrentUserService;
using DesignPlatform.Models;
using DesignPlatform.ViewModels.MessagesViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DesignPlatform.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthorizeRoles(Roles.Admin)]
    public class MessagesController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly ICurrentUserService currentUserService;

        public MessagesController(ApplicationDbContext context, ICurrentUserService currentUserService)
        {
            this.context = context;
            this.currentUserService = currentUserService;
        }

        public async Task<IActionResult> Index(int Id)
        {
            var project = await context.Projects.FirstOrDefaultAsync(i=> i.Id == Id);


            if(project == null)
            {
                return BadRequest();
            }


            var messages = await context.Messages.Where(i => (i.SenderId == project.ProjectManagerId || i.SenderId == project.ClientId) && (i.ReceiverId == project.ProjectManagerId || i.ReceiverId == project.ClientId)).Select(i => new MessagesListViewModel()
            {
                Id = i.Id,
                Message = i.Title,
                Date = i.Date.ToShortDateString(),
                Time = i.Date.ToShortTimeString(),
                IsMyMessage = i.SenderId == project.ProjectManagerId,
            }).ToListAsync();

            var ClientRole = ((int)Roles.Client).ToString();

            

            var ViewModel = new MessagesViewModel()
            {
                Messages = messages,
            };


            return View(ViewModel);
        }

        public async Task<IActionResult> IntChat(int Id)
        {
            var project = await context.Projects.FirstOrDefaultAsync(i => i.Id == Id);


            if (project == null)
            {
                return BadRequest();
            }

            var messages = await context.Messages.Where(i => (i.SenderId == project.ProjectManagerId || i.SenderId == project.DesignerId) && (i.ReceiverId == project.ProjectManagerId || i.ReceiverId == project.DesignerId)).Select(i => new MessagesListViewModel()
            {
                Id = i.Id,
                Message = i.Title,
                Date = i.Date.ToShortDateString(),
                Time = i.Date.ToShortTimeString(),
                IsMyMessage = i.SenderId == project.ProjectManagerId,
            }).ToListAsync();

            var ClientRole = ((int)Roles.Desinger).ToString();

           

            var ViewModel = new MessagesViewModel()
            {
                Messages = messages,
            };


            return View(ViewModel);
        }
    }
}
