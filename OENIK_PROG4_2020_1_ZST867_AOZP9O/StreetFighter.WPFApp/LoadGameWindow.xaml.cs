// <copyright file="LoadGameWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace StreetFighter.WPFApp
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
    using StreetFighter.WPFApp.ViewmodelSG;

    /// <summary>
    /// Interaction logic for LoadGameWindow.xaml.
    /// </summary>
    public partial class LoadGameWindow : Window
    {
        private readonly LoadGameViewModel vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadGameWindow"/> class.
        /// </summary>
        public LoadGameWindow()
        {
            this.InitializeComponent();
            this.vm = this.FindResource("my_viewmodel3") as LoadGameViewModel;
            if (this.vm.CloseAction == null)
            {
                this.vm.CloseAction = new Action(() => this.Close());
            }
        }
    }
}
