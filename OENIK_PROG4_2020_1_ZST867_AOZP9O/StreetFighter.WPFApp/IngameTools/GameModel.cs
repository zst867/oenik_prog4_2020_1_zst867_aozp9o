using StreetFighter.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetFighter.WPFApp.IngameTools
{
    public class GameModel
    {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }

        public GameModel()
        {
            Player1 = new Player("name1", 12, 13, true);
            Player2 = new Player("name2", 14, 15, false);
        }
    }
}
