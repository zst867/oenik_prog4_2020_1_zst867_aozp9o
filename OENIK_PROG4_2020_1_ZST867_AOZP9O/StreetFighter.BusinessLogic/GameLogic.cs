// <copyright file="GameLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace StreetFighter.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Media;

    /// <summary>
    /// Game logic.
    /// </summary>
    public class GameLogic : IGameLogic
    {
        /// <summary>
        /// GameModel field.
        /// </summary>
        private GameModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameLogic"/> class.
        /// </summary>
        /// <param name="model">GameModel.</param>
        public GameLogic(GameModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameLogic"/> class.
        /// </summary>
        public GameLogic()
        {
        }

        /// <summary>
        /// RefreshScreen event.
        /// </summary>
        public event EventHandler RefreshScreen;

        /// <summary>
        /// Blocking logic.
        /// </summary>
        /// <param name="a">Player blocking.</param>
        /// <param name="counter">Monitors how many times Block method has been called, so it can increase staminacost.</param>
        public void Block(Player a, int counter)
        {
            int staminacost = 1;
            if (this.EnoughStamina(a, staminacost * 2 ^ counter))
            {
                a.Invulnerable = true;
                this.RefreshScreen?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Determines whether there is enough stamina for given action.
        /// </summary>
        /// <param name="a">Player needing the staminacheck.</param>
        /// <param name="staminacost">Stamina needed.</param>
        /// <returns>True if there is enough stamina.</returns>
        public bool EnoughStamina(Player a, int staminacost)
        {
            return a.Stamina > staminacost;
        }

        /// <summary>
        /// Jumping logic.
        /// </summary>
        /// <param name="a">Player in air.</param>
        /// <param name="b">Other Player.</param>
        public void JumpLogic(Player a, Player b)
        {
        }

        /// <summary>
        /// Moves Player down.
        /// </summary>
        /// <param name="a">Player object.</param>
        /// <param name="b">The other Player object for direction control.</param>
        public void MoveDown(Player a, Player b)
        {
            a.CY -= 1;
            this.JumpLogic(a, b);
        }

        /// <summary>
        /// Moves Player left.
        /// </summary>
        /// <param name="a">Player object.</param>
        public void MoveLeft(Player a, Player b)
        {
            if ((!a.FacinLeft || !a.IsHit(b)) && a.Geometry.Bounds.Left > 8)
            {
                a.CX -= 40;
                RefreshScreen?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Moves Player right.
        /// </summary>
        /// <param name="a">Player object.</param>
        public void MoveRight(Player a, Player b, GameModel  model2)
        {
            if ((a.FacinLeft || !a.IsHit(b)) && a.Geometry.Bounds.Right < model2.Width - 5)
            {
                a.CX += 40;
                RefreshScreen?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Moves Player up.
        /// </summary>
        /// <param name="a">Moving Player object.</param>
        /// <param name="b">The other Player object for direction control.</param>
        public void MoveUp(Player a, Player b)
        {
            if (!a.IsJumping)
            {
                a.DY = 25;
                a.IsJumping = true;
                this.JumpLogic(a, b);
                //RefreshScreen?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Slapping logic.
        /// </summary>
        /// <param name="a">Player attacking.</param>
        /// <param name="b">Player attacked.</param>
        public void Slap(Player a, Player b)
        {

            if (a.Stamina >= 30)
            {
                a.State = PlayerStatus.IsPunching;
                a.Timer = 1;
                if (a.FacinLeft)
                {
                    a.Geometry = Player.FacingLeftPunchGeometry;
                }
                else
                {
                    a.Geometry = Player.FacingRightPunchGeometry;
                }

                if (a.IsHit(b) && !b.Invulnerable)
                {
                    b.Health -= 10;
                    a.Score += a.Health * 9;
                }

                a.Stamina -= 30;
                RefreshScreen?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Kicking logic.
        /// </summary>
        /// <param name="a">Player attacking.</param>
        /// <param name="b">Player attacked.</param>
        public void Kick(Player a, Player b)
        {
            if (a.Stamina >= 50)
            {
                a.State = PlayerStatus.IsKicking;
                a.Timer = 1;
                if (a.FacinLeft)
                {
                    a.Geometry = Player.FacingLeftKickGeometry;
                }
                else
                {
                    a.Geometry = Player.FacingRightKickGeometry;
                }

                if (a.IsHit(b) && !b.Invulnerable)
                {
                    b.Health -= 20;
                    a.Score += a.Health * 10;
                }

                a.Stamina -= 50;

                RefreshScreen?.Invoke(this, EventArgs.Empty);
            }
        }

        public void StaminaRegen()
        {
            if (model.Player1.Stamina < 100)
            {
                model.Player1.Stamina+=5;
            }
            if (model.Player2.Stamina < 100)
            {
                model.Player2.Stamina+=5;
            }
        }

        public void JumpTick()
        {
            if (model.Player1.IsJumping)
            {
                model.Player1.CY -= model.Player1.DY;
                model.Player1.DY -= 1;
                if (model.Player1.Geometry.Bounds.Bottom == model.Height-50)
                {
                    model.Player1.IsJumping = false;
                }
            }

            if (model.Player2.IsJumping)
            {
                model.Player2.CY -= model.Player2.DY;
                model.Player2.DY -= 1;
                if (model.Player2.Geometry.Bounds.Bottom == model.Height-50)
                {
                    model.Player2.IsJumping = false;
                }
            }
        }

        public void ShapeAndStaminaTick()
        {
            if (model.Player1.CX < model.Player2.CX)
            {
                model.Player1.FacinLeft = false;
                model.Player2.FacinLeft = true;
            }

            else if (model.Player1.CX > model.Player2.CX)
            {
                model.Player1.FacinLeft = true;
                model.Player2.FacinLeft = false;
            }

            if (model.Player1.Timer == 0)
            {
                model.Player1.Geometry = model.Player1.FacinLeft ? Player.FacingLeftBaseGeometry : Player.FacingRightBaseGeometry;
                model.Player1.State = PlayerStatus.IsStanding;
            }
            else
            {
                model.Player1.Timer--;
            }

            if (model.Player2.Timer == 0)
            {
                model.Player2.Geometry = model.Player2.FacinLeft ? Player.FacingLeftBaseGeometry : Player.FacingRightBaseGeometry;
                model.Player2.State = PlayerStatus.IsStanding;
            }
            else
            {
                model.Player2.Timer--;
            }
            StaminaRegen();
        }
    }
}
