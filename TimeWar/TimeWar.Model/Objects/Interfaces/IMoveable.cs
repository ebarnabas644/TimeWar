// <copyright file="IMoveable.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Model.Objects.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Moveable objects interface.
    /// </summary>
    public interface IMoveable
    {
        /// <summary>
        /// Gets or sets character position.
        /// </summary>
        public Point Position { get; set; }

        // <summary>
        // Gets or sets character movement speed.
        // </summary>
        // public int Speed { get; set; }
    }
}
