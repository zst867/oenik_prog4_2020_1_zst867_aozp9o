// <copyright file="Player.cs" company="PlaceholderCompany">
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
    /// Player class.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class. Empty.
        /// </summary>
        public Player()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="name">Name of the Player object.</param>
        /// <param name="positionX">X Position of the Player object.</param>
        /// <param name="positionY">Y Position of the Player object.</param>
        /// <param name="Health">Health of the Player object.</param>
        /// <param name="Stamina">Stamina of the Player object.</param>
        /// <param name="Score">Score of the Player object.</param>
        /// <param name="facingLeft">Determines whether the Player object is facing left.</param>
        /// <param name="Invulnerable">Determines whether the Player object is invulnerable.</param>
        /// <param name="Stunned">Determines whether the Player object is stunned.</param>
        public Player(string name, int positionX, int positionY, bool facingLeft)
        {
            this.Name = name;
            this.PositionX = positionX;
            this.PositionY = positionY;
            this.Health = 10;
            this.Stamina = 10;
            this.Score = 0;
            this.FacinLeft = facingLeft;
            this.Invulnerable = false;
            this.Stunned = false;
        }

        /// <summary>
        /// Gets or sets the name of the Player object.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the x-position of the Player object.
        /// </summary>
        public int PositionX { get; set; }

        /// <summary>
        /// Gets or sets the y-position of the Player object.
        /// </summary>
        public int PositionY { get; set; }

        /// <summary>
        /// Gets or sets the health of the Player object.
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        /// Gets or sets the stamina of the Player object.
        /// </summary>
        public int Stamina { get; set; }

        /// <summary>
        /// Gets or sets the score of the Player object.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Player object is facing left.
        /// </summary>
        public bool FacinLeft { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Player object is invulnerable.
        /// </summary>
        public bool Invulnerable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Player object is stunned.
        /// </summary>
        public bool Stunned { get; set; }
    }
}
