// <copyright file="Player.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace StreetFighter.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Media;

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
            this.geometry = baseGeometry;
        }

        /// <summary>
        /// Initializes static members of the <see cref="Player"/> class.
        /// Genarates the static geometries.
        /// </summary>
        static Player()
        {
            baseGeometry = new RectangleGeometry(new Rect(0, 0, 30, 100));

            StreamGeometry punchStream = new StreamGeometry();
            Point[] punchPoints = { new Point(0, 0), new Point(30, 0), new Point(30, 20), new Point(65, 20), new Point(65, 30), new Point(30, 30), new Point(30, 100), new Point(0, 100) };
            using (StreamGeometryContext ctx = punchStream.Open())
            {
                ctx.BeginFigure(punchPoints[0], true, true);
                ctx.PolyLineTo(punchPoints.ToList(), true, true);
            }

            //punchStream.Freeze();

            StreamGeometry rightPunchStream = punchStream.Clone();
            rightPunchStream.Transform = new ScaleTransform(-1, 1);

            facing_Right_Punch_Geometry = rightPunchStream;
            facing_Left_Punch_Geometry = punchStream;

            StreamGeometry kickStream = new StreamGeometry();
            Point[] kickPoints = { new Point(0, 0), new Point(30, 0), new Point(30, 40), new Point(65, 20), new Point(65, 30), new Point(30, 50), new Point(30, 100), new Point(0, 100) };
            using (StreamGeometryContext ctx = kickStream.Open())
            {
                ctx.BeginFigure(punchPoints[0], true, true);
                ctx.PolyLineTo(punchPoints.ToList(), true, true);
            }

            //kickStream.Freeze();

            StreamGeometry rightKickStream = kickStream.Clone();
            rightKickStream.Transform = new ScaleTransform(-1, 1);

            facing_Right_Kick_Geometry = rightKickStream;
            facing_Left_Kick_Geometry = kickStream;

        }

        static Geometry baseGeometry; 

        static Geometry facing_Left_Punch_Geometry;

        static Geometry facing_Left_Kick_Geometry;

        static Geometry facing_Right_Punch_Geometry;

        static Geometry facing_Right_Kick_Geometry;

        Geometry geometry;
        public Geometry Geometry
        {
            get
            {
                return this.geometry;
            }
            set
            {
                this.geometry = value;
            }
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
