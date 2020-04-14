using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfAppTemporary.ViewmodelSG;

namespace WpfAppTemporary
{
    /// <summary>
    /// Interaction logic for SaveGameWindow.xaml
    /// </summary>
    public partial class LoadGameWindow : Window
    {
        private LoadGameViewModel vm;

        public LoadGameWindow()
        {
            InitializeComponent();
            vm = FindResource("my_viewmodel3") as LoadGameViewModel;
            if (vm.CloseAction == null)
                vm.CloseAction = new Action(() => this.Close());
        }

    }
}
