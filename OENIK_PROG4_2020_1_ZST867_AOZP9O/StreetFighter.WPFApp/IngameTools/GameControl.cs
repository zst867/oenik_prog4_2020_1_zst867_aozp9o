// <copyright file="GameControl.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace StreetFighter.WPFApp.IngameTools
{
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
    using StreetFighter.BusinessLogic;
    using StreetFighter.WPFApp.Viewmodel;
    using MessageBox = System.Windows.Forms.MessageBox;

    /// <summary>
    /// GameControl class.
    /// </summary>
    public class GameControl : FrameworkElement
    {
        private readonly GameModel model = MainMenuViewModel.Gm;
        private GameLogic logic;
        private GameRenderer renderer;
        private DispatcherTimer jumpTimer;
        private DispatcherTimer playerShapeAndStaminaTimer;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameControl"/> class.
        /// </summary>
        public GameControl()
        {
            this.Loaded += this.GameControl_Loaded;
        }

        /// <summary>
        /// Called on rendering.
        /// </summary>
        /// <param name="drawingContext">Drawing context.</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            if (this.renderer != null)
            {
                this.renderer.DrawThings(drawingContext);
            }
        }

        /// <summary>
        /// Initializes the components and subscribe on events.
        /// </summary>
        /// <param name="sender">Event sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void GameControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.logic = new GameLogic(this.model);
            this.renderer = new GameRenderer(this.model);

            Window win = Window.GetWindow(this);
            if (win != null)
            {
                this.jumpTimer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromMilliseconds(25),
                };
                this.jumpTimer.Tick += this.JumpTick;
                this.playerShapeAndStaminaTimer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromMilliseconds(200),
                };
                this.playerShapeAndStaminaTimer.Tick += this.ShapeTick;
                win.KeyDown += this.Win_KeyDown;
                this.jumpTimer.Start();
                this.playerShapeAndStaminaTimer.Start();
            }

            this.logic.RefreshScreen += (obj, args) => this.InvalidateVisual();
            this.InvalidateVisual();
        }

        private void Win_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W: this.logic.MoveUp(this.model.Player1, this.model.Player2); break;
                case Key.A: this.logic.MoveLeft(this.model.Player1, this.model.Player2); break;
                case Key.D: this.logic.MoveRight(this.model.Player1, this.model.Player2); break;
                case Key.Q:
                    {
                        this.logic.Kick(this.model.Player1, this.model.Player2);
                        if (this.model.Player2.Health <= 0)
                        {
                            ILogicSaveGame l = new LogicSaveGame();
                            l.Write("autosave", this.model.Player1, this.model.Player2, "saved_games.txt");
                            MessageBox.Show(this.model.Player1.Name + " won");
                            Window.GetWindow(this).Close();
                            break;
                        }

                        break;
                    }

                case Key.E:
                    {
                        this.logic.Slap(this.model.Player1, this.model.Player2);
                        if (this.model.Player2.Health <= 0)
                        {
                            ILogicSaveGame l = new LogicSaveGame();
                            l.Write("autosave", this.model.Player1, this.model.Player2, "saved_games.txt");
                            MessageBox.Show(this.model.Player1.Name + " won");
                            Window.GetWindow(this).Close();
                            break;
                        }

                        break;
                    }

                case Key.Up: this.logic.MoveUp(this.model.Player2, this.model.Player1); break;
                case Key.Left: this.logic.MoveLeft(this.model.Player2, this.model.Player1); break;
                case Key.Right: this.logic.MoveRight(this.model.Player2, this.model.Player1); break;
                case Key.I:
                    {
                        this.logic.Kick(this.model.Player2, this.model.Player1);
                        if (this.model.Player1.Health <= 0)
                        {
                            ILogicSaveGame l = new LogicSaveGame();
                            l.Write("autosave", this.model.Player1, this.model.Player2, "saved_games.txt");
                            MessageBox.Show(this.model.Player2.Name + " won");
                            Window.GetWindow(this).Close();
                            break;
                        }

                        break;
                    }

                case Key.O:
                    {
                        this.logic.Slap(this.model.Player2, this.model.Player1);

                        if (this.model.Player1.Health <= 0)
                        {
                            ILogicSaveGame l = new LogicSaveGame();
                            l.Write("autosave", this.model.Player1, this.model.Player2, "saved_games.txt");
                            MessageBox.Show(this.model.Player2.Name + " won");
                            Window.GetWindow(this).Close();
                            break;
                        }

                        break;
                    }

                case Key.Escape:
                    {
                        Thread t = new Thread(() =>
                        {
                            SaveGameWindow pw = new SaveGameWindow();
                            SavedGame sg = new SavedGame();
                            pw.DataContext = sg;
                            if ((pw.ShowDialog() == true) && (sg.Name != string.Empty))
                            {
                                ILogicSaveGame l = new LogicSaveGame();
                                l.Write(sg.Name, this.model.Player1, this.model.Player2, "saved_games.txt");
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

        /// <summary>
        /// Called on a Shapetimer tick.
        /// </summary>
        /// <param name="sender">Sender objec.</param>
        /// <param name="e">Event argument.</param>
        private void ShapeTick(object sender, EventArgs e)
        {
            this.logic.ShapeAndStaminaTick();
            this.InvalidateVisual();
        }

        /// <summary>
        /// Called on a Jumptimer tick.
        /// </summary>
        /// <param name="sender">Sender objec.</param>
        /// <param name="e">Event argument.</param>
        private void JumpTick(object sender, EventArgs e)
        {
            this.logic.JumpTick();
            this.InvalidateVisual();
        }
    }
}
