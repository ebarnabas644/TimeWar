// <copyright file="Bullet.cs" company="Time War">
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
    /// Types of bullet.
    /// </summary>
    public enum BulletType
    {
        /// <summary>
        /// Basic bullet type.
        /// </summary>
        Basic,

        /// <summary>
        /// Bullet's speed is increasing.
        /// </summary>
        Accelerating,

        /// <summary>
        /// Bullet bounces upon inpact.
        /// </summary>
        Bouncing,

        /// <summary>
        /// Bullet is bouncing on the ground.
        /// </summary>
        CurvedBouncing,
    }

    /// <summary>
    /// Basic bullet.
    /// </summary>
    public class Bullet : IMoveable, IGameObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Bullet"/> class.
        /// </summary>
        /// <param name="pos">Character position.</param>
        /// <param name="damage">Base damage.</param>
        /// <param name="type">Type of the bullet.</param>
        /// <param name="height">Character height.</param>
        /// <param name="width">Character width.</param>
        /// <param name="spriteFile">Name of the sprite file.</param>
        public Bullet(Point pos, int height, int width, string spriteFile, int damage = 10, BulletType type = BulletType.Basic)
        {
            this.Position = pos;
            this.Damage = damage;
            this.Height = height;
            this.Width = width;
            this.SpriteFile = spriteFile;
            this.Stance = Stances.StandRight;
            this.Type = type;
        }

        /// <inheritdoc/>
        public Point Position { get; set; }

        /// <inheritdoc/>
        public int Height { get; set; }

        /// <inheritdoc/>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets damage of the bullet.
        /// </summary>
        public int Damage { get; set; }

        /// <inheritdoc/>
        public string SpriteFile { get; set; }

        /// <summary>
        /// Gets or sets the type of the bullet.
        /// </summary>
        public BulletType Type { get; set; }

        /// <inheritdoc/>
        public Stances Stance { get; set; }
    }
}
