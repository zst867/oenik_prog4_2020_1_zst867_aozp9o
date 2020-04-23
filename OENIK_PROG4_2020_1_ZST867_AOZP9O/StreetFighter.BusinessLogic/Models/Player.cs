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

    public enum PlayerStatus { IsStanding, IsPunching, IsKicking }

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
            this.CX = positionX;
            this.CY = positionY;
            this.DY = 0;
            this.IsJumping = false;
            this.Health = 100;
            this.Stamina = 100;
            this.Score = 0;
            this.FacinLeft = facingLeft;
            this.Invulnerable = false;
            this.Stunned = false;
            this.State = PlayerStatus.IsStanding;
            this.Timer = 0;
            this.Geometry = this.FacinLeft ? facing_Left_BaseGeometry : facing_Right_BaseGeometry;
        }

        static Player()
        {
            facing_Right_BaseGeometry = new RectangleGeometry(new Rect(0, 0, 75, 225));
            facing_Right_BaseGeometry.Transform = null;
            Geometry flg = facing_Right_BaseGeometry.Clone();
            flg.Transform = new ScaleTransform(-1, 1);
            facing_Left_BaseGeometry = flg.GetFlattenedPathGeometry();

            StreamGeometry punchStream = new StreamGeometry();
            Point[] punchPoints = { new Point(0, 0), new Point(75, 0), new Point(75, 30), new Point(120, 30), new Point(120, 60), new Point(75, 60), new Point(75, 225), new Point(0, 225) };
            using (StreamGeometryContext ctx = punchStream.Open())
            {
                ctx.BeginFigure(punchPoints[0], true, true);
                ctx.PolyLineTo(punchPoints.ToList(), true, true);
            }

            StreamGeometry leftPunchStream = punchStream.Clone();
            leftPunchStream.Transform = new ScaleTransform(-1, 1);

            facing_Left_Punch_Geometry = leftPunchStream.GetFlattenedPathGeometry();
            facing_Right_Punch_Geometry = punchStream;

            StreamGeometry kickStream = new StreamGeometry();
            Point[] kickPoints = { new Point(0, 0), new Point(75, 0), new Point(75, 83), new Point(143, 83), new Point(143, 120), new Point(75, 120), new Point(75, 225), new Point(0, 225) };
            using (StreamGeometryContext ctx = kickStream.Open())
            {
                ctx.BeginFigure(kickPoints[0], true, true);
                ctx.PolyLineTo(kickPoints.ToList(), true, true);
            }

            StreamGeometry leftKickStream = kickStream.Clone();
            leftKickStream.Transform = new ScaleTransform(-1, 1);

            facing_Left_Kick_Geometry = leftKickStream.GetFlattenedPathGeometry();
            facing_Right_Kick_Geometry = kickStream;
        }

        public static Geometry facing_Right_BaseGeometry;

        public static Geometry facing_Left_BaseGeometry;

        public static Geometry facing_Right_Punch_Geometry;

        public static Geometry facing_Left_Kick_Geometry;

        public static Geometry facing_Left_Punch_Geometry;

        public static Geometry facing_Right_Kick_Geometry;

        Geometry geometry;

        public Geometry Geometry
        {
            get
            {
                this.geometry.Transform = new TranslateTransform(this.CX, this.CY);
                return this.geometry;
            }

            set
            {
                this.geometry = value.Clone();
                this.geometry.Transform = new TranslateTransform(this.CX, this.CY);
            }
        }

        public Geometry GetGeometryWithoutTransform
        {
            get
            {
                Geometry ret = geometry.Clone();
                ret.Transform = new TranslateTransform(-CX, -CY);
                return ret;
            }
        }

        /// <summary>
        /// Gets or sets the name of the Player object.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the x-position of the Player object.
        /// </summary>
        public int CX { get; set; }

        /// <summary>
        /// Gets or sets the y-position of the Player object.
        /// </summary>
        public int CY { get; set; }

        public int DY { get; set; }

        public bool IsJumping { get; set; }

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

        public PlayerStatus State { get; set; }

        public int Timer { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Player object is stunned.
        /// </summary>
        public bool Stunned { get; set; }

        public bool IsHit(Player otherPlayer)
        {
            return Geometry.Combine(this.Geometry, otherPlayer.Geometry,
                GeometryCombineMode.Intersect, null).GetArea() > 0;
        }
    }
}
