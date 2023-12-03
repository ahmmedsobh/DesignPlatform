using DesignPlatform.Data;
using DesignPlatform.Helpers.CurrentUserService;
using DesignPlatform.ViewModels.MessagesViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace DesignPlatform.Controllers
{
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
            var User = await currentUserService.GetUser();

            if(User == null)
            {
                return BadRequest();
            }

            var messages = await context.Messages.Where(i => (i.SenderId == userId || i.SenderId == SelectedUser) && (i.ReceiverId == userId || i.ReceiverId == SelectedUser)).OrderByDescending(i=> i.Id).Select(i=> new MessagesListViewModel()
            {
                Id = i.Id,
                Message = i.Title,
                Date = i.Date.ToShortDateString(),
                Time = i.Date.ToShortTimeString(),
                IsMyMessage = i.SenderId == userId,
            }).ToListAsync();

            var users = await context.Users.Where(i => i.ReceivedMessages.Any(x => x.SenderId == userId)).Select(x=> new MessageUsersListViewModel()
            {
                Id = x.Id,
                Name = $"{x.FirstName} {x.LastName}",
                LastMessage = context.Messages.Where(i => (i.SenderId == userId || i.SenderId == i.SenderId) && (i.ReceiverId == userId || i.ReceiverId == i.ReceiverId)).OrderByDescending(i => i.Id).Select(i=> i.Title).FirstOrDefault(),
                Selected = x.Id == SelectedUser,

            }).ToListAsync();

            var ViewModel = new MessagesViewModel()
            {
                Users = users,
                Messages = messages,
                Name = $"{User.FirstName} {User.LastName}",
            };


            return View(ViewModel);
        }


    }
}
