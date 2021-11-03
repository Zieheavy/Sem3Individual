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
        GameDAL gd = new GameDAL();

        bool gameLoopStarted = false;

        public async Task SendMessage(PongGame pongGame)
        {
            gl.calculateBallPos(pongGame);

            await Clients.All.SendAsync("ReceiveMessage", pongGame);
        }

        public async Task StartGame()
        {
            if (!gameLoopStarted)
            {
                gameLoopStarted = true;
                await GameLoop();
            }
        }

        public async Task JoingGame(string groupName, int playerType)
        {
            gd.AddUserToGame(Context.ConnectionId, groupName, playerType);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task LeaveGame(string groupName, int playerType)
        {
            gd.RemoveUserFromGame(Context.ConnectionId, groupName, playerType);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task GameLoop()
        {
            //search for currently running games in database
            List<PongGame> pongGames = gd.GetGames();
            pongGames.Add(new PongGame());
            pongGames.Add(new PongGame());
            PongGame pg = new PongGame();
            pg.GroupName = "12";
            pongGames.Add(pg);
            await Task.Delay(100);

            foreach (PongGame pongGame in pongGames)
            {
                string group = pongGame.GroupName;
                if (group == null)
                {
                    group = "1";
                }
                await Clients.Group(group).SendAsync("ReceiveMessage", pongGame);
            }

            await GameLoop();
        }
    }
}
