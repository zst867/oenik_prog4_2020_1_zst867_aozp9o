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
using WpfAppTemporary.ViewmodelsHS;

namespace WpfAppTemporary
{
    /// <summary>
    /// Interaction logic for HighScoreWindow.xaml
    /// </summary>
    public partial class HighScoreWindow : Window
    {
        private HighScoreViewModel vm2;
        public HighScoreWindow()
        {
            InitializeComponent();
            vm2 = FindResource("my_viewmodel") as HighScoreViewModel;
            if (vm2.CloseAction == null)
                vm2.CloseAction = new Action(() => this.Close());
        }
    }
}
