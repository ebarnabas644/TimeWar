// <copyright file="NavigationService.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Main.View
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    /// Navigation service class.
    /// </summary>
    /// <typeparam name="T">Enum type.</typeparam>
    public class NavigationService<T> : INavigationService<T>, INotifyPropertyChanged
    {
        /// <summary>
        /// The xaml x:Name used by the frame the navigationservice will use for pages.
        /// </summary>
        private readonly string frameName;

        /// <summary>
        /// List of pages.
        /// </summary>
        private readonly Dictionary<string, Uri> pagesByKey;

        /// <summary>
        /// Current page.
        /// </summary>
        private string currentPageKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationService{T}"/> class.
        /// </summary>
        /// <param name="frameName">Main frame name.</param>
        public NavigationService(string frameName = "MainFrame")
        {
            var t = typeof(T);
            if (!t.IsEnum)
            {
                throw new InvalidOperationException(t.ToString() + " is not a valid Type.  Must be an enum.");
            }

            this.pagesByKey = new Dictionary<string, Uri>();
            this.frameName = frameName;
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets current page.
        /// </summary>
        public string CurrentPageKey
        {
            get
            {
                return this.currentPageKey;
            }

            private set
            {
                if (this.currentPageKey != value)
                {
                    this.currentPageKey = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        /// <inheritdoc/>
        public void GoBack()
        {
        }

        /// <inheritdoc/>
        public void NavigateTo(T navigationPage)
        {
            this.NavigateTo(navigationPage.ToString(), null);
        }

        /// <inheritdoc/>
        public void NavigateTo(string pageKey)
        {
            this.NavigateTo(pageKey, null);
        }

        /// <inheritdoc/>
        public void NavigateTo(string pageKey, object parameter)
        {
            if (pageKey != this.CurrentPageKey)
            {
                lock (this.pagesByKey)
                {
                    if (!this.pagesByKey.ContainsKey(pageKey))
                    {
                        throw new ArgumentException(string.Format(System.Globalization.CultureInfo.CurrentCulture, "No such page: {0} ", pageKey), nameof(pageKey));
                    }

                    var frame = GetDescendantFromName(Application.Current.MainWindow, this.frameName) as Frame;

                    if (frame != null)
                    {
                        frame.Source = this.pagesByKey[pageKey];
                    }

                    this.CurrentPageKey = pageKey;
                }
            }
        }

        /// <summary>
        /// Add pages to the dictionary.
        /// </summary>
        /// <param name="pageKey">Name of the page.</param>
        /// <param name="pagePath">Path of the page(optional).</param>
        public void ConfigurePage(string pageKey, Uri pagePath = null)
        {
            if (pagePath == null)
            {
                pagePath = new Uri(string.Join(string.Empty, new string[] { "../View/", pageKey, ".xaml" }), UriKind.Relative);
            }

            lock (this.pagesByKey)
            {
                if (this.pagesByKey.ContainsKey(pageKey))
                {
                    this.pagesByKey[pageKey] = pagePath;
                }
                else
                {
                    this.pagesByKey.Add(pageKey, pagePath);
                }
            }
        }

        /// <summary>
        /// Configure all of the pages for the T enum.
        /// </summary>
        public void ConfigurePages()
        {
            foreach (var value in Enum.GetNames(typeof(T)))
            {
                this.ConfigurePage(value, null);
            }
        }

        /// <summary>
        /// Search for frame.
        /// </summary>
        /// <param name="parent">Current window.</param>
        /// <param name="name">Name of the frame.</param>
        /// <returns>Framework element if found else null.</returns>
        private static FrameworkElement GetDescendantFromName(DependencyObject parent, string name)
        {
            var count = VisualTreeHelper.GetChildrenCount(parent);

            if (count < 1)
            {
                return null;
            }

            for (var i = 0; i < count; i++)
            {
                var frameworkElement = VisualTreeHelper.GetChild(parent, i) as FrameworkElement;
                if (frameworkElement != null)
                {
                    if (frameworkElement.Name == name)
                    {
                        return frameworkElement;
                    }

                    frameworkElement = GetDescendantFromName(frameworkElement, name);
                    if (frameworkElement != null)
                    {
                        return frameworkElement;
                    }
                }
            }

            return null;
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
