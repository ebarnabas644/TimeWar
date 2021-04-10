// <copyright file="StaticObject.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Model.Objects.Classes
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TimeWar.Model.Objects.Interfaces;

    /// <summary>
    /// Static object class.
    /// </summary>
    public class StaticObject : IGameObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StaticObject"/> class.
        /// </summary>
        /// <param name="height">Object height.</param>
        /// <param name="width">Object width.</param>
        /// <param name="spritefile">Object sprite file.</param>
        /// <param name="position">Object position.</param>
        public StaticObject(int height, int width, string spritefile, Point position)
        {
            this.Height = height;
            this.Width = width;
            this.SpriteFile = spritefile;
            this.Position = position;
            this.CurrentSprite = 0;
        }

        /// <inheritdoc/>
        public int Height { get; set; }

        /// <inheritdoc/>
        public int Width { get; set; }

        /// <inheritdoc/>
        public string SpriteFile { get; set; }

        /// <inheritdoc/>
        public Point Position { get; set; }

        /// <inheritdoc/>
        public int CurrentSprite { get; set; }
    }
}
