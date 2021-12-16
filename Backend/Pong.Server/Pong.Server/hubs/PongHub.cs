using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL;
using Logic;
using Microsoft.AspNetCore.SignalR;
using Model;

namespace Pong.Server.hubs
{
    public class PongHub : Hub
    {
        private readonly GameLogic _gl = new GameLogic();

        public class PlayerPositions
        {
            public string GameName { get; set; }

            public int Position { get; set; }

            public int PlayerType { get; set; }
        }

        public async Task SendPlayerPosition(PlayerPositions position)
        {
            PongGame pongGame;
            if (position.PlayerType == 1)
            {
                pongGame = GameLogic.SetPlayerPosition(position.GameName, position.Position, -1);
            }
            else
            {
                pongGame = GameLogic.SetPlayerPosition(position.GameName, -1, position.Position);
            }

            await Clients.Group(position.GameName).SendAsync("ReceivePlayerPosition", pongGame);
        }

        public async Task TestFuncion(string message)
        {
            await Clients.All.SendAsync("TestReceive", message);
        }

        public async Task CalculateBallPos(string gameName)
        {
            PongGame pongGame = GameLogic.ReturnGame(gameName);
            if (pongGame.GameOver)
            {
                await Clients.Group(gameName).SendAsync("GameOver", pongGame.p1Score, pongGame.p2Score);
            }

            await Clients.Group(gameName).SendAsync("ReceiveBallPosition", GameLogic.calculateBallPos(pongGame));
        }

        public async Task CreateGame(string gameName)
        {
            GameLogic.CreateGame(gameName);

            await Clients.All.SendAsync("GameAdded", GameLogic.ReturnGames());
        }

        public async Task JoingGame(string groupName, int playerType)
        {
            GameLogic.JoinGame(Context.ConnectionId, groupName, playerType);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.All.SendAsync("GameAdded", GameLogic.ReturnGames());
        }

        public async Task LeaveGame(string groupName, int playerType)
        {
            GameLogic.RemoveUserFromGame(Context.ConnectionId, groupName, playerType);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.All.SendAsync("GameAdded", GameLogic.ReturnGames());
        }

        public async Task GameOver(string group, PongScores pongGame)
        {
            await Clients.Group(group).SendAsync("GameOver", pongGame);
        }
    }
}
