// <copyright file="GameViewModel.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Main.ViewModel
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using TimeWar.Main.View;

    /// <summary>
    /// Game view model class.
    /// </summary>
    public class GameViewModel : ViewModelBase
    {
        private INavigationService<NavigationPages> navigationService;
        private RelayCommand menuPageCommand;
        private string mapName;
        private bool menuVisibility;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">Navigation service.</param>
        public GameViewModel(INavigationService<NavigationPages> navigationService)
        {
            this.MenuVisibility = false;
            this.navigationService = navigationService;
        }

        /// <summary>
        /// Gets or sets map name.
        /// </summary>
        public string MapName
        {
            get { return this.mapName; }
            set { this.mapName = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether visible.
        /// </summary>
        public bool MenuVisibility
        {
            get { return this.menuVisibility; }
            set { this.Set(ref this.menuVisibility, value); }
        }

        /// <summary>
        /// Gets navigation service context.
        /// </summary>
        public INavigationService<NavigationPages> NavigationContext
        {
            get { return this.navigationService; }
        }

        /// <summary>
        /// Gets the navigate to game page command.
        /// </summary>
        public RelayCommand MenuPageCommand => this.menuPageCommand
                    ?? (this.menuPageCommand = new RelayCommand(
                    () => this.navigationService.NavigateTo("MenuPage")));
    }
}
