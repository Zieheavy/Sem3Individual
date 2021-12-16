using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Threading.Tasks;
using DAL;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Model;

namespace Pong.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly GameLogic _gl = new GameLogic();
        private readonly ILogger<GameController> _logger;

        public GameController(ILogger<GameController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<PongGame> GetGames()
        {
            return GameLogic.ReturnGames();
        }

        [HttpPost]
        public void CreateGame(string gameId)
        {
            GameLogic.CreateGame(gameId);
        }
    }
}
