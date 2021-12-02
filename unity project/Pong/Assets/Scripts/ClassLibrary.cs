using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassLibrary : MonoBehaviour
{
    public class PongGame
    {
        //Game Variables
        public bool GameStarted { get; set; }
        public string GameName { get; set; }

        //Player Variables
        public string P1Id { get; set; }
        public string P2Id { get; set; }
        public int P1Pos { get; set; }
        public int P2Pos { get; set; }

        //Ball variables
        public int BalX { get; set; }
        public int BalY { get; set; }
        public int BalXDir { get; set; }
        public int BalYDir { get; set; }
    }

    public class PlayerPositions
    {
        public string GameName { get; set; }
        public int Position { get; set; }
        public int PlayerType { get; set; }
    }
}
