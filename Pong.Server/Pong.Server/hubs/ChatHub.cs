using System.Threading.Tasks;
using Model;
using Microsoft.AspNetCore.SignalR;

namespace Pong.Server.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(ChatMessage message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}