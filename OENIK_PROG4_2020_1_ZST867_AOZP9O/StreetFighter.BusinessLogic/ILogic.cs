// <copyright file="ILogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace StreetFighter.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for logic.
    /// </summary>
    public interface ILogic
    {
        /// <summary>
        /// Moves Player left.
        /// </summary>
        /// <param name="a">Player object.</param>
        void MoveLeft(Player a);

        /// <summary>
        /// Moves Player right.
        /// </summary>
        /// <param name="a">Player object.</param>
        void MoveRight(Player a);

        /// <summary>
        /// Moves Player up.
        /// </summary>
        /// <param name="a">Moving Player object.</param>
        /// <param name="b">The other Player object for direction control.</param>
        void MoveUp(Player a, Player b);

        /// <summary>
        /// Moves Player down.
        /// </summary>
        /// <param name="a">Player object.</param>
        /// <param name="b">The other Player object for direction control.</param>
        void MoveDown(Player a, Player b);

        /// <summary>
        /// Slapping logic.
        /// </summary>
        /// <param name="a">Player attacking.</param>
        /// <param name="b">Player attacked.</param>
        void Slap(Player a, Player b);

        /// <summary>
        /// Kicking logic.
        /// </summary>
        /// <param name="a">Player attacking.</param>
        /// <param name="b">Player attacked.</param>
        void Kick(Player a, Player b);

        /// <summary>
        /// Blocking logic.
        /// </summary>
        /// <param name="a">Player blocking.</param>
        void Block(Player a);

        /// <summary>
        /// Determines whether there is collusion between the two Players.
        /// </summary>
        /// <returns>True if there is collution.</returns>
        bool Collusion();

        /// <summary>
        /// Determines whether there is collusion between the two Players.
        /// </summary>
        /// <param name="a">Player needing the staminacheck.</param>
        /// <param name="staminacost">Stamina needed.</param>
        /// <returns>True if there is enough stamina.</returns>
        bool EnoughStamina(Player a, int staminacost);

        /// <summary>
        /// Jumping logic.
        /// </summary>
        /// <param name="a">Player in air.</param>
        /// <param name="b">Other Player.</param>
        void JumpLogic(Player a, Player b);
    }
}
