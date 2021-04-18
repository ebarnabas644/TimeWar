// <copyright file="GamePage.xaml.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Main.View
{
    using System.Windows.Controls;
    using TimeWar.Main.ViewModel;

    /// <summary>
    /// Interaction logic for GameWindow.xaml.
    /// </summary>
    public partial class GameWindow : Page
    {
        private GameViewModel vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameWindow"/> class.
        /// </summary>
        public GameWindow()
        {
            this.InitializeComponent();
            this.vm = this.DataContext as GameViewModel;
            GameControl gc = this.FindName("cont") as GameControl;
            gc.MapName = (string)this.vm.NavigationContext.Parameter;
        }
    }
}
