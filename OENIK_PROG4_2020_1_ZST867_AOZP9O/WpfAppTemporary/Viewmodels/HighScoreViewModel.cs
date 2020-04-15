// <copyright file="HighScoreViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace WpfAppTemporary.ViewmodelsHS
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.CommandWpf;
    using StreetFighter.BusinessLogic;

    /// <summary>
    /// ViewModel for highscore window.
    /// </summary>
    public class HighScoreViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HighScoreViewModel"/> class.
        /// </summary>
        public HighScoreViewModel()
        {
            ILogicHighScore l = new LogicHighScore();
            this.HSScore = l.CalculateHighscore("test.txt").Score;
            this.HSName = l.CalculateHighscore("test.txt").Name;
            this.CloseCommand = new RelayCommand(() => this.Close());
        }

        /// <summary>
        /// Gets CloseCommand.
        /// </summary>
        public ICommand CloseCommand { get; private set; }

        /// <summary>
        /// Gets or sets HSScore.
        /// </summary>
        public int HSScore { get; set; }

        /// <summary>
        /// Gets or sets HSName.
        /// </summary>
        public string HSName { get; set; }

        /// <summary>
        /// Gets or sets CloseAction.
        /// </summary>
        public Action CloseAction { get; set; }

        private void Close()
        {
            this.CloseAction();
        }
    }
}
