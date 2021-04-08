// <copyright file="GameViewModel.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Main.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GalaSoft.MvvmLight.Command;
    using TimeWar.Main.View;

    /// <summary>
    /// Game view model class.
    /// </summary>
    public class GameViewModel
    {
        private INavigationService<NavigationPages> navigationService;
        private RelayCommand menuPageCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">Navigation service.</param>
        public GameViewModel(INavigationService<NavigationPages> navigationService)
        {
            this.navigationService = navigationService;
        }

        /// <summary>
        /// Gets the navigate to game page command.
        /// </summary>
        public RelayCommand MenuPageCommand => this.menuPageCommand
                    ?? (this.menuPageCommand = new RelayCommand(
                    () => this.navigationService.NavigateTo("Menu")));
    }
}
