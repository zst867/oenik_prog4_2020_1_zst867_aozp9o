// <copyright file="CreditWindow.xaml.cs" company="PlaceholderCompany">
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
    using WpfAppTemporary.Viewmodels;

    /// <summary>
    /// Interaction logic for Credits.xaml.
    /// </summary>
    public partial class CreditWindow : Window
    {
        private readonly CreditViewModel vm2;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreditWindow"/> class.
        /// </summary>
        public CreditWindow()
        {
            this.InitializeComponent();
            this.vm2 = this.FindResource("my_viewmodel2") as CreditViewModel;
            if (this.vm2.CloseAction == null)
            {
                this.vm2.CloseAction = new Action(() => this.Close());
            }
        }
    }
}
