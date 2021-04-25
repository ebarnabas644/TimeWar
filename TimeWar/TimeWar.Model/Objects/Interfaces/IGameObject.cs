// <copyright file="IGameObject.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Model.Objects.Interfaces
{
    using System.Drawing;

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
        /// Jump right stance.
        /// </summary>
        JumpRight,

        /// <summary>
        /// Jump left stance.
        /// </summary>
        JumpLeft,
    }

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
        /// Gets or sets object stance.
        /// </summary>
        public Stances Stance { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether stateless.
        /// </summary>
        public bool StanceLess { get; set; }

        /// <summary>
        /// Gets or sets movement vector.
        /// </summary>
        public Point MovementVector { get; set; }

        /// <summary>
        /// Gets or sets current sprite frame value.
        /// </summary>
        public int CurrentSprite { get; set; }

        /// <summary>
        /// Gets or sets character position.
        /// </summary>
        public Point Position { get; set; }
    }
}
