using System.Threading.Tasks;
using Model;

namespace Pong.Server.Hubs.Clients
{
    public interface IChatClient
    {
        Task ReceiveMessage(PongGame message);
    }
}