// <copyright file="IGameObject.cs" company="Time War">
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
    /// Game object interface.
    /// </summary>
    public interface IGameObject
    {
        /// <summary>
        /// Gets or sets character height in pixel.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets character width in pixel.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the character sprite file name.
        /// </summary>
        public string SpriteFile { get; set; }

        /// <summary>
        /// Gets or sets character position.
        /// </summary>
        public Point Position { get; set; }
    }
}
