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

    /// <summary>
    /// Game logic.
    /// </summary>
    public class GameLogic : IGameLogic
    {
        public event EventHandler RefreshScreen;

        GameModel model;

        public GameLogic(GameModel model)
        {
            this.model = model;
        }

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
                RefreshScreen?.Invoke(this, EventArgs.Empty);
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
            if ((a.FacinLeft == true) && (a.CX > b.CX))
            {
                a.FacinLeft = false;
                b.FacinLeft = true;
                RefreshScreen?.Invoke(this, EventArgs.Empty);
            }

            if ((a.FacinLeft == false) && (a.CX < b.CX))
            {
                a.FacinLeft = true;
                b.FacinLeft = false;
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
            if (!b.Invulnerable)
            {
                b.Health -= 2;
            }

            a.Stamina -= 5;
            RefreshScreen?.Invoke(this, EventArgs.Empty);
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
        public void MoveLeft(Player a)
        {
            a.CX -= 30;
            RefreshScreen?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Moves Player right.
        /// </summary>
        /// <param name="a">Player object.</param>
        public void MoveRight(Player a)
        {
            a.CX += 30;
            RefreshScreen?.Invoke(this, EventArgs.Empty);
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
                a.DY = 20;
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
            if (!b.Invulnerable)
            {
                b.Health -= 1;
            }

            a.Stamina -= 3;
            RefreshScreen?.Invoke(this, EventArgs.Empty);
        }

        public void JumpTick()
        {
            if (model.Player1.IsJumping)
            {
                model.Player1.CY -= model.Player1.DY;
                model.Player1.DY -= 1;
                if (model.Player1.Geometry.Bounds.Bottom == 450)
                {
                    model.Player1.IsJumping = false;
                }
            }

            if (model.Player2.IsJumping)
            {
                model.Player2.CY -= model.Player2.DY;
                model.Player2.DY -= 1;
                if (model.Player2.Geometry.Bounds.Bottom == 450)
                {
                    model.Player2.IsJumping = false;
                }
            }
        }
    }
}
