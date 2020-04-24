// <copyright file="LoadGameViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace StreetFighter.WPFApp.ViewmodelSG
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Input;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.CommandWpf;
    using StreetFighter.BusinessLogic;
    using StreetFighter.WPFApp.Viewmodel;

    /// <summary>
    /// ViewModel for load game window.
    /// </summary>
    public class LoadGameViewModel : ViewModelBase
    {
        private static readonly string Filename = "saved_games.txt";

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadGameViewModel"/> class.
        /// </summary>
        public LoadGameViewModel()
        {
            this.Gm = MainMenuViewModel.Gm;
            ILogicLoadGame l = new LogicLoadGame();
            this.SavedGameCollection = new ObservableCollection<SavedGame>(l.ReadGame(Filename));
            this.DeleteGameCommand = new RelayCommand(() => this.Delete());
            this.LoadSelectedCommand = new RelayCommand(() => this.Load(this.Gm));
            this.CloseCommand = new RelayCommand(() => this.Close());
        }

        /// <summary>
        /// Gets or sets GameModel.
        /// </summary>
        public GameModel Gm { get; set; }

        /// <summary>
        /// Gets or sets SavedGameCollection.
        /// </summary>
        public ObservableCollection<SavedGame> SavedGameCollection { get; set; }

        /// <summary>
        /// Gets or sets SelectedGame.
        /// </summary>
        public SavedGame SelectedGame { get; set; }

        /// <summary>
        /// Gets LoadSelectedCommand.
        /// </summary>
        public ICommand LoadSelectedCommand { get; private set; }

        /// <summary>
        /// Gets DeleteGameCommand.
        /// </summary>
        public ICommand DeleteGameCommand { get; private set; }

        /// <summary>
        /// Gets RemoveGameCommand.
        /// </summary>
        public ICommand RemoveGameCommand { get; private set; }

        /// <summary>
        /// Gets CloseCommand.
        /// </summary>
        public ICommand CloseCommand { get; private set; }

        /// <summary>
        /// Gets or sets CloseAction.
        /// </summary>
        public Action CloseAction { get; set; }

        private void Delete()
        {
            ILogicLoadGame log = new LogicLoadGame();
            int counter = 0;
            int counterfound = 0;
            foreach (SavedGame g in this.SavedGameCollection)
            {
                counter++;
                if (g == this.SelectedGame)
                {
                    counterfound = counter;
                }

                if ((counterfound != 0) && (g != this.SelectedGame))
                {
                    g.Id--;
                }
            }

            log.Delete(counterfound, this.SelectedGame.Name, this.SelectedGame.Hour, this.SelectedGame.Minute, Filename);
            this.SavedGameCollection.Remove(this.SelectedGame);
        }

        private void Load(GameModel gm)
        {
            ILogicLoadGame l = new LogicLoadGame();
            List<Player> players = l.Read(this.SelectedGame.Id, Filename);
            gm.Player1 = players.First();
            gm.Player2 = players.Last();
            gm.Modified = true;
            this.CloseAction();
        }

        private void Close()
        {
            this.CloseAction();
        }
    }
}
