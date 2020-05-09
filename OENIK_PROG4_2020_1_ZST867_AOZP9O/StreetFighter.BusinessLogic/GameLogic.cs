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
        /// <param name="a">First Player object.</param>
        /// <param name="b">Second Player object.</param>
        public void MoveLeft(Player a, Player b)
        {
            if ((!a.FacinLeft || !a.IsHit(b)) && a.Geometry.Bounds.Left > 8)
            {
                a.CX -= 40;
                this.RefreshScreen?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Moves Player right.
        /// </summary>
        /// <param name="a">First Player object.</param>
        /// <param name="b">Second Player object.</param>
        public void MoveRight(Player a, Player b)
        {
            if ((a.FacinLeft || !a.IsHit(b)) && a.Geometry.Bounds.Right < this.model.Width - 5)
            {
                a.CX += 40;
                this.RefreshScreen?.Invoke(this, EventArgs.Empty);
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

                // RefreshScreen?.Invoke(this, EventArgs.Empty);
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
                this.RefreshScreen?.Invoke(this, EventArgs.Empty);
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

                this.RefreshScreen?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Stamina regen.
        /// </summary>
        public void StaminaRegen()
        {
            if (this.model.Player1.Stamina < 100)
            {
                this.model.Player1.Stamina += 5;
            }

            if (this.model.Player2.Stamina < 100)
            {
                this.model.Player2.Stamina += 5;
            }
        }

        /// <summary>
        /// Jumpstick.
        /// </summary>
        public void JumpTick()
        {
            if (this.model.Player1.IsJumping)
            {
                this.model.Player1.CY -= this.model.Player1.DY;
                this.model.Player1.DY -= 1;
                if (this.model.Player1.Geometry.Bounds.Bottom == this.model.Height - 50)
                {
                    this.model.Player1.IsJumping = false;
                }
            }

            if (this.model.Player2.IsJumping)
            {
                this.model.Player2.CY -= this.model.Player2.DY;
                this.model.Player2.DY -= 1;
                if (this.model.Player2.Geometry.Bounds.Bottom == this.model.Height - 50)
                {
                    this.model.Player2.IsJumping = false;
                }
            }
        }

        /// <summary>
        /// Jumpstick.
        /// </summary>
        public void ShapeAndStaminaTick()
        {
            if (this.model.Player1.CX < this.model.Player2.CX)
            {
                this.model.Player1.FacinLeft = false;
                this.model.Player2.FacinLeft = true;
            }
            else if (this.model.Player1.CX > this.model.Player2.CX)
            {
                this.model.Player1.FacinLeft = true;
                this.model.Player2.FacinLeft = false;
            }

            if (this.model.Player1.Timer == 0)
            {
                this.model.Player1.Geometry = this.model.Player1.FacinLeft ? Player.FacingLeftBaseGeometry : Player.FacingRightBaseGeometry;
                this.model.Player1.State = PlayerStatus.IsStanding;
            }
            else
            {
                this.model.Player1.Timer--;
            }

            if (this.model.Player2.Timer == 0)
            {
                this.model.Player2.Geometry = this.model.Player2.FacinLeft ? Player.FacingLeftBaseGeometry : Player.FacingRightBaseGeometry;
                this.model.Player2.State = PlayerStatus.IsStanding;
            }
            else
            {
                this.model.Player2.Timer--;
            }

            this.StaminaRegen();
        }
    }
}
