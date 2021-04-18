// <copyright file="ProfilesPage.xaml.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Main.View
{
    using System.Windows.Controls;
    using TimeWar.Main.ViewModel;

    /// <summary>
    /// Interaction logic for ProfilesPage.xaml.
    /// </summary>
    public partial class ProfilesPage : Page
    {
        private ProfilesViewModel vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfilesPage"/> class.
        /// </summary>
        public ProfilesPage()
        {
            this.InitializeComponent();
            this.vm = this.DataContext as ProfilesViewModel;
            MenuControl menuControl = this.FindName("cont") as MenuControl;
            Grid a = this.FindName("MainGrid") as Grid;
            menuControl.MapName = "test2";
            menuControl.ScrollMode = false;
            menuControl.TitleEnabled = false;
        }

        private void Add_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.vm.Editing = new Data.PlayerProfileUI();
            this.AddDialog.Visibility = System.Windows.Visibility.Visible;
        }

        private void Modify_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.vm.SelectedPlayer != null)
            {
                this.vm.Editing = this.vm.SelectedPlayer;
                this.EditDialog.Visibility = System.Windows.Visibility.Visible;
            }
        }
    }
}
