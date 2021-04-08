// <copyright file="MenuViewModel.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Main.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using TimeWar.Main.View;

    /// <summary>
    /// Menu view model class.
    /// </summary>
    public class MenuViewModel : ViewModelBase
    {
        private INavigationService<NavigationPages> navigationService;
        private RelayCommand gamePageCommand;

        private RelayCommand profilePageCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">Navigation service.</param>
        public MenuViewModel(INavigationService<NavigationPages> navigationService)
        {
            this.navigationService = navigationService;
        }

        /// <summary>
        /// Gets the navigate to game page command.
        /// </summary>
        public RelayCommand GamePageCommand => this.gamePageCommand
                    ?? (this.gamePageCommand = new RelayCommand(
                    () => this.navigationService.NavigateTo("GamePage")));

        /// <summary>
        /// Gets the navigate to profile page command.
        /// </summary>
        public RelayCommand ProfilePageCommand => this.profilePageCommand
                       ?? (this.profilePageCommand = new RelayCommand(
                           () => this.navigationService.NavigateTo("ProfilesPage")));
    }
}
