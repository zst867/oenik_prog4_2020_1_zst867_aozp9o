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

        public readonly int width = 800;
        public readonly int height = 450;

        public GameModel()
        {
            Player1 = new Player("name1", 100, 350, true);
            Player2 = new Player("name2", 200, 350, false);
        }
    }
}
