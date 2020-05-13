// <copyright file="GameRenderer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace StreetFighter.WPFApp.IngameTools
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using StreetFighter.BusinessLogic;

    /// <summary>
    /// GameRenderer class.
    /// </summary>
    public class GameRenderer
    {
        private readonly GameModel model;

        // Pen blackPen = new Pen(Brushes.Black, 2);
        private readonly GeometryDrawing background;
        private readonly Typeface font = new Typeface("Arial");
        private readonly Pen redPen = new Pen(Brushes.Red, 2);
        private readonly Pen bluePen = new Pen(Brushes.DarkBlue, 2);
        private readonly Pen yellowPen = new Pen(Brushes.Yellow, 2);
        private readonly Pen greenPen = new Pen(Brushes.GreenYellow, 2);
        private Point player1HealthLocation = new Point(270, 20);
        private Point player2HealthLocation = new Point(800, 20);
        private Point player1StaminaLocation = new Point(270, 50);
        private Point player2StaminaLocation = new Point(800, 50);
        private Point player1ScoreLocation = new Point(270, 75);
        private Point player2ScoreLocation = new Point(800, 75);

        private Dictionary<string, ImageBrush> player1Brushes;
        private Dictionary<string, ImageBrush> player2Brushes;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameRenderer"/> class.
        /// </summary>
        /// <param name="model">GameModel.</param>
        public GameRenderer(GameModel model)
        {
            this.model = model;
            BitmapImage bG = new BitmapImage();
            string bgFileName = Assembly.GetExecutingAssembly().GetManifestResourceNames().Where(x => x.Contains("bigbg.jpg")).First();
            bG.BeginInit();
            bG.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream(bgFileName);
            bG.EndInit();
            ImageBrush ib = new ImageBrush(bG);
            Brush bgBrush = new ImageBrush(bG);
            this.background = new GeometryDrawing(bgBrush, null, new RectangleGeometry(new Rect(0, 0, model.Width, model.Height)));
            this.FillTheDictionaries();
        }

        /// <summary>
        /// Creates and draw geometry drawings.
        /// </summary>
        /// <param name="ctx">Drawing context.</param>
        public void DrawThings(DrawingContext ctx)
        {
            DrawingGroup dg = new DrawingGroup();

            GeometryDrawing player1 = new GeometryDrawing(this.GetPlayer1Brush(), null, this.model.Player1.Geometry);
            GeometryDrawing player2 = new GeometryDrawing(this.GetPlayer2Brush(), null, this.model.Player2.Geometry);

            dg.Children.Add(this.background);
            dg.Children.Add(player1);
            dg.Children.Add(player2);
            this.DrawHealthAndStamina(ref dg);
            ctx.DrawDrawing(dg);
        }

        /// <summary>
        /// Fills the dictionaries whith image brushes.
        /// </summary>
        private void FillTheDictionaries()
        {
            this.player1Brushes = new Dictionary<string, ImageBrush>();

            string[] redFileNames = Assembly.GetExecutingAssembly().GetManifestResourceNames().Where(x => x.Contains("Red")).ToArray();
            if (redFileNames.Count() == 0)
            {
                throw new Exception("shit");
            }

            for (int i = 0; i < redFileNames.Length; i++)
            {
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream(redFileNames[i]);
                bmp.EndInit();
                ImageBrush ib = new ImageBrush(bmp);
                this.player1Brushes.Add(redFileNames[i], ib);
            }

            this.player2Brushes = new Dictionary<string, ImageBrush>();
            string[] blueFileNames = Assembly.GetExecutingAssembly().GetManifestResourceNames().Where(x => x.Contains("Blue")).ToArray();
            for (int i = 0; i < blueFileNames.Length; i++)
            {
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream(blueFileNames[i]);
                bmp.EndInit();
                ImageBrush ib = new ImageBrush(bmp);
                this.player2Brushes.Add(blueFileNames[i], ib);
            }
        }

        /// <summary>
        /// Gets Player1's actual brush.
        /// </summary>
        /// <returns>The actual brush.</returns>
        private Brush GetPlayer1Brush()
        {
            string key = "StreetFighter.WPFApp.Images.Red_";
            if (this.model.Player1.FacinLeft)
            {
                key += "L_";
            }
            else
            {
                key += "R_";
            }

            if (this.model.Player1.State == PlayerStatus.IsStanding)
            {
                key += "Base";
            }
            else if (this.model.Player1.State == PlayerStatus.IsPunching)
            {
                key += "Punch";
            }
            else
            {
                key += "Kick";
            }

            key += ".png";
            return this.player1Brushes[key];
        }

        private Brush GetPlayer2Brush()
        {
            string key = "StreetFighter.WPFApp.Images.Blue_";
            if (this.model.Player2.FacinLeft)
            {
                key += "L_";
            }
            else
            {
                key += "R_";
            }

            if (this.model.Player2.State == PlayerStatus.IsStanding)
            {
                key += "Base";
            }
            else if (this.model.Player2.State == PlayerStatus.IsPunching)
            {
                key += "Punch";
            }
            else
            {
                key += "Kick";
            }

            key += ".png";
            return this.player2Brushes[key];
        }

        private void DrawHealthAndStamina(ref DrawingGroup dg)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            FormattedText player1HealthText = new FormattedText(
                "Health: " + this.model.Player1.Health.ToString() + "/100",
                System.Globalization.CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                this.font,
                26,
                Brushes.Black);
            GeometryDrawing player1HealthGD = new GeometryDrawing(null, this.redPen, player1HealthText.BuildGeometry(this.player1HealthLocation));
            dg.Children.Add(player1HealthGD);

            FormattedText player2HealthText = new FormattedText(
                "Health: " + this.model.Player2.Health.ToString() + "/100",
                System.Globalization.CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                this.font,
                26,
                Brushes.Black);
            GeometryDrawing player2HealthGD = new GeometryDrawing(null, this.bluePen, player2HealthText.BuildGeometry(this.player2HealthLocation));
            dg.Children.Add(player2HealthGD);

            FormattedText player1StaminaText = new FormattedText(
                "Stamina: " + this.model.Player1.Stamina.ToString() + "/100",
                System.Globalization.CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                this.font,
                20,
                Brushes.Black);
            GeometryDrawing player1StaminaGD = new GeometryDrawing(null, this.yellowPen, player1StaminaText.BuildGeometry(this.player1StaminaLocation));
            dg.Children.Add(player1StaminaGD);

            FormattedText player2StaminaText = new FormattedText(
                "Stamina: " + this.model.Player2.Stamina.ToString() + "/100",
                System.Globalization.CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                this.font,
                20,
                Brushes.Black);
            GeometryDrawing player2StaminaGD = new GeometryDrawing(null, this.yellowPen, player2StaminaText.BuildGeometry(this.player2StaminaLocation));
            dg.Children.Add(player2StaminaGD);

            FormattedText player1ScoreText = new FormattedText(
                "Score: " + this.model.Player1.Score.ToString(),
                System.Globalization.CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                this.font,
                16,
                Brushes.Black);
            GeometryDrawing player1ScoreGD = new GeometryDrawing(null, this.greenPen, player1ScoreText.BuildGeometry(this.player1ScoreLocation));
            dg.Children.Add(player1ScoreGD);

            FormattedText player2ScoreText = new FormattedText(
                "Score: " + this.model.Player2.Score.ToString(),
                System.Globalization.CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                this.font,
                16,
                Brushes.Black);
            GeometryDrawing player2ScoreGD = new GeometryDrawing(null, this.greenPen, player2ScoreText.BuildGeometry(this.player2ScoreLocation));
            dg.Children.Add(player2ScoreGD);

#pragma warning restore CS0618 // Type or member is obsolete
        }
    }
}
