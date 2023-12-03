using DesignPlatform.Data;
using Microsoft.AspNetCore.SignalR;

namespace DesignPlatform.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext context;

        public ChatHub(ApplicationDbContext context)
        {
            this.context = context;
        }


        public async Task SendMessage(string senderId,string receiverId, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", userId, message);
        }
    }
}
