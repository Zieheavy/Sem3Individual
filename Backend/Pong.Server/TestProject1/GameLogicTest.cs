using System;
using System.Collections.Generic;
using Logic;
using Model;
using Xunit;

namespace TestProject1
{
    public class GameLogicTest
    {
        [Theory]
        [MemberData(nameof(SetPlayerPostion))]
        public void TestSetPlayerPositon(string gameName, int p1Pos, int p2Pos, PongGame expected)
        {
            GameLogic gameLogic = new GameLogic();
            gameLogic.CreateGame(gameName);

            PongGame result = gameLogic.SetPlayerPosition(gameName, p1Pos, p2Pos);

            Assert.Equal(expected.GameName, result.GameName);
            Assert.Equal(expected.P1Pos, result.P1Pos);
            Assert.Equal(expected.P2Pos, result.P2Pos);
        }

        [Theory]
        [MemberData(nameof(CalculateBallPosition))]
        public void TestCalculateBallPosition(PongGame pongGame, PongGame expected)
        {
            GameLogic gameLogic = new GameLogic();

            PongGame result = gameLogic.calculateBallPos(pongGame);

            Assert.Equal(expected.GameStarted, result.GameStarted);
            Assert.Equal(expected.GameName, result.GameName);
            Assert.Equal(expected.BalX, result.BalX);
            Assert.Equal(expected.BalY, result.BalY);
            Assert.Equal(expected.BalXDir, result.BalXDir);
            Assert.Equal(expected.BalYDir, result.BalYDir);
        }

        public static IEnumerable<object[]> SetPlayerPostion()
        {
            return new List<object[]>
            {
                new object[]
                {
                    "test",
                    50,
                    0,
                    new PongGame
                    {
                        GameName = "test",
                        P1Pos = 50,
                        P2Pos = 0,
                    },
                },
                new object[]
                {
                    "test",
                    0,
                    50,
                    new PongGame
                    {
                        GameName = "test",
                        P1Pos = 50,
                        P2Pos = 50,
                    },
                },
                new object[]
                {
                    "test",
                    0,
                    75,
                    new PongGame
                    {
                        GameName = "test",
                        P1Pos = 50,
                        P2Pos = 75,
                    },
                }
            };
        }

        public static IEnumerable<object[]> CalculateBallPosition()
        {
            return new List<object[]>
            {
                //bal change position test
                new object[]
                {
                    new PongGame
                    {
                        GameStarted = true,
                        GameName = "test",
                        P1Id = "axi109n1230",
                        P2Id = "axi109n1230",
                        P1Pos = 40,
                        P2Pos = 40,
                        BalX = 40,
                        BalY = 40,
                        BalXDir = 1,
                        BalYDir = 1,
                    },
                    new PongGame
                    {
                        GameStarted = true,
                        GameName = "test",
                        P1Id = "axi109n1230",
                        P2Id = "axi109n1230",
                        P1Pos = 40,
                        P2Pos = 40,
                        BalX = 30,
                        BalY = 35,
                        BalXDir = 1,
                        BalYDir = 1,
                    }
                },
                //ball hit player 2 test
                new object[]
                {
                    new PongGame
                    {
                        GameStarted = true,
                        GameName = "test",
                        P1Id = "axi109n1230",
                        P2Id = "axi109n1230",
                        P1Pos = 40,
                        P2Pos = 40,
                        BalX = 485,
                        BalY = 40,
                        BalXDir = 0,
                        BalYDir = 0,
                    },
                    new PongGame
                    {
                        GameStarted = true,
                        GameName = "test",
                        P1Id = "axi109n1230",
                        P2Id = "axi109n1230",
                        P1Pos = 40,
                        P2Pos = 40,
                        BalX = 475,
                        BalY = 45,
                        BalXDir = 1,
                        BalYDir = 0,
                    },
                },
                //ball hit player 1 test
                new object[]
                {
                    new PongGame
                    {
                        GameStarted = true,
                        GameName = "test",
                        P1Id = "axi109n1230",
                        P2Id = "axi109n1230",
                        P1Pos = 40,
                        P2Pos = 40,
                        BalX = 20,
                        BalY = 40,
                        BalXDir = 1,
                        BalYDir = 0,
                    },
                    new PongGame
                    {
                        GameStarted = true,
                        GameName = "test",
                        P1Id = "axi109n1230",
                        P2Id = "axi109n1230",
                        P1Pos = 40,
                        P2Pos = 40,
                        BalX = 25,
                        BalY = 45,
                        BalXDir = 0,
                        BalYDir = 0,
                    },
                }
            };
        }
    }
}
