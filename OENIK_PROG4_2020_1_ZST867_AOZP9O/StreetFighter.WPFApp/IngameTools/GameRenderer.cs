using StreetFighter.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace StreetFighter.WPFApp.IngameTools
{
    public class GameRenderer
    {
        GameModel model;
        Pen pen;
        GeometryDrawing background;

        public GameRenderer(GameModel model)
        {
            this.model = model;
            this.background = new GeometryDrawing(Brushes.Cyan, pen, new RectangleGeometry(new Rect(0, 0, model.width, model.height)));
            this.pen = new Pen(Brushes.Black, 2);
        }

        //TODO: dicitionary az imagekhez

        public void DrawThings(DrawingContext ctx)
        {
            DrawingGroup dg = new DrawingGroup();

            GeometryDrawing player1 = new GeometryDrawing(Brushes.Red, pen, this.model.Player1.Geometry);
            GeometryDrawing player2 = new GeometryDrawing(Brushes.DarkBlue, pen, this.model.Player2.Geometry);

            dg.Children.Add(this.background);
            dg.Children.Add(player1);
            dg.Children.Add(player2);

            ctx.DrawDrawing(dg);
        }
    }
}
