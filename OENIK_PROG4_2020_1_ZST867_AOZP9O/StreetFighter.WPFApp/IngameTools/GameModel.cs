using StreetFighter.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetFighter.WPFApp.IngameTools
{
    class GameModel
    {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }

        public GameModel()
        {
            Player1 = new Player();
            Player2 = new Player();
        }
    }
}
