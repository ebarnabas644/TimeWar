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
        private GameControl gc;
        private Control menu;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameWindow"/> class.
        /// </summary>
        public GameWindow()
        {
            this.InitializeComponent();
            this.vm = this.DataContext as GameViewModel;
            this.gc = this.FindName("cont") as GameControl;
            this.menu = this.FindName("pauseMenu") as Control;
            string param = (string)this.vm.NavigationContext.Parameter;
            if (param == "saveloading")
            {
                this.gc.SaveLoad = true;
            }
            else
            {
                this.gc.MapName = param;
            }
        }

        private void Unsubscribe_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.vm.MenuVisibility = false;
            if (this.gc.LevelFinished)
            {
                this.gc.SaveEndGame();
            }

            this.gc.SaveGameProgress();
            this.gc.BackgroundMusic.Close();
            this.gc.Exit = true;
        }

        private void Continue_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.gc.IsPaused = !this.gc.IsPaused;
            this.vm.MenuVisibility = false;
        }
    }
}
