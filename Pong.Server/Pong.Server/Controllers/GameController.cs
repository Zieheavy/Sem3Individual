using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using DAL;
using Model;

namespace Pong.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private GameDAL gd = new GameDAL();
        private readonly ILogger<GameController> _logger;

        public GameController(ILogger<GameController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<PongGameDB> GetGames()
        {
            return gd.GetGames();
        }

        [HttpPost]
        public void CreateGame(string gameId)
        {
            gd.CreateGame(gameId);
        }
    }
}
