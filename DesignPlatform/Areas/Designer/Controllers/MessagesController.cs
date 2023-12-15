using DesignPlatform.Data;
using DesignPlatform.Enums;
using DesignPlatform.Extensions;
using DesignPlatform.Helpers.CurrentUserService;
using DesignPlatform.Models;
using DesignPlatform.ViewModels.MessagesViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DesignPlatform.Areas.Designer.Controllers
{
    [Area("Designer")]
    [AuthorizeRoles(Roles.Desinger)]

    public class MessagesController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly ICurrentUserService currentUserService;

        public MessagesController(ApplicationDbContext context, ICurrentUserService currentUserService)
        {
            this.context = context;
            this.currentUserService = currentUserService;
        }

        public async Task<IActionResult> Index(string SelectedUser)
        {
            var userId = currentUserService.UserId;
            var User = await context.Users.FirstOrDefaultAsync(i => i.Id == SelectedUser);

            if (User == null)
            {
                User = new ApplicationUser();
            }

            var messages = await context.Messages.Where(i => (i.SenderId == userId || i.SenderId == SelectedUser) && (i.ReceiverId == userId || i.ReceiverId == SelectedUser)).Select(i => new MessagesListViewModel()
            {
                Id = i.Id,
                Message = i.Title,
                Date = i.Date.ToShortDateString(),
                Time = i.Date.ToShortTimeString(),
                IsMyMessage = i.SenderId == userId,
            }).ToListAsync();

            var ProjectMangerRole = ((int)Roles.ProjectManger).ToString();

            var users = await context.Users.Where(i => i.ReceivedMessages.Any(x => x.SenderId == userId) && i.MainRoles.Contains(ProjectMangerRole)).Select(x => new MessageUsersListViewModel()
            {
                Id = x.Id,
                Name = $"{x.FirstName} {x.LastName}",
                LastMessage = context.Messages.Where(i => (i.SenderId == userId || i.SenderId == i.SenderId) && (i.ReceiverId == userId || i.ReceiverId == i.ReceiverId)).OrderByDescending(i => i.Id).Select(i => i.Title).FirstOrDefault(),
                Selected = x.Id == SelectedUser,

            }).ToListAsync();

            var ViewModel = new MessagesViewModel()
            {
                Users = users,
                Messages = messages,
                ReceiverName = $"{User.FirstName} {User.LastName}",
                ReceiverId = User.Id,
                NoSelectedUser = string.IsNullOrEmpty(User.FirstName),
                SenderId = userId,
            };


            return View(ViewModel);
        }

        
    }
}
