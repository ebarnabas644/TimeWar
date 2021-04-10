// <copyright file="Character.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Model.Objects
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TimeWar.Model.Objects.Interfaces;

    /// <summary>
    /// Movement types.
    /// </summary>
    public enum Stances
    {
        /// <summary>
        /// Base right stance.
        /// </summary>
        StandRight,

        /// <summary>
        /// Base left stance.
        /// </summary>
        StandLeft,

        /// <summary>
        /// Right stance.
        /// </summary>
        Right,

        /// <summary>
        /// Left stance.
        /// </summary>
        Left,

        /// <summary>
        /// Up stance.
        /// </summary>
        Up,

        /// <summary>
        /// Down stance.
        /// </summary>
        Down,

        /// <summary>
        /// Jump stance.
        /// </summary>
        Jump,
    }

    /// <summary>
    /// Basic character information class.
    /// </summary>
    public abstract class Character : IMoveable, IGameObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Character"/> class.
        /// </summary>
        /// <param name="pos">Character position.</param>
        /// <param name="speed">Movement speed.</param>
        /// <param name="health">Base health.</param>
        /// <param name="height">Character height.</param>
        /// <param name="width">Character width.</param>
        /// <param name="spriteFile">Name of the sprite file.</param>
        protected Character(Point pos, int speed, int health, int height, int width, string spriteFile)
        {
            this.Position = pos;
            this.Speed = speed;
            this.Health = health;
            this.Height = height;
            this.Width = width;
            this.SpriteFile = spriteFile;
            this.CurrentSprite = 0;
            this.Direction = Stances.StandRight;
        }

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
        /// Gets or sets current sprite frame.
        /// </summary>
        public int CurrentSprite { get; set; }

        /// <summary>
        /// Gets or sets moving direction.
        /// </summary>
        public Stances Direction { get; set; }

        /// <inheritdoc/>
        public Point Position { get; set; }

        /// <inheritdoc/>
        public int Speed { get; set; }

        /// <summary>
        /// Gets or sets the character health.
        /// </summary>
        public int Health { get; set; }
    }
}
