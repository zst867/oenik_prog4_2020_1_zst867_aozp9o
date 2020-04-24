// <copyright file="MainWindow.xaml.cs" company="PlaceholderCompany">
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
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using StreetFighter.WPFApp.Viewmodel;

    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Property used to make window scalable.
        /// </summary>
        public static readonly DependencyProperty ScaleValueProperty = DependencyProperty.Register("ScaleValue", typeof(double), typeof(MainWindow), new UIPropertyMetadata(1.0, new PropertyChangedCallback(OnScaleValueChanged), new CoerceValueCallback(OnCoerceScaleValue)));

#pragma warning disable IDE0052 // Remove unread private members
        private readonly MainMenuViewModel vm;
#pragma warning restore IDE0052 // Remove unread private members

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
            this.vm = this.FindResource("my_viewmodel") as MainMenuViewModel;
        }

        /// <summary>
        /// Gets or sets the value of scaling.
        /// </summary>
        public double ScaleValue
        {
            get
            {
                return (double)this.GetValue(ScaleValueProperty);
            }

            set
            {
                this.SetValue(ScaleValueProperty, value);
            }
        }

        /// <summary>
        /// Helper method for scaling main window.
        /// </summary>
        /// <returns>0.1 or value whichever is bigger.</returns>
        /// <param name="value">Value to be checked.</param>
        protected virtual double OnCoerceScaleValue(double value)
        {
            if (double.IsNaN(value))
            {
                return 1.0f;
            }

            value = Math.Max(0.1, value);
            return value;
        }

        /// <summary>
        /// Helper method for scaling main window.
        /// </summary>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        protected virtual void OnScaleValueChanged(double oldValue, double newValue)
        {
        }

        private static object OnCoerceScaleValue(DependencyObject o, object value)
        {
            if (o is MainWindow mainWindow)
            {
                return mainWindow.OnCoerceScaleValue((double)value);
            }
            else
            {
                return value;
            }
        }

        private static void OnScaleValueChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (o is MainWindow mainWindow)
            {
                mainWindow.OnScaleValueChanged((double)e.OldValue, (double)e.NewValue);
            }
        }

        private void MainGrid_SizeChanged(object sender, EventArgs e)
        {
            this.CalculateScale();
        }

        private void CalculateScale()
        {
            double yScale = this.ActualHeight / 600f;
            double xScale = this.ActualWidth / 400f;
            double value = Math.Min(xScale, yScale);
            this.ScaleValue = (double)OnCoerceScaleValue(this.myMainWindow, value);
        }
    }
}
