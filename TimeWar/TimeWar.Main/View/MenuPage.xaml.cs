// <copyright file="MenuPage.xaml.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Main.View
{
    using System.Windows.Controls;
    using TimeWar.Main.ViewModel;

    /// <summary>
    /// Interaction logic for MenuPage.xaml.
    /// </summary>
    public partial class MenuPage : Page
    {
        private MenuViewModel vm;
        private MenuControl menuControl;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuPage"/> class.
        /// </summary>
        public MenuPage()
        {
            this.InitializeComponent();
            this.vm = this.DataContext as MenuViewModel;
            this.vm.Init();
            this.ProfileLab.Content = this.vm.MenuText;
            this.menuControl = this.FindName("cont") as MenuControl;
            this.menuControl.MapName = "test";
            this.menuControl.ScrollMode = true;
            this.menuControl.TitleEnabled = true;
        }

        private void Unsubscribe_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.menuControl.Exit = true;
        }
    }
}
