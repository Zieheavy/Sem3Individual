using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class GameLogic
    {
        private bool lastBool;
        private bool goingLeft = false;

        static List<PongGame> activeGames = new List<PongGame>();

        public void CreateGame(string _gameName)
        {
            //creates a new game
            activeGames.Add(new PongGame()
            {
                GameStarted = false,
                GameName = _gameName
            });
        }

        public PongGame SetPlayerPosition(string _gameName, int _p1Pos, int _p2Pos)
        {
            //find the game based on the name of the game
            PongGame selectedGame = activeGames.Find(g => g.GameName == _gameName);

            if (selectedGame != null)
            {
                if (_p1Pos > 0)
                    selectedGame.P1Pos = _p1Pos;

                if (_p2Pos > 0)
                    selectedGame.P2Pos = _p2Pos;
            }
            else
            {
                selectedGame = new PongGame();
            }

            return selectedGame;
        }

        public void JoinGame(string _playerId, string _gameName, int _playerType)
        {
            PongGame selectedGame = activeGames.Find(g => g.GameName == _gameName);

            //checks if the p
            if (_playerType == 1)
                selectedGame.P1Id = _playerId;
            else
                selectedGame.P2Id = _playerId;
            
        }

        public PongGame ReturnGame(string _gameName)
        {
            return activeGames.Find(g => g.GameName == _gameName);
        }

        public List<PongGame> ReturnGames()
        {
            return activeGames;
        }

        public PongGame calculateBallPos(PongGameDB pongGame)
        {
            PongGame balPos = new PongGame();

            return balPos;
        }
    }
}
