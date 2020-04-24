using StreetFighter.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace StreetFighter.WPFApp.IngameTools
{
    public class GameRenderer
    {
        GameModel model;
        //Pen blackPen = new Pen(Brushes.Black, 2);
        Pen redPen = new Pen(Brushes.Red, 2);
        Pen bluePen = new Pen(Brushes.DarkBlue, 2);
        Pen yellowPen = new Pen(Brushes.Yellow, 2);
        Pen greenPen = new Pen(Brushes.GreenYellow, 2);
        Point player1HealthLocation = new Point(270, 20);
        Point player2HealthLocation = new Point(800, 20);
        Point player1StaminaLocation = new Point(270, 50);
        Point player2StaminaLocation = new Point(800, 50);
        Point player1ScoreLocation = new Point(270, 75);
        Point player2ScoreLocation = new Point(800, 75);
        Typeface font = new Typeface("Arial");

        GeometryDrawing background;
        Dictionary<string, ImageBrush> player1Brushes;
        Dictionary<string, ImageBrush> player2Brushes;

        private void FillTheDictionaries()
        {
            player1Brushes = new Dictionary<string, ImageBrush>();

            string[] redFileNames = Assembly.GetExecutingAssembly().GetManifestResourceNames().Where(x => x.Contains("Red")).ToArray();
            ;
            if (redFileNames.Count() == 0)
            {
                throw new Exception("shit");
            }
            ;
            for (int i = 0; i < redFileNames.Length; i++)
            {
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream(redFileNames[i]);
                bmp.EndInit();
                ImageBrush ib = new ImageBrush(bmp);
                player1Brushes.Add(redFileNames[i], ib);
            }

            player2Brushes = new Dictionary<string, ImageBrush>();
            string[] blueFileNames = Assembly.GetExecutingAssembly().GetManifestResourceNames().Where(x => x.Contains("Blue")).ToArray();
            for (int i = 0; i < blueFileNames.Length; i++)
            {
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream(blueFileNames[i]);
                bmp.EndInit();
                ImageBrush ib = new ImageBrush(bmp);
                player2Brushes.Add(blueFileNames[i], ib);
            }
        }

        public GameRenderer(GameModel model)
        {
            this.model = model;
            BitmapImage BG = new BitmapImage();
            string bgFileName = Assembly.GetExecutingAssembly().GetManifestResourceNames().Where(x => x.Contains("bigbg.jpg")).First();
            BG.BeginInit();
            BG.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream(bgFileName);
            BG.EndInit();
            ImageBrush ib = new ImageBrush(BG);
            Brush bgBrush = new ImageBrush(BG);
            this.background = new GeometryDrawing(bgBrush, null, new RectangleGeometry(new Rect(0, 0, model.Width, model.Height)));
            FillTheDictionaries();
            ;
        }

        

        private Brush GetPlayer1Brush()
        {
            string key = "StreetFighter.WPFApp.Images.Red_";
            if (model.Player1.FacinLeft)
            {
                key += "L_";
            }
            else
            {
                key += "R_";
            }
            if (model.Player1.State == PlayerStatus.IsStanding)
            {
                key += "Base";
            }
            else if (model.Player1.State == PlayerStatus.IsPunching)
            {
                key += "Punch";
            }
            else
            {
                key += "Kick";
            }
            key += ".png";
            return player1Brushes[key];
        }

        private Brush GetPlayer2Brush()
        {
            string key = "StreetFighter.WPFApp.Images.Blue_";
            if (model.Player2.FacinLeft)
            {
                key += "L_";
            }
            else
            {
                key += "R_";
            }
            if (model.Player2.State == PlayerStatus.IsStanding)
            {
                key += "Base";
            }
            else if (model.Player2.State == PlayerStatus.IsPunching)
            {
                key += "Punch";
            }
            else
            {
                key += "Kick";
            }
            key += ".png";
            return player2Brushes[key];
        }

        public void DrawThings(DrawingContext ctx)
        {
            DrawingGroup dg = new DrawingGroup();

            GeometryDrawing player1 = new GeometryDrawing(GetPlayer1Brush(), null, this.model.Player1.Geometry);
            GeometryDrawing player2 = new GeometryDrawing(GetPlayer2Brush(), null, this.model.Player2.Geometry);

            dg.Children.Add(this.background);
            dg.Children.Add(player1);
            dg.Children.Add(player2);
            DrawHealthAndStamina(ref dg);
            ctx.DrawDrawing(dg);
        }

        private void DrawHealthAndStamina(ref DrawingGroup dg)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            FormattedText player1HealthText = new FormattedText("Health: "+this.model.Player1.Health.ToString()+"/100",
               System.Globalization.CultureInfo.CurrentCulture,
               FlowDirection.LeftToRight,
               this.font,
               26,
               Brushes.Black);
            GeometryDrawing player1HealthGD = new GeometryDrawing(null, redPen, player1HealthText.BuildGeometry(player1HealthLocation));
            dg.Children.Add(player1HealthGD);

            FormattedText player2HealthText = new FormattedText("Health: "+this.model.Player2.Health.ToString() + "/100",
             System.Globalization.CultureInfo.CurrentCulture,
             FlowDirection.LeftToRight,
             this.font,
             26,
             Brushes.Black);
            GeometryDrawing player2HealthGD = new GeometryDrawing(null, bluePen, player2HealthText.BuildGeometry(player2HealthLocation));
            dg.Children.Add(player2HealthGD);

            FormattedText player1StaminaText = new FormattedText("Stamina: "+this.model.Player1.Stamina.ToString() + "/100",
               System.Globalization.CultureInfo.CurrentCulture,
               FlowDirection.LeftToRight,
               this.font,
               20,
               Brushes.Black);
            GeometryDrawing player1StaminaGD = new GeometryDrawing(null, yellowPen, player1StaminaText.BuildGeometry(player1StaminaLocation));
            dg.Children.Add(player1StaminaGD);

            FormattedText player2StaminaText = new FormattedText("Stamina: "+this.model.Player2.Stamina.ToString() + "/100",
              System.Globalization.CultureInfo.CurrentCulture,
              FlowDirection.LeftToRight,
              this.font,
              20,
              Brushes.Black);
            GeometryDrawing player2StaminaGD = new GeometryDrawing(null, yellowPen, player2StaminaText.BuildGeometry(player2StaminaLocation));
            dg.Children.Add(player2StaminaGD);

            FormattedText player1ScoreText = new FormattedText("Score: "+this.model.Player1.Score.ToString(),
               System.Globalization.CultureInfo.CurrentCulture,
               FlowDirection.LeftToRight,
               this.font,
               16,
               Brushes.Black);
            GeometryDrawing player1ScoreGD = new GeometryDrawing(null, greenPen, player1ScoreText.BuildGeometry(player1ScoreLocation));
            dg.Children.Add(player1ScoreGD);

            FormattedText player2ScoreText = new FormattedText("Score: "+this.model.Player2.Score.ToString(),
              System.Globalization.CultureInfo.CurrentCulture,
              FlowDirection.LeftToRight,
              this.font,
              16,
              Brushes.Black);
            GeometryDrawing player2ScoreGD = new GeometryDrawing(null, greenPen, player2ScoreText.BuildGeometry(player2ScoreLocation));
            dg.Children.Add(player2ScoreGD);

#pragma warning restore CS0618 // Type or member is obsolete
        }
    }
}
