using StreetFighter.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetFighter.BusinessLogic
{
    public class GameModel
    {
        public Player Player1 { get; set; }

        public Player Player2 { get; set; }

        public readonly int width = 1200;
        public readonly int height = 650;

        public GameModel()
        {
            Player1 = new Player("name1", 100, 375, false);
            Player2 = new Player("name2", 1100, 375, true);
        }
    }
}
