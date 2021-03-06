// <copyright file="SavedGame.cs" company="PlaceholderCompany">
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
    using GalaSoft.MvvmLight;

    /// <summary>
    /// Game class.
    /// </summary>
    public class SavedGame : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the Id of the Game object.
        /// </summary>
        private int id;

        /// <summary>
        /// Initializes a new instance of the <see cref="SavedGame"/> class. Empty.
        /// </summary>
        public SavedGame()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SavedGame"/> class.
        /// </summary>
        /// <param name="id">Id of the Game object.</param>
        /// <param name="name">Name of the Game object.</param>
        /// <param name="hour">Time of the saving (hours).</param>
        /// <param name="minute">Time of the saving (minutes).</param>
        public SavedGame(int id, string name, int hour, int minute)
        {
            this.Id = id;
            this.Name = name;
            this.Hour = hour;
            this.Minute = minute;
        }

        /// <summary>
        /// Event for monitoring change in object.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets id of object.
        /// </summary>
        public int Id
        {
            get
            {
                return this.id;
            }

            set
            {
                this.id = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the Name of the Game object.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the hour of the Game object.
        /// </summary>
        public int Hour { get; set; }

        /// <summary>
        /// Gets or sets the hour of the Game object.
        /// </summary>
        public int Minute { get; set; }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
