﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PongGameDB
    {
        public int Id { get; set; }
        public string GameId { get; set; }
        public string Player1Id { get; set; }
        public string Player2Id { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
