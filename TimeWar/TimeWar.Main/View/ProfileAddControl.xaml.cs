// <copyright file="ProfileAddControl.xaml.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Main.View
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
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
    using System.Windows.Threading;
    using TimeWar.Main.Data;
    using TimeWar.Main.ViewModel;

    /// <summary>
    /// Interaction logic for ProfileEditorControl.xaml.
    /// </summary>
    public partial class ProfileAddControl : UserControl
    {
        private ProfilesViewModel vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileAddControl"/> class.
        /// </summary>
        public ProfileAddControl()
        {
            this.InitializeComponent();
            this.vm = this.DataContext as ProfilesViewModel;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
