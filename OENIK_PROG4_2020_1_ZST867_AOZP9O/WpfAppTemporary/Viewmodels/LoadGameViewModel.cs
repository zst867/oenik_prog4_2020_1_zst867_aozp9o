using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using StreetFighter.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace WpfAppTemporary.ViewmodelSG
{
    class LoadGameViewModel : ViewModelBase
    {
        public ObservableCollection<Game> SavedGameCollection { get; set; }
        public Game SelectedGame { get; set; }
        public ICommand LoadSelectedCommand { get; private set; }
        public ICommand DeleteGameCommand { get; private set; }

        public ICommand RemoveGameCommand { get; private set; }
        public ICommand closeCommand { get; private set; }
        
        public Action CloseAction { get; set; }

        public static string filename = "test.txt";

        public LoadGameViewModel()
        {
           
            ILogicLoadGame l = new LogicLoadGame();
            SavedGameCollection = new ObservableCollection<Game>(l.ReadGame(filename));
            DeleteGameCommand = new RelayCommand(() => this.Delete());
            LoadSelectedCommand = new RelayCommand(() => this.Load());
            closeCommand = new RelayCommand(() => this.Close());
        }

        private void Delete()
        {
            ILogicLoadGame log = new LogicLoadGame();
            int counter = 0;
            int counterfound=0;
            foreach (Game g in SavedGameCollection)
            {
                counter++;
                if (g == SelectedGame)
                {
                    counterfound = counter;
                }
            }

            log.Delete(counterfound, SelectedGame.Name, SelectedGame.Hour, SelectedGame.Minute, filename);
            SavedGameCollection.Remove(SelectedGame);
        }

        private void Load()
        {
            MessageBox.Show("not implemented");
        }

        private void Close()
        {
            CloseAction();
        }
    }
}
