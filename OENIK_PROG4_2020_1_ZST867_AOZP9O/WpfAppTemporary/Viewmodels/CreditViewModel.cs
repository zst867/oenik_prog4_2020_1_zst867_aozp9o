using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfAppTemporary.Viewmodels
{
    class CreditViewModel : ViewModelBase
    {
        public ICommand closeCommand { get; private set; }
       
        public Action CloseAction { get; set; }

        public CreditViewModel()
        {
            closeCommand = new RelayCommand(() => this.Close());
        }

        private void Close()
        {
            CloseAction();
        }
    }
}
