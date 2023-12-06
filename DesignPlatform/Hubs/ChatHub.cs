using DesignPlatform.Data;
using DesignPlatform.Helpers.CurrentUserService;
using DesignPlatform.Models;
using Microsoft.AspNetCore.SignalR;

namespace DesignPlatform.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext context;
        private readonly ICurrentUserService currentUserService;

        public ChatHub(ApplicationDbContext context,ICurrentUserService currentUserService)
        {
            this.context = context;
            this.currentUserService = currentUserService;
        }


        public async Task SendMessage(string receiverId, string message)
        {
            var NowDate = DateTime.Now;
            var date = NowDate.ToShortDateString();
            var time = NowDate.ToShortTimeString();

            string senderId = currentUserService.UserId;

            var messageToAdd = new Message()
            {
                Title = message,
                Date = NowDate,
                SenderId = senderId,
                ReceiverId = receiverId,
            };

            await context.AddAsync(messageToAdd);
            await context.SaveChangesAsync();

            var c = Context.ConnectionId;

            List<string> users = new List<string>()
            {
                receiverId,senderId
            };


            await Clients.Users(users).SendAsync("ReceiveMessage", message, date, time, senderId);

        }
    }
}
