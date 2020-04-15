// <copyright file="HighScoreWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace WpfAppTemporary
{
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

    /// <summary>
    /// Interaction logic for HighScoreWindow.xaml.
    /// </summary>
    public partial class HighScoreWindow : Window
    {
        private readonly HighScoreViewModel vm2;

        /// <summary>
        /// Initializes a new instance of the <see cref="HighScoreWindow"/> class.
        /// </summary>
        public HighScoreWindow()
        {
            this.InitializeComponent();
            this.vm2 = this.FindResource("my_viewmodel") as HighScoreViewModel;
            if (this.vm2.CloseAction == null)
            {
                this.vm2.CloseAction = new Action(() => this.Close());
            }
        }
    }
}
