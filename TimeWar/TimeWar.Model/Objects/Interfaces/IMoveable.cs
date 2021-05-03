// <copyright file="IMoveable.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Model.Objects.Interfaces
{
    using System.Drawing;

    /// <summary>
    /// Moveable objects interface.
    /// </summary>
    public interface IMoveable
    {
        /// <summary>
        /// Gets or sets character position.
        /// </summary>
        public Point Position { get; set; }
    }
}
