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
        Pen blackPen = new Pen(Brushes.Black, 2);
        Pen redPen = new Pen(Brushes.Red, 2);
        Pen bluePen = new Pen(Brushes.DarkBlue, 2);
        Pen yellowPen = new Pen(Brushes.Yellow, 2);
        Point player1HealthLocation = new Point(20, 20);
        Point player2HealthLocation = new Point(240, 20);
        Point player1StaminaLocation = new Point(20, 40);
        Point player2StaminaLocation = new Point(240, 40);
        Typeface font = new Typeface("Arial");

        GeometryDrawing background;

        public GameRenderer(GameModel model)
        {
            this.model = model;
            this.background = new GeometryDrawing(Brushes.Cyan, blackPen, new RectangleGeometry(new Rect(0, 0, model.width, model.height)));
        }

        //TODO: dicitionary az imagekhez

        public void DrawThings(DrawingContext ctx)
        {
            DrawingGroup dg = new DrawingGroup();

            GeometryDrawing player1 = new GeometryDrawing(Brushes.Red, blackPen, this.model.Player1.Geometry);
            GeometryDrawing player2 = new GeometryDrawing(Brushes.DarkBlue, blackPen, this.model.Player2.Geometry);

            dg.Children.Add(this.background);
            dg.Children.Add(player1);
            dg.Children.Add(player2);
            DrawHealthAndStamina(ref dg);
            ctx.DrawDrawing(dg);
        }

        private void DrawHealthAndStamina(ref DrawingGroup dg)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            FormattedText player1HealthText = new FormattedText(this.model.Player1.Health.ToString()+"/10",
               System.Globalization.CultureInfo.CurrentCulture,
               FlowDirection.LeftToRight,
               this.font,
               16,
               Brushes.Black);
            GeometryDrawing player1HealthGD = new GeometryDrawing(null, redPen, player1HealthText.BuildGeometry(player1HealthLocation));
            dg.Children.Add(player1HealthGD);

            FormattedText player2HealthText = new FormattedText(this.model.Player2.Health.ToString() + "/10",
             System.Globalization.CultureInfo.CurrentCulture,
             FlowDirection.LeftToRight,
             this.font,
             16,
             Brushes.Black);
            GeometryDrawing player2HealthGD = new GeometryDrawing(null, bluePen, player2HealthText.BuildGeometry(player2HealthLocation));
            dg.Children.Add(player2HealthGD);

            FormattedText player1StaminaText = new FormattedText(this.model.Player1.Stamina.ToString() + "/10",
               System.Globalization.CultureInfo.CurrentCulture,
               FlowDirection.LeftToRight,
               this.font,
               16,
               Brushes.Black);
            GeometryDrawing player1StaminaGD = new GeometryDrawing(null, yellowPen, player1StaminaText.BuildGeometry(player1StaminaLocation));
            dg.Children.Add(player1StaminaGD);

            FormattedText player2StaminaText = new FormattedText(this.model.Player2.Stamina.ToString() + "/10",
              System.Globalization.CultureInfo.CurrentCulture,
              FlowDirection.LeftToRight,
              this.font,
              16,
              Brushes.Black);
            GeometryDrawing player2StaminaGD = new GeometryDrawing(null, yellowPen, player2StaminaText.BuildGeometry(player2StaminaLocation));
            dg.Children.Add(player2StaminaGD);

#pragma warning restore CS0618 // Type or member is obsolete
        }
    }
}
