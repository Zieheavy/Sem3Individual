using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PongScores
    {
        public int Id { get; set; }

        public string GameId { get; set; }

        public string Player1Id { get; set; }

        public string Player2Id { get; set; }

        public int Player1Position { get; set; }

        public int Player2Position { get; set; }

        public DateTime CreationDate { get; set; }

        public int BalDirection { get; set; }

        public double BalX { get; set; }

        public double BalY { get; set; }

        public DateTime LastUpdate { get; set; }
    }
}
