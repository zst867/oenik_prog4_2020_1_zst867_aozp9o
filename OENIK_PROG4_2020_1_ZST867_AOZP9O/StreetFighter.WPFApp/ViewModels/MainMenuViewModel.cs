// <copyright file="MainMenuViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace StreetFighter.WPFApp.Viewmodel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.CommandWpf;
    using StreetFighter.BusinessLogic;

    /// <summary>
    /// ViewModel for main window.
    /// </summary>
    public class MainMenuViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainMenuViewModel"/> class.
        /// </summary>
        public MainMenuViewModel()
        {
            Gm = new GameModel();
            this.NewGameCommand = new RelayCommand(() => this.NewGame(Gm));
            this.LoadGameCommand = new RelayCommand(() => this.LoadGame(Gm));
            this.CreditsCommand = new RelayCommand(() => this.Credits());
            this.HighScoreCommand = new RelayCommand(() => this.HighScore());
            this.ExitGameCommand = new RelayCommand(() => this.ExitGame());
        }

        /// <summary>
        /// Gets or sets GameModel.
        /// </summary>
        public static GameModel Gm { get; set; }

        /// <summary>
        /// Gets NewGameCommand.
        /// </summary>
        public ICommand NewGameCommand { get; private set; }

        /// <summary>
        /// Gets LoadGameCommand.
        /// </summary>
        public ICommand LoadGameCommand { get; private set; }

        /// <summary>
        /// Gets OptionsCommand.
        /// </summary>
        public ICommand OptionsCommand { get; private set; }

        /// <summary>
        /// Gets CreditsCommand.
        /// </summary>
        public ICommand CreditsCommand { get; private set; }

        /// <summary>
        /// Gets HighScoreCommand.
        /// </summary>
        public ICommand HighScoreCommand { get; private set; }

        /// <summary>
        /// Gets ExitGameCommand.
        /// </summary>
        public ICommand ExitGameCommand { get; private set; }

        private void HighScore()
        {
            HighScoreWindow hs = new HighScoreWindow();
            hs.Show();
        }

        private void Credits()
        {
            CreditWindow npw = new CreditWindow();
            npw.Show();
        }

        private void ExitGame()
        {
            Application.Current.Shutdown();
        }

        private void NewGame(GameModel gm)
        {
            PlayerNameWindow pw = new PlayerNameWindow
            {
                DataContext = gm,
            };
            pw.ShowDialog();
            GameWindow gw = new GameWindow
            {
                DataContext = gm,
            };
            gw.Show();
        }

        private void LoadGame(GameModel gm)
        {
            LoadGameWindow npw = new LoadGameWindow();
            npw.ShowDialog();
            if (gm.Modified)
            {
                GameWindow gw = new GameWindow();
                gw.Show();
            }
        }
    }
}
