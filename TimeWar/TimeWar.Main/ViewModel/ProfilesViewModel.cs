// <copyright file="ProfilesViewModel.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Main.ViewModel
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using CommonServiceLocator;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Ioc;
    using TimeWar.Main.BL.Interfaces;
    using TimeWar.Main.Data;
    using TimeWar.Main.View;

    /// <summary>
    /// Profile view model class.
    /// </summary>
    public class ProfilesViewModel : ViewModelBase
    {
        private INavigationService<NavigationPages> navigationService;
        private ObservableCollection<PlayerProfileUI> playerProfileUIs;
        private IViewerLogicUI viewerLogicUI;
        private IManagerLogicUI managerLogicUI;
        private PlayerProfileUI selectedPlayer;
        private PlayerProfileUI editing;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfilesViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">Navigation service.</param>
        /// <param name="viewerLogicUI">Viewer logic.</param>
        /// <param name="managerLogicUI">Manager logic.</param>
        public ProfilesViewModel(INavigationService<NavigationPages> navigationService, IViewerLogicUI viewerLogicUI, IManagerLogicUI managerLogicUI)
        {
            this.playerProfileUIs = new ObservableCollection<PlayerProfileUI>();
            this.editing = new PlayerProfileUI();
            this.navigationService = navigationService;
            this.viewerLogicUI = viewerLogicUI;
            this.managerLogicUI = managerLogicUI;
            if (this.IsInDesignMode)
            {
            }
            else
            {
                this.viewerLogicUI.GetProfiles().ToList().ForEach(x => this.playerProfileUIs.Add(x));
                this.MenuPageCommand = new RelayCommand(() =>
                {
                    if (this.SelectedPlayer != null)
                    {
                        PlayerProfileUI deselected = this.viewerLogicUI.GetSelectedProfile();
                        if (deselected != null)
                        {
                            deselected.Selected = false;
                            this.managerLogicUI.ModifyProfile(deselected);
                        }

                        this.SelectedPlayer.Selected = true;
                        this.managerLogicUI.ModifyProfile(this.selectedPlayer);
                    }

                    this.navigationService.NavigateTo("MenuPage");
                });
                this.CreateProfileCommand = new RelayCommand(() =>
                {
                    this.managerLogicUI.CreateProfile(this.PlayerProfileUIs, this.editing);
                    this.playerProfileUIs.Clear();
                    this.viewerLogicUI.GetProfiles().ToList().ForEach(x => this.playerProfileUIs.Add(x));
                    this.editing = new PlayerProfileUI();
                });
                this.ModifyProfileCommand = new RelayCommand(() =>
                {
                    this.managerLogicUI.ModifyProfile(this.editing);
                    this.editing = new PlayerProfileUI();
                });
                this.DeleteProfileCommand = new RelayCommand(() =>
                {
                    this.managerLogicUI.DeleteProfile(this.playerProfileUIs, this.selectedPlayer);
                    this.editing = new PlayerProfileUI();
                });
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfilesViewModel"/> class.
        /// </summary>
        [PreferredConstructor]
        public ProfilesViewModel()
            : this(IsInDesignModeStatic ? null : ServiceLocator.Current.GetInstance<INavigationService<NavigationPages>>(), IsInDesignModeStatic ? null : ServiceLocator.Current.GetInstance<IViewerLogicUI>(), IsInDesignModeStatic ? null : ServiceLocator.Current.GetInstance<IManagerLogicUI>())
        {
        }

        /// <summary>
        /// Gets the navigate to game page command.
        /// </summary>
        public RelayCommand MenuPageCommand { get; private set; }

        /// <summary>
        /// Gets the create profile command.
        /// </summary>
        public RelayCommand CreateProfileCommand { get; private set; }

        /// <summary>
        /// Gets the create profile command.
        /// </summary>
        public RelayCommand ModifyProfileCommand { get; private set; }

        /// <summary>
        /// Gets the create profile command.
        /// </summary>
        public RelayCommand DeleteProfileCommand { get; private set; }

        /// <summary>
        /// Gets or sets currently selected player.
        /// </summary>
        public PlayerProfileUI SelectedPlayer
        {
            get { return this.selectedPlayer; }
            set { this.Set(ref this.selectedPlayer, value); }
        }

        /// <summary>
        /// Gets or sets editing instance.
        /// </summary>
        public PlayerProfileUI Editing
        {
            get { return this.editing; }
            set { this.Set(ref this.editing, value); }
        }

        /// <summary>
        /// Gets the player profiles collection.
        /// </summary>
        public ObservableCollection<PlayerProfileUI> PlayerProfileUIs
        {
            get
            {
                return this.playerProfileUIs;
            }

            private set
            {
                this.playerProfileUIs = value;
            }
        }
    }
}
