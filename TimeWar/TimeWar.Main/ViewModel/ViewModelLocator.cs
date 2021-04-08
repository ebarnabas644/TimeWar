// <copyright file="ViewModelLocator.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Main.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CommonServiceLocator;
    using GalaSoft.MvvmLight.Ioc;
    using TimeWar.Main.View;

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
            MyIoc.Instance.Register<MainViewModel>();
            MyIoc.Instance.Register<MenuViewModel>();
            MyIoc.Instance.Register<GameViewModel>();
            SetupNavigation();
            this.MainViewModel = ServiceLocator.Current.GetInstance<MainViewModel>();
            this.MenuViewModel = ServiceLocator.Current.GetInstance<MenuViewModel>();
            this.GameViewModel = ServiceLocator.Current.GetInstance<GameViewModel>();
        }

        public MenuViewModel MenuViewModel { get; set; }

        public MainViewModel MainViewModel { get; set; }

        public GameViewModel GameViewModel { get; set; }

        private static void SetupNavigation()
        {
            var navigationService = new NavigationService<NavigationPages>();
            navigationService.ConfigurePages();
            MyIoc.Instance.Register<INavigationService<NavigationPages>>(() => navigationService);
        }
    }
}
