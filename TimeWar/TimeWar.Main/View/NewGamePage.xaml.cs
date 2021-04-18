// <copyright file="NewGamePage.xaml.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Main.View
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
    using TimeWar.Main.ViewModel;

    /// <summary>
    /// Interaction logic for NewGamePage.xaml.
    /// </summary>
    public partial class NewGamePage : Page
    {
        private NewGameViewModel vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewGamePage"/> class.
        /// </summary>
        public NewGamePage()
        {
            this.InitializeComponent();
            this.vm = this.DataContext as NewGameViewModel;
            this.vm.InitMaps();
            MenuControl menuControl = this.FindName("cont") as MenuControl;
            menuControl.MapName = "test2";
            menuControl.ScrollMode = false;
            menuControl.TitleEnabled = false;
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            this.vm.InitMaps();
        }
    }
}
