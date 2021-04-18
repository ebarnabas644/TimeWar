// <copyright file="INavigationService.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Main.View
{
    using GalaSoft.MvvmLight.Views;

    /// <summary>
    /// Navigation service interface for navigation between pages.
    /// </summary>
    /// <typeparam name="T">Page type.</typeparam>
    public interface INavigationService<T> : INavigationService
    {
        /// <summary>
        /// Gets optional parameter.
        /// </summary>
        object Parameter { get; }

        /// <summary>
        /// Navigate to the selected page.
        /// </summary>
        /// <param name="navigationPage">Name of the page.</param>
        void NavigateTo(T navigationPage);
    }
}
