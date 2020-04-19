using StreetFighter.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using MessageBox = System.Windows.Forms.MessageBox;

namespace StreetFighter.WPFApp.IngameTools
{
    public class GameControl : FrameworkElement
    {
        GameLogic logic;
        GameRenderer renderer;
        GameModel model;
        DispatcherTimer jumpTimer;
        DispatcherTimer playerShapeAndStaminaTimer;

        public GameControl()
        {
            Loaded += GameControl_Loaded;
        }

        private void GameControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.model = new GameModel();
            this.logic = new GameLogic(this.model);
            this.renderer = new GameRenderer(this.model);

            Window win = Window.GetWindow(this);
            if (win != null)
            {
                this.jumpTimer = new DispatcherTimer();
                this.jumpTimer.Interval = TimeSpan.FromMilliseconds(25);
                this.jumpTimer.Tick += this.jumpTick;
                this.playerShapeAndStaminaTimer = new DispatcherTimer();
                this.playerShapeAndStaminaTimer.Interval = TimeSpan.FromMilliseconds(500);
                this.playerShapeAndStaminaTimer.Tick += this.shapeTick;
                win.KeyDown += this.Win_KeyDown;
                this.jumpTimer.Start();
                this.playerShapeAndStaminaTimer.Start();
            }

            logic.RefreshScreen += (obj, args) => InvalidateVisual();
            InvalidateVisual();
        }

        private void Win_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W: logic.MoveUp(model.Player1, model.Player2); break;
                case Key.A: logic.MoveLeft(model.Player1, model.Player2); break;
                case Key.D: logic.MoveRight(model.Player1, model.Player2, model); break;
                case Key.Q: 
                    {
                        logic.Kick(model.Player1, model.Player2);
                        if (model.Player1.Health <= 0)
                        {
                            MessageBox.Show(model.Player1.Name + " won");
                            break;
                        }

                        if (model.Player2.Health <= 0)
                        {
                            MessageBox.Show(model.Player2.Name + " won");
                            break;
                        }
                        break;
                    } 
                case Key.E:
                    {
                        logic.Slap(model.Player1, model.Player2);
                        if (model.Player1.Health <= 0)
                        {
                            MessageBox.Show(model.Player1.Name + " won");
                            break;
                        }

                        if (model.Player2.Health <= 0)
                        {
                            MessageBox.Show(model.Player2.Name + " won");
                            break;
                        }
                        break;
                    }
                case Key.Space: logic.Block(model.Player1, 3); break;
                case Key.Up: logic.MoveUp(model.Player2, model.Player1); break;
                case Key.Left: logic.MoveLeft(model.Player2, model.Player1); break; break;
                case Key.Right: logic.MoveRight(model.Player2, model.Player1, model); break; break;
                case Key.I: logic.Kick(model.Player2, model.Player1); break;
                case Key.O: logic.Slap(model.Player2, model.Player1); break;
                case Key.P: logic.Block(model.Player2, 3); break;
                case Key.Escape: {
                        Thread t = new Thread(() => {
                            SaveGameWindow pw = new SaveGameWindow();
                            SavedGame sg = new SavedGame();
                            pw.DataContext = sg;
                            if ((pw.ShowDialog() == true) && (sg.Name != string.Empty))
                            {
                                ILogicSaveGame l = new LogicSaveGame();
                                l.Write(sg.Name, model.Player1, model.Player2, "saved_games.txt");
                                MessageBox.Show("game saved");
                            }
                        });
                        t.SetApartmentState(ApartmentState.STA);
                        t.Start();
                        while (t.IsAlive)
                        {
                            Thread.Sleep(10);
                        }
                        break;
                }
            }
        }

        private void shapeTick(object sender, EventArgs e)
        {
            logic.ShapeAndStaminaTick();
            InvalidateVisual();
        }

        private void jumpTick(object sender, EventArgs e)
        {
            logic.JumpTick();
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
