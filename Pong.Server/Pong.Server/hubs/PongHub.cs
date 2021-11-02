using System.Threading.Tasks;
using Model;
using Microsoft.AspNetCore.SignalR;

namespace Pong.Server.hubs
{
    public class PongHub: Hub
    {
        public async Task SendMessage(PongGame pongGame)
        {
            await Clients.All.SendAsync("ReceiveMessage", pongGame);
        }
    }
}
