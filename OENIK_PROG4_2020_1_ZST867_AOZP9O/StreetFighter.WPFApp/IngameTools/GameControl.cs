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
    public class GameControl : FrameworkElement
    {
        GameLogic logic;
        GameRenderer renderer;
        GameModel model;

        public GameControl()
        {
            Loaded += GameControl_Loaded;
        }

        private void GameControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.logic = new GameLogic();
            this.model = new GameModel();
            this.renderer = new GameRenderer(this.model);
            

            Window win = Window.GetWindow(this);
            if (win != null)
            {
               //TODO: ticks to start...
            }

            InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (this.renderer != null)
            {
                this.renderer.DrawThings(drawingContext);
            }
        }
    }
}
