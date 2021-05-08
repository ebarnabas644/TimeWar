// <copyright file="MenuViewModel.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Main.ViewModel
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using TimeWar.Main.BL.Interfaces;
    using TimeWar.Main.Data;
    using TimeWar.Main.View;

    /// <summary>
    /// Menu view model class.
    /// </summary>
    public class MenuViewModel : ViewModelBase
    {
        private INavigationService<NavigationPages> navigationService;
        private IViewerLogicUI viewerLogicUI;
        private bool continueVisibility;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">Navigation service.</param>
        /// <param name="viewerLogicUI">Viewer logic.</param>
        public MenuViewModel(INavigationService<NavigationPages> navigationService, IViewerLogicUI viewerLogicUI)
        {
            this.navigationService = navigationService;

            if (this.IsInDesignMode)
            {
            }
            else
            {
                this.NewGamePageCommand = new RelayCommand(() => this.navigationService.NavigateTo("NewGamePage"));
                this.ContinueGameCommand = new RelayCommand(() => this.navigationService.NavigateTo("GamePage", "saveloading"));
                this.ProfilesPageCommand = new RelayCommand(() => this.navigationService.NavigateTo("ProfilesPage"));
                this.ExitCommand = new RelayCommand(() => System.Windows.Application.Current.Shutdown());
                this.viewerLogicUI = viewerLogicUI;
            }
        }

        /// <summary>
        /// Gets the continue game command.
        /// </summary>
        public RelayCommand ContinueGameCommand { get; private set; }

        /// <summary>
        /// Gets the navigate to game page command.
        /// </summary>
        public RelayCommand NewGamePageCommand { get; private set; }

        /// <summary>
        /// Gets the navigate to profile page command.
        /// </summary>
        public RelayCommand ProfilesPageCommand { get; private set; }

        /// <summary>
        /// Gets the navigate to profile page command.
        /// </summary>
        public RelayCommand ExitCommand { get; private set; }

        /// <summary>
        /// Gets the currently selected profile.
        /// </summary>
        public PlayerProfileUI SelectedProfile { get; private set; }

        /// <summary>
        /// Gets the main menu text.
        /// </summary>
        public string MenuText { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether continue button visible.
        /// </summary>
        public bool ContinueVisibility
        {
            get { return this.continueVisibility; }
            set { this.Set(ref this.continueVisibility, value); }
        }

        /// <summary>
        /// Load menu text.
        /// </summary>
        public void Init()
        {
            if (this.viewerLogicUI != null)
            {
                PlayerProfileUI q = this.viewerLogicUI.GetSelectedProfile();
                if (q == null)
                {
                    this.MenuText = "Welcome back Guest!";
                    this.ContinueVisibility = false;
                }
                else
                {
                    this.SelectedProfile = q;
                    this.MenuText = "Welcome back " + this.SelectedProfile.PlayerName + "!";
                    if (q.Save == null)
                    {
                        this.ContinueVisibility = false;
                    }
                    else
                    {
                        this.ContinueVisibility = true;
                    }
                }
            }
        }
    }
}
