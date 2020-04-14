using StreetFighter.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace StreetFighter.WPFApp.IngameTools
{
    public class GameControl : FrameworkElement
    {
        GameLogic logic;
        GameRenderer renderer;
        GameModel model;
        DispatcherTimer jumpTimer;
        DispatcherTimer playerShapeTimer;

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
                this.jumpTimer = new DispatcherTimer();
                this.jumpTimer.Interval = TimeSpan.FromMilliseconds(25);
                this.jumpTimer.Tick += this.jumpTick;
                this.playerShapeTimer = new DispatcherTimer();
                this.playerShapeTimer.Interval = TimeSpan.FromMilliseconds(100);
                this.playerShapeTimer.Tick += this.shapeTick;
                win.KeyDown += this.Win_KeyDown;
                this.jumpTimer.Start();
                this.playerShapeTimer.Start();
            }

            logic.RefreshScreen += (obj, args) => InvalidateVisual();
            InvalidateVisual();
        }

        private void Win_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W: logic.MoveUp(model.Player1, model.Player2); break;
                case Key.A: logic.MoveLeft(model.Player1); break;
                case Key.D: logic.MoveRight(model.Player1); break;
                case Key.Q: logic.Kick(model.Player1, model.Player2); break;
                case Key.E: logic.Slap(model.Player1, model.Player2); break;
                case Key.Space: logic.Block(model.Player1, 3); break;
                case Key.Up: logic.MoveUp(model.Player1, model.Player2); break;
                case Key.Left: logic.MoveLeft(model.Player2); break; break;
                case Key.Right: logic.MoveRight(model.Player2); break; break;
                case Key.I: logic.Kick(model.Player2, model.Player1); break;
                case Key.O: logic.Slap(model.Player2, model.Player1); break;
                case Key.P: logic.Block(model.Player2, 3); break;
                case Key.Escape: break;
            }
        }

        private void shapeTick(object sender, EventArgs e)
        {
            //TODO
        }

        private void jumpTick(object sender, EventArgs e)
        {
            //TODO
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
