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

        public void calculateBallPos(PongGame pongGame)
        {
            //get stored info from database

            if(lastBool != pongGame.GameStarted)
            {
                if(pongGame.GameStarted == true)
                {
                    //game has started
                }

                lastBool = pongGame.GameStarted;
            }

            if (pongGame.GameStarted)
            {
                if (!goingLeft)
                {
                    pongGame.BalX++;
                }
                else
                {
                    pongGame.BalX--;
                }

                if(pongGame.BalX > 180 && !goingLeft)
                {
                    goingLeft = true;
                }else if(pongGame.BalX < 20 && goingLeft)
                {
                    goingLeft = false;
                }
            }
        }
    }
}
