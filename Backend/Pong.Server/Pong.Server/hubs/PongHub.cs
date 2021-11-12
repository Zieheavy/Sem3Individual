using System.Threading.Tasks;
using Model;
using Microsoft.AspNetCore.SignalR;
using Logic;
using DAL;
using System.Collections;
using System.Collections.Generic;

namespace Pong.Server.hubs
{
    public class PongHub : Hub
    {
        GameLogic gl = new GameLogic();

        bool gameLoopStarted = false;

        public class PlayerPositions
        {
            public string GameName { get; set; }
            public int Position { get; set; }
            public int PlayerType { get; set; }
        }

        public async Task SendPlayerPosition(PlayerPositions _position)
        {
            PongGame pongGame;
            if (_position.PlayerType == 1)
                pongGame = gl.SetPlayerPosition(_position.GameName, _position.Position, -1);
            else
                pongGame = gl.SetPlayerPosition(_position.GameName, -1, _position.Position);
            //gd.UpdatePlayerPosition(pongGame);
            //;
            //await Clients.Group(pongGame.GroupName).SendAsync("ReceiveBallPosition", gl.calculateBallPos(gd.GetGame(pongGame)));
            await Clients.Group(_position.GameName).SendAsync("ReceivePlayerPosition", pongGame);
        }

        public async Task CalculateBallPos(string gameName)
        {
            PongGame pongGame = gl.ReturnGame(gameName);
            await Clients.Group(gameName).SendAsync("ReceiveBallPosition", pongGame);
        }

        public async Task JoingGame(string groupName, int playerType)
        {
            gl.JoinGame(Context.ConnectionId, groupName, playerType);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task LeaveGame(string groupName, int playerType)
        {
            //gd.RemoveUserFromGame(Context.ConnectionId, groupName, playerType);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task GameOver(string group, PongGameDB pongGame)
        {
            await Clients.Group(group).SendAsync("GameOver", pongGame);
        }
    }
}
