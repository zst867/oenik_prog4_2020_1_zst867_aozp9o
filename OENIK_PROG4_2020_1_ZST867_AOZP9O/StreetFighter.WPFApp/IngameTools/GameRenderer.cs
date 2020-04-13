using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace StreetFighter.WPFApp.IngameTools
{
    class GameRenderer
    {
        GameModel model;
        public GameRenderer(GameModel model)
        {
            this.model = model;
        }

        //TODO: dicitionary az imagekhez

        public void DrawThings(DrawingContext ctx)
        {
            DrawingGroup dg = new DrawingGroup();
            
            //TODO
        }
    }
}
