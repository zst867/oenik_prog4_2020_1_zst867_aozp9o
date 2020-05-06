// <copyright file="Player.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace StreetFighter.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// PlayerStatus enum.
    /// </summary>
    public enum PlayerStatus
    {
        /// <summary>
        /// Represents standing.
        /// </summary>
        IsStanding,

        /// <summary>
        /// Represents punching.
        /// </summary>
        IsPunching,

        /// <summary>
        /// Represents kicking.
        /// </summary>
        IsKicking,
    }

    /// <summary>
    /// Player class.
    /// </summary>
    public class Player : INotifyPropertyChanged
    {
        /// <summary>
        /// Name.
        /// </summary>
        private string name;

        /// <summary>
        /// Geometry.
        /// </summary>
        private Geometry geometry;

        /// <summary>
        /// Initializes static members of the <see cref="Player"/> class.
        /// Sets the geometries.
        /// </summary>
        static Player()
        {
            FacingRightBaseGeometry = new RectangleGeometry(new Rect(0, 0, 75, 225))
            {
                Transform = null,
            };
            Geometry flg = FacingRightBaseGeometry.Clone();
            flg.Transform = new ScaleTransform(-1, 1);
            FacingLeftBaseGeometry = flg.GetFlattenedPathGeometry();

            StreamGeometry punchStream = new StreamGeometry();
            Point[] punchPoints = { new Point(0, 0), new Point(75, 0), new Point(75, 30), new Point(120, 30), new Point(120, 60), new Point(75, 60), new Point(75, 225), new Point(0, 225) };
            using (StreamGeometryContext ctx = punchStream.Open())
            {
                ctx.BeginFigure(punchPoints[0], true, true);
                ctx.PolyLineTo(punchPoints.ToList(), true, true);
            }

            StreamGeometry leftPunchStream = punchStream.Clone();
            leftPunchStream.Transform = new ScaleTransform(-1, 1);

            FacingLeftPunchGeometry = leftPunchStream.GetFlattenedPathGeometry();
            FacingRightPunchGeometry = punchStream;

            StreamGeometry kickStream = new StreamGeometry();
            Point[] kickPoints = { new Point(0, 0), new Point(75, 0), new Point(75, 83), new Point(143, 83), new Point(143, 120), new Point(75, 120), new Point(75, 225), new Point(0, 225) };
            using (StreamGeometryContext ctx = kickStream.Open())
            {
                ctx.BeginFigure(kickPoints[0], true, true);
                ctx.PolyLineTo(kickPoints.ToList(), true, true);
            }

            StreamGeometry leftKickStream = kickStream.Clone();
            leftKickStream.Transform = new ScaleTransform(-1, 1);

            FacingLeftKickGeometry = leftKickStream.GetFlattenedPathGeometry();
            FacingRightKickGeometry = kickStream;
        }

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
        /// <param name="facingLeft">Determines whether the Player object is facing left.</param>
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
            this.Geometry = this.FacinLeft ? FacingLeftBaseGeometry : FacingRightBaseGeometry;
        }

        /// <summary>
        /// Event for monitoring change in object.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets FacingRightBaseGeometry.
        /// </summary>
        public static Geometry FacingRightBaseGeometry { get; set; }

        /// <summary>
        /// Gets or sets FacingLeftBaseGeometry.
        /// </summary>
        public static Geometry FacingLeftBaseGeometry { get; set; }

        /// <summary>
        /// Gets or sets FacingRightPunchGeometry.
        /// </summary>
        public static Geometry FacingRightPunchGeometry { get; set; }

        /// <summary>
        /// Gets or sets FacingRightKickGeometry.
        /// </summary>
        public static Geometry FacingRightKickGeometry { get; set; }

        /// <summary>
        /// Gets or sets FacingLeftKickGeometry.
        /// </summary>
        public static Geometry FacingLeftKickGeometry { get; set; }

        /// <summary>
        /// Gets or sets FacingLeftPunchGeometry.
        /// </summary>
        public static Geometry FacingLeftPunchGeometry { get; set; }

        /// <summary>
        /// Gets or sets the geometry of the Player object.
        /// </summary>
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

        /// <summary>
        /// Gets the geometry without transform.
        /// </summary>
        public Geometry GetGeometryWithoutTransform
        {
            get
            {
                Geometry ret = this.geometry.Clone();
                ret.Transform = new TranslateTransform(-this.CX, -this.CY);
                return ret;
            }
        }

        /// <summary>
        /// Gets or sets the name of the Player object.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the x-position of the Player object.
        /// </summary>
        public int CX { get; set; }

        /// <summary>
        /// Gets or sets the y-position of the Player object.
        /// </summary>
        public int CY { get; set; }

        /// <summary>
        /// Gets or sets the dy-position of the Player object.
        /// </summary>
        public int DY { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Player is jumping.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the Player's state.
        /// </summary>
        public PlayerStatus State { get; set; }

        /// <summary>
        /// Gets or sets the timer.
        /// </summary>
        public int Timer { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Player object is stunned.
        /// </summary>
        public bool Stunned { get; set; }

        /// <summary>
        /// Determines if the Player is hit.
        /// </summary>
        /// <returns> Whether there is collusion between the two Players.</returns>
        /// <param name="otherPlayer">The other Player.</param>
        public bool IsHit(Player otherPlayer)
        {
            return Geometry.Combine(
                this.Geometry,
                otherPlayer.Geometry,
                GeometryCombineMode.Intersect,
                null).GetArea() > 0;
        }

        /// <summary>
        /// Invokes the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">Name of the property in string.</param>
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
