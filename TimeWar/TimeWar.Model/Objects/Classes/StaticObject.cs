// <copyright file="StaticObject.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Model.Objects.Classes
{
    using System.Drawing;
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
        /// <param name="hud">Hud object.</param>
        public StaticObject(int height, int width, string spritefile, Point position, bool hud = false)
        {
            this.Height = height;
            this.Width = width;
            this.SpriteFile = spritefile;
            this.Position = position;
            this.Stance = Stances.StandRight;
            this.Hud = hud;
        }

        /// <inheritdoc/>
        public int Height { get; set; }

        /// <inheritdoc/>
        public int Width { get; set; }

        /// <inheritdoc/>
        public string SpriteFile { get; set; }

        /// <inheritdoc/>
        public Point Position { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether hud or not.
        /// </summary>
        public bool Hud { get; set; }

        /// <inheritdoc/>
        public Stances Stance { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            StaticObject a = (StaticObject)obj;
            if (a != null)
            {
                return this.SpriteFile == a.SpriteFile;
            }

            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.SpriteFile.GetHashCode(System.StringComparison.OrdinalIgnoreCase);
        }
    }
}
