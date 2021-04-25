// <copyright file="Bullet.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Model.Objects.Classes
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
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
        /// <param name="destination">Destination of the bullet.</param>
        /// <param name="type">Type of the bullet.</param>
        /// <param name="height">Character height.</param>
        /// <param name="width">Character width.</param>
        /// <param name="spriteFile">Name of the sprite file.</param>
        public Bullet(Point pos, int height, int width, string spriteFile, Point destination, int damage = 10, BulletType type = BulletType.Basic)
        {
            this.Position = pos;
            this.Damage = damage;
            this.Height = height;
            this.Width = width;
            this.SpriteFile = spriteFile;
            this.Stance = Stances.StandRight;
            this.StanceLess = true;
            this.Type = type;
            this.Destination = destination;
            this.Acceleration = 3;
            this.BulletStopwatch = new Stopwatch();
            this.DespawnStopwatch = new Stopwatch();
            this.DespawnStopwatch.Start();
            this.MoveVector = new PointF(0, 0);
            this.MovementVectorF = Normalize(this.GetVectorDirection());
            this.CurrentSprite = 0;
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

        /// <summary>
        /// Gets or sets bullet acceleration.
        /// </summary>
        public int Acceleration { get; set; }

        /// <summary>
        /// Gets or sets bullet destination.
        /// </summary>
        public Point Destination { get; set; }

        /// <summary>
        /// Gets or sets bullet move vector.
        /// </summary>
        public PointF MoveVector { get; set; }

        /// <summary>
        /// Gets or sets bullet acceleration stopwatch.
        /// </summary>
        public Stopwatch BulletStopwatch { get; set; }

        /// <summary>
        /// Gets or sets bullet acceleration stopwatch.
        /// </summary>
        public Stopwatch DespawnStopwatch { get; set; }

        /// <summary>
        /// Gets or sets Movement Vector.
        /// </summary>
        public PointF MovementVectorF { get; set; }

        /// <inheritdoc/>
        public Point MovementVector { get; set; }

        /// <inheritdoc/>
        public Stances Stance { get; set; }

        /// <inheritdoc/>
        public bool StanceLess { get; set; }

        /// <inheritdoc/>
        public int CurrentSprite { get; set; }

        private static PointF Normalize(PointF vector)
        {
            float distance = (float)Math.Sqrt((vector.X * vector.X) + (vector.Y * vector.Y));
            return new PointF(vector.X / distance, vector.Y / distance);
        }

        private PointF GetVectorDirection()
        {
            return new PointF(this.Destination.X - this.Position.X, this.Destination.Y - this.Position.Y);
        }
    }
}
