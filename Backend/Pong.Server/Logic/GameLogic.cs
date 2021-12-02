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
        private static int balYSpeed = 5; // the speed that the ball travels up and down
        private static int balXSpeed = 10; // the speed that the ball travels left and right
        private static int balSize = 12; // the size of the ball itself (ballRadius * 2)

        private static int playerSize = 45; //the height of the player (playerHeight)
        private static int playerOffset = 12; // the amount that the player is from the border of the canvas (playerwidth + margin)

        private static int canvasHeight = 250; // the height of the canvas 
        private static int canvasWidth = 500; // the width of the canvas

        static List<PongGame> activeGames = new List<PongGame>();

        public void CreateGame(string _gameName)
        {
            if (_gameName == "initialUpdateList")
                return;
            foreach (PongGame pg in activeGames)
            {
                if (pg.GameName == _gameName)
                    return;
            }
            //creates a new game
            activeGames.Add(new PongGame()
            {
                GameStarted = false,
                GameName = _gameName,
                BalX = canvasWidth/2 - balSize/2,
                BalY = canvasHeight/2 - balSize/2
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

        public PongGame calculateBallPos(PongGame pongGame)
        {
            bool gameOver = false;

            //checks if there are 2 players in the game (pauses game when 1 disconects)
            if (pongGame.P1Id != null && pongGame.P2Id != null)
            {
                //handles the y direction and top and bottom screen collision
                if(pongGame.BalYDir == 0)
                {
                    pongGame.BalY += balYSpeed;

                    //checks if the ball has passed the bottom of the screen
                    if(pongGame.BalY + balSize >= canvasHeight)
                    {
                        //changes the ball direction
                        pongGame.BalYDir = 1;
                        //sets the ball directly against the bottom border of the playfield
                        pongGame.BalY = canvasHeight - balSize;
                    }
                }
                else
                {
                    pongGame.BalY -= balYSpeed;

                    //checks if the ball has passed the top of the screen
                    if (pongGame.BalY <= 0)
                    {
                        //changes the ball direction
                        pongGame.BalYDir = 0;
                        //sets the ball directly against the bottom border of the playfield
                        pongGame.BalY = 0;
                    }
                }

                //handles the x direction checks player hit detetion can give gameover state
                if (pongGame.BalXDir == 0) //goign to player 2 (right player)
                {
                    pongGame.BalX += balXSpeed;

                    //verticaly inside the player
                    if (pongGame.BalY >= pongGame.P1Pos && pongGame.BalY - balSize <= pongGame.P1Pos + playerSize)
                    {
                        //hits the player
                        if (pongGame.BalX <= playerOffset) 
                        {
                            //sets the ball position directly against the player
                            pongGame.BalX = playerOffset;
                            //revers the ball direction
                            pongGame.BalXDir = 0;
                        }
                    }
                }
                else //going to player 1 (left player)
                {
                    pongGame.BalX -= balXSpeed;

                    //verticaly inside the player
                    if (pongGame.BalY >= pongGame.P1Pos && pongGame.BalY - balSize <= pongGame.P1Pos + playerSize)
                    {
                        //hits the player
                        if (pongGame.BalX+playerSize >= canvasWidth-playerOffset)
                        {
                            //sets the ball position directly against the player
                            pongGame.BalX = canvasWidth-playerOffset-playerSize;
                            //revers the ball direction
                            pongGame.BalXDir = 1;
                        }
                    }
                }

                //gameover alone should be enough the other two statements are insurance
                if (gameOver || pongGame.BalX < 0 || pongGame.BalX - balSize > canvasWidth)
                {
                    //game over

                    //these two if statements are temp
                    if (pongGame.BalX < 0)
                    {
                        pongGame.BalXDir = 0;
                    }
                    if(pongGame.BalX - balSize > canvasWidth)
                    {
                        pongGame.BalXDir = 1;
                    }
                }
            }

            return pongGame;
        }
    }
}
