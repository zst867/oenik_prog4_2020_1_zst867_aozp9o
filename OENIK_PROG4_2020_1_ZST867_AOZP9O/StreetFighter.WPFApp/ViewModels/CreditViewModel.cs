// <copyright file="CreditViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace WpfAppTemporary.Viewmodels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.CommandWpf;

    /// <summary>
    /// ViewModel for credit window.
    /// </summary>
    public class CreditViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreditViewModel"/> class.
        /// </summary>
        public CreditViewModel()
        {
            this.CloseCommand = new RelayCommand(() => this.Close());
        }

        /// <summary>
        /// Gets CloseCommand.
        /// </summary>
        public ICommand CloseCommand { get; private set; }

        /// <summary>
        /// Gets or sets CloseAction.
        /// </summary>
        public Action CloseAction { get; set; }

        private void Close()
        {
            this.CloseAction();
        }
    }
}
