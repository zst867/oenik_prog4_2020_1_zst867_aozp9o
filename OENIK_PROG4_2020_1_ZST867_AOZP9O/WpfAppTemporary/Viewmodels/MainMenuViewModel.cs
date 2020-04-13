using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfAppTemporary.Viewmodel
{
        class MainMenuViewModel : ViewModelBase
        {
            public ICommand NewGameCommand { get; private set; }
            public ICommand LoadGameCommand { get; private set; }
            public ICommand OptionsCommand { get; private set; }
            public ICommand CreditsCommand { get; private set; }
            public ICommand HighScoreCommand { get; private set; }
            public ICommand ExitGameCommand { get; private set; }


            public MainMenuViewModel()
            {
            
            //-NewGameCommand = new RelayCommand(() => this.NewGame());
            //LoadGameCommand = new RelayCommand(() => this.LoadGame());
            //OptionsCommand = new RelayCommand(() => this.Options());
                    CreditsCommand = new RelayCommand(() => this.Credits());
                //HighScoreCommand = new RelayCommand(() => this.HighScore());
                    ExitGameCommand = new RelayCommand(() => this.ExitGame());
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
        }   
}
