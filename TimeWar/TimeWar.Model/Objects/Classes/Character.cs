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
    public enum Directions
    {
        /// <summary>
        /// Base stance.
        /// </summary>
        Stand,

        /// <summary>
        /// Right direction.
        /// </summary>
        Right,

        /// <summary>
        /// Left direction.
        /// </summary>
        Left,

        /// <summary>
        /// Up direction.
        /// </summary>
        Up,

        /// <summary>
        /// Down direction.
        /// </summary>
        Down,
    }

    /// <summary>
    /// Basic character information class.
    /// </summary>
    public abstract class Character : IMoveable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Character"/> class.
        /// </summary>
        /// <param name="pos">Character position.</param>
        /// <param name="speed">Movement speed.</param>
        /// <param name="health">Base health.</param>
        protected Character(Point pos, int speed, int health)
        {
            this.Position = pos;
            this.Speed = speed;
            this.Health = health;
            this.Direction = Directions.Stand;
        }

        /// <summary>
        /// Gets or sets moving direction.
        /// </summary>
        public Directions Direction { get; set; }

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
