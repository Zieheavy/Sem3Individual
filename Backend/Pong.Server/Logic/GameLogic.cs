﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace Logic
{
    public class GameLogic
    {
        private static readonly List<PongGame> ActiveGames = new List<PongGame>();

        // the speed that the ball travels up and down
        private static int balYSpeed = 5;

        // the speed that the ball travels left and right
        private static int balXSpeed = 10;

        // the size of the ball itself (ballRadius * 2)
        private static int balSize = 12;
        private static int halfBalSize = balSize / 2;

        // the height of the player (playerHeight)
        private static int playerSize = 60;

        // the amount that the player is from the border of the canvas (playerwidth + margin)
        private static int playerOffset = 15;

        // the height of the canvas
        private static int canvasHeight = 250;

        // the width of the canvas
        private static int canvasWidth = 500;

        private GameDAL _gd = new GameDAL();

        public void CreateGame(string gameName)
        {
            if (gameName == "initialUpdateList")
            {
                return;
            }
            foreach (PongGame pg in ActiveGames)
            {
                if (pg.GameName == gameName)
                {
                    return;
                }
            }

            // creates a new game
            ActiveGames.Add(new PongGame()
            {
                GameStarted = false,
                GameName = gameName,
                // BalX = canvasWidth/2 - balSize/2,
                BalX = (canvasWidth / 2) - halfBalSize,
                BalY = (canvasHeight / 2) - halfBalSize
            });

            _gd.CreateGame(gameName);
        }

        public PongGame SetPlayerPosition(string gameName, int p1Pos, int p2Pos)
        {
            // find the game based on the name of the game
            PongGame selectedGame = ActiveGames.Find(g => g.GameName == gameName);

            if (selectedGame != null)
            {
                if (p1Pos > 0)
                {
                    selectedGame.P1Pos = p1Pos;
                }

                if (p2Pos > 0)
                {
                    selectedGame.P2Pos = p2Pos;
                }
            }
            else
            {
                selectedGame = new PongGame();
            }

            return selectedGame;
        }

        public void JoinGame(string playerId, string gameName, int playerType)
        {
            PongGame selectedGame = ActiveGames.Find(g => g.GameName == gameName);

            if (playerType == 1)
            {
                selectedGame.P1Id = playerId;
            }
            else
            {
                selectedGame.P2Id = playerId;
            }
        }

        public PongGame ReturnGame(string gameName)
        {
            return ActiveGames.Find(g => g.GameName == gameName);
        }

        public List<PongGame> ReturnGames()
        {
            return ActiveGames;
        }

        public PongGame calculateBallPos(PongGame pongGame)
        {
            bool gameOver = false;

            // checks if there are 2 players in the game (pauses game when 1 disconects)
            if (pongGame.P1Id != null && pongGame.P2Id != null && !pongGame.GameOver)
            {
                // handles the y direction and top and bottom screen collision
                if (pongGame.BalYDir == 0)
                {
                    pongGame.BalY += balYSpeed;

                    // checks if the ball has passed the bottom of the screen
                    if (pongGame.BalY + halfBalSize >= canvasHeight)
                    {
                        // changes the ball direction
                        pongGame.BalYDir = 1;
                        // sets the ball directly against the bottom border of the playfield
                        pongGame.BalY = canvasHeight - halfBalSize;
                    }
                }
                else
                {
                    pongGame.BalY -= balYSpeed;

                    // checks if the ball has passed the top of the screen
                    if (pongGame.BalY <= halfBalSize)
                    {
                        // changes the ball direction
                        pongGame.BalYDir = 0;
                        // sets the ball directly against the bottom border of the playfield
                        pongGame.BalY = halfBalSize;
                    }
                }

                // handles the x direction checks player hit detetion can give gameover state
                // goign to player 2 (right player)
                if (pongGame.BalXDir == 0)
                {
                    pongGame.BalX += balXSpeed;

                    // verticaly inside the player
                    if (pongGame.BalY >= pongGame.P1Pos - (playerSize / 2) && pongGame.BalY <= pongGame.P1Pos + playerSize)
                    {
                        // hits the player
                        if (pongGame.BalX + halfBalSize >= canvasWidth - playerOffset)
                        {
                            // revers the ball direction
                            pongGame.BalXDir = 1;
                            pongGame.BalX = canvasWidth - playerOffset - balXSpeed;
                        }
                    }
                }

                // going to player 2 (right player)
                else
                {
                    pongGame.BalX -= balXSpeed;

                    // verticaly inside the player
                    if (pongGame.BalY >= pongGame.P2Pos - (playerSize / 2) && pongGame.BalY <= pongGame.P2Pos + playerSize)
                    {
                        // hits the player
                        if (pongGame.BalX - halfBalSize <= playerOffset)
                        {
                            // revers the ball direction
                            pongGame.BalX = playerOffset + balXSpeed;
                            pongGame.BalXDir = 0;
                        }
                    }
                }

                // gameover alone should be enough the other two statements are debugging
                if (gameOver || pongGame.BalX < 0 || pongGame.BalX - balSize > canvasWidth)
                {
                    // game over
                    pongGame.GameOver = true;

                    // these two if statements are temp
                    if (pongGame.BalX < -50)
                    {
                        pongGame.BalXDir = 0;
                        pongGame.p2Score++;
                    }
                    if (pongGame.BalX - balSize > canvasWidth + 50)
                    {
                        pongGame.BalXDir = 1;
                        pongGame.p1Score++;
                    }

                    // reset
                    pongGame.BalX = (canvasWidth / 2) - halfBalSize;
                    pongGame.BalY = (canvasHeight / 2) - halfBalSize;
                    Random rnd = new Random();
                    pongGame.BalXDir = rnd.Next(2, 0);
                    pongGame.BalYDir = rnd.Next(2, 0);

                    _gd.UpdateScore(pongGame.GameName, pongGame.p1Score, pongGame.p2Score);
                }
            }

            return pongGame;
        }
    }
}
