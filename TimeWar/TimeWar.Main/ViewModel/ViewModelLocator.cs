// <copyright file="ViewModelLocator.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Main.ViewModel
{
    using CommonServiceLocator;
    using GalaSoft.MvvmLight.Messaging;
    using TimeWar.Main.BL;
    using TimeWar.Main.BL.Classes;
    using TimeWar.Main.BL.Interfaces;
    using TimeWar.Main.Data;
    using TimeWar.Main.View;
    using TimeWar.Main.ViewModel;

    /// <summary>
    /// View model locator class.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelLocator"/> class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => MyIoc.Instance);
            MyIoc.Instance.Register<Factory, Factory>();
            MyIoc.Instance.Register<IViewerLogicUI, ViewerLogicUI>();
            MyIoc.Instance.Register<IManagerLogicUI, ManagerLogicUI>();
            MyIoc.Instance.Register<IMessenger>(() => Messenger.Default);
            MyIoc.Instance.Register<MainViewModel>();
            MyIoc.Instance.Register<MenuViewModel>();
            MyIoc.Instance.Register<GameViewModel>();
            MyIoc.Instance.Register<ProfilesViewModel>();
            MyIoc.Instance.Register<NewGameViewModel>();
            SetupNavigation();
            this.MainViewModel = ServiceLocator.Current.GetInstance<MainViewModel>();
            this.MenuViewModel = ServiceLocator.Current.GetInstance<MenuViewModel>();
            this.GameViewModel = ServiceLocator.Current.GetInstance<GameViewModel>();
            this.ProfilesViewModel = ServiceLocator.Current.GetInstance<ProfilesViewModel>();
            this.NewGameViewModel = ServiceLocator.Current.GetInstance<NewGameViewModel>();
        }

        /// <summary>
        /// Gets or sets menu view model.
        /// </summary>
        public MenuViewModel MenuViewModel { get; set; }

        /// <summary>
        /// Gets or sets main frame view model.
        /// </summary>
        public MainViewModel MainViewModel { get; set; }

        /// <summary>
        /// Gets or sets game view model.
        /// </summary>
        public GameViewModel GameViewModel { get; set; }

        /// <summary>
        /// Gets or sets profiles view model.
        /// </summary>
        public ProfilesViewModel ProfilesViewModel { get; set; }

        /// <summary>
        /// Gets or sets new game view model.
        /// </summary>
        public NewGameViewModel NewGameViewModel { get; set; }

        private static void SetupNavigation()
        {
            var navigationService = new NavigationService<NavigationPages>();
            navigationService.ConfigurePages();
            MyIoc.Instance.Register<INavigationService<NavigationPages>>(() => navigationService);
        }
    }
}
