// <copyright file="MainViewModel.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Main.ViewModel
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using TimeWar.Main.View;

    /// <summary>
    /// Main view model.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private INavigationService<NavigationPages> navigationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">Navigator service.</param>
        public MainViewModel(INavigationService<NavigationPages> navigationService)
        {
            this.navigationService = navigationService;
            if (this.IsInDesignMode)
            {
            }
            else
            {
                this.LoadedCommand = new RelayCommand(() => this.navigationService.NavigateTo("MenuPage"));
            }
        }

        /// <summary>
        /// Gets main menu on load.
        /// </summary>
        public RelayCommand LoadedCommand { get; private set; }
    }
}
