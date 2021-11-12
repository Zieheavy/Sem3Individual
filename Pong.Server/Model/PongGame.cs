using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
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
        public bool BalDirection { get; set; }
    }
}
