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
        /// Basic bullet type. Can pass through enemies.
        /// </summary>
        Basic,

        /// <summary>
        /// Basic bullet type. Can pass through enemies.
        /// </summary>
        BasicEnemyBullet,

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
        private string spriteFile;
        private int width;
        private int height;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bullet"/> class.
        /// </summary>
        /// <param name="pos">Character position.</param>
        /// <param name="damage">Base damage.</param>
        /// <param name="destination">Destination of the bullet.</param>
        /// <param name="type">Type of the bullet.</param>
        /// <param name="playerBullet">Is shot by a player.</param>
        /// <param name="height">Character height.</param>
        /// <param name="width">Character width.</param>
        /// <param name="spriteFile">Name of the sprite file.</param>
        public Bullet(Point pos, int height, int width, string spriteFile, Point destination, int damage = 10, BulletType type = BulletType.Basic, bool playerBullet = false)
        {
            this.Position = pos;
            this.Damage = damage;
            this.height = height;
            this.width = width;
            this.Stance = Stances.StandRight;
            this.spriteFile = spriteFile;
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
            this.PlayerBullet = playerBullet;
        }

        /// <inheritdoc/>
        public Point Position { get; set; }

        /// <inheritdoc/>
        public int Height
        {
            get
            {
                int height = 0;
                switch (this.Type)
                {
                    case BulletType.Basic: height = 16; break;
                    case BulletType.BasicEnemyBullet: height = 8; break;
                    case BulletType.Accelerating: height = 7; break;
                    case BulletType.Bouncing: height = 9; break;
                    case BulletType.CurvedBouncing: height = 9; break;
                }

                return height;
            }

            set
            {
                this.height = value;
            }
        }

        /// <inheritdoc/>
        public int Width
        {
            get
            {
                int width = 0;
                switch (this.Type)
                {
                    case BulletType.Basic: width = 16; break;
                    case BulletType.BasicEnemyBullet: width = 8; break;
                    case BulletType.Accelerating: width = 15; break;
                    case BulletType.Bouncing: width = 9; break;
                    case BulletType.CurvedBouncing: width = 9; break;
                }

                return width;
            }

            set
            {
                this.width = value;
            }
        }

        /// <summary>
        /// Gets or sets damage of bullet.
        /// </summary>
        public int Damage { get; set; }

        /// <inheritdoc/>
        public string SpriteFile
        {
            get
            {
                string file = string.Empty;
                switch (this.Type)
                {
                    case BulletType.Basic: file = "basicbullet"; break;
                    case BulletType.BasicEnemyBullet: file = "basicenemybullet"; break;
                    case BulletType.Accelerating: file = "acceleratingbullet"; break;
                    case BulletType.Bouncing: file = "bouncingbullet"; break;
                    case BulletType.CurvedBouncing: file = "curvedbouncingbullet"; break;
                }

                return file;
            }

            set
            {
                this.spriteFile = value;
            }
        }

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

        /// <summary>
        /// Gets or sets a value indicating whether is shot by a player.
        /// </summary>
        public bool PlayerBullet { get; set; }

        /// <inheritdoc/>
        public int CurrentSprite { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            string retString = string.Empty;
            retString += "bullet;"; // Bullet.
            retString += this.Type + ";"; // bullet Type.
            retString += this.Position.X + ";"; // Bullet position X.
            retString += this.Position.Y + ";"; // Bullet position Y.
            retString += this.Height + ";"; // Bullet hight,.
            retString += this.Width + ";"; // Bullet width.
            retString += this.Damage + ";"; // Bullet damage.
            retString += this.MovementVector.X + ";"; // Movementvector X.
            retString += this.MovementVector.Y + ";"; // Movementvector Y.
            retString += this.MovementVectorF.X + ";"; // MovementvectorF X.
            retString += this.MovementVectorF.Y + ";"; // MovementvectorF Y.
            retString += this.Destination.X + ";"; // Bullet destination X.
            retString += this.Destination.Y + ";"; // Bullet destination Y.
            retString += this.Acceleration + ";"; // Bullet acceleration.
            retString += this.PlayerBullet + ";"; // Player bullet or not.
            return retString;
        }

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
