// <copyright file="NewGameViewModel.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Main.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using TimeWar.Main.BL.Interfaces;
    using TimeWar.Main.Data;
    using TimeWar.Main.View;

    /// <summary>
    /// New game view model.
    /// </summary>
    public class NewGameViewModel : ViewModelBase
    {
        private INavigationService<NavigationPages> navigationService;
        private IViewerLogicUI viewerLogic;
        private ObservableCollection<MapRecordUI> scoreboard;
        private ObservableCollection<MapFiles> maps;
        private MapFiles selectedMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewGameViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">Navigation service.</param>
        /// <param name="viewerLogic">Viewer logic.</param>
        public NewGameViewModel(INavigationService<NavigationPages> navigationService, IViewerLogicUI viewerLogic)
        {
            this.navigationService = navigationService;
            this.viewerLogic = viewerLogic;
            this.maps = new ObservableCollection<MapFiles>();
            if (this.IsInDesignMode)
            {
            }
            else
            {
                this.MenuPageCommand = new RelayCommand(() => this.navigationService.NavigateTo("MenuPage"));
                this.GamePageCommand = new RelayCommand(() =>
                {
                    if (this.selectedMap != null)
                    {
                        this.navigationService.NavigateTo("GamePage", this.SelectedMap.Name);
                    }
                });
                this.InitMaps();
            }
        }

        /// <summary>
        /// Gets or sets currently selected map.
        /// </summary>
        public MapFiles SelectedMap
        {
            get { return this.selectedMap; }
            set { this.Set(ref this.selectedMap, value); }
        }

        /// <summary>
        /// Gets the navigate to menu page command.
        /// </summary>
        public RelayCommand MenuPageCommand { get; private set; }

        /// <summary>
        /// Gets the navigate to game page command.
        /// </summary>
        public RelayCommand GamePageCommand { get; private set; }

        /// <summary>
        /// Gets maps collection.
        /// </summary>
        public ObservableCollection<MapFiles> Maps
        {
            get { return this.maps; }
            private set { this.maps = value; }
        }

        /// <summary>
        /// Gets scoreboard collection.
        /// </summary>
        public ObservableCollection<MapRecordUI> Scoreboard
        {
            get { return this.scoreboard; }
            private set { this.scoreboard = value; }
        }

        /// <summary>
        /// Init avaiable maps.
        /// </summary>
        public void InitMaps()
        {
            this.maps.Clear();
            this.viewerLogic.LoadMaps().ToList().ForEach(x => this.maps.Add(x));
        }
    }
}
