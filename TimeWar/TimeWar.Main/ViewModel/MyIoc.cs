// <copyright file="MyIoc.cs" company="Time War">
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

    /// <summary>
    /// SimpleIoc with IServiceLocator interface.
    /// </summary>
    internal class MyIoc : SimpleIoc, IServiceLocator
    {
        /// <summary>
        /// Gets the MyIoc instance.
        /// </summary>
        public static MyIoc Instance { get; private set; } = new MyIoc();
    }
}
