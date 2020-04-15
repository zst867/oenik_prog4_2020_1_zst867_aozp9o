using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using StreetFighter.BusinessLogic;

namespace WpfAppTemporary.ViewmodelsHS
{
    class HighScoreViewModel: ViewModelBase
    {
        public ICommand closeCommand { get; private set; }

        public int HSScore { get; set; }
        public string HSName { get; set; }

        public Action CloseAction { get; set; }

        public HighScoreViewModel()
        {
            ILogicHighScore l = new LogicHighScore();
            HSScore = l.CalculateHighscore("test.txt").Score;
            HSName = l.CalculateHighscore("test.txt").Name;
            closeCommand = new RelayCommand(() => this.Close());
        }

        private void Close()
        {
            CloseAction();
        }


    }
}
