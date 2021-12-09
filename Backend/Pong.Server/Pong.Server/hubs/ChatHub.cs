using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Model;

namespace Pong.Server.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(ChatHub message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
