// <copyright file="GameModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace StreetFighter.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;
    using StreetFighter.BusinessLogic;

    /// <summary>
    /// GameModel.
    /// </summary>
    public class GameModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Player2.
        /// </summary>
        private Player player2;

        /// <summary>
        /// Player1.
        /// </summary>
        private Player player1;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameModel"/> class.
        /// </summary>
        public GameModel()
        {
            this.Player1 = new Player("name1", 100, 375, false);
            this.Player2 = new Player("name2", 1100, 375, true);
            this.Modified = false;
            this.Height = 650;
            this.Width = 1200;
        }

        /// <summary>
        /// Event for monitoring change in object.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the Width property.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the Height property.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the Player1 property.
        /// </summary>
        public Player Player1
        {
            get
            {
                return this.player1;
            }

            set
            {
                this.player1 = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the Player2 property.
        /// </summary>
        public Player Player2
        {
            get
            {
                return this.player2;
            }

            set
            {
                this.player2 = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the gamemodel is modified.
        /// </summary>
        public bool Modified { get; set; }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
