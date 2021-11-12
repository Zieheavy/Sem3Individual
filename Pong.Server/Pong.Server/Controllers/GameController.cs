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
using Logic;

namespace Pong.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        GameLogic gl = new GameLogic();
        private readonly ILogger<GameController> _logger;

        public GameController(ILogger<GameController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<PongGame> GetGames()
        {
            return gl.ReturnGames();
        }

        [HttpPost]
        public void CreateGame(string gameId)
        {
            gl.CreateGame(gameId);
        }
    }
}
