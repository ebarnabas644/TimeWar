// <copyright file="BulletLogic.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Logic.Classes.Characters.Actions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TimeWar.Model;
    using TimeWar.Model.Objects.Classes;

    /// <summary>
    /// Default bullet logic.
    /// </summary>
    public class BulletLogic
    {
        private GameModel model;
        private Bullet bullet;
        private CommandManager commandManager;
        private Stopwatch bulletStopwatch = new Stopwatch();
        private Stopwatch despawnStopwatch = new Stopwatch();
        private int despawnTime;
        private int acceleration;
        private Point destination;
        private PointF moveVector;

        /// <summary>
        /// Initializes a new instance of the <see cref="BulletLogic"/> class.
        /// </summary>
        /// <param name="model">Game Model.</param>
        /// <param name="bullet">Bullet.</param>
        /// <param name="commandManager">Command manager.</param>
        /// <param name="destination">Destination.</param>
        /// <param name="despawnTime">How many seconds until the bullet despawns.</param>
        public BulletLogic(GameModel model, Bullet bullet, CommandManager commandManager, Point destination, int despawnTime = 30)
        {
            this.model = model;
            this.bullet = bullet;
            this.commandManager = commandManager;
            this.destination = destination;
            this.acceleration = 3;
            this.despawnTime = despawnTime * 1000;
            this.despawnStopwatch.Start();
        }

        /// <summary>
        /// One tick method.
        /// </summary>
        public void OneTick()
        {
            this.Movement();
        }

        private static PointF Normalize(PointF vector)
        {
            float distance = (float)Math.Sqrt((vector.X * vector.X) + (vector.Y * vector.Y));
            return new PointF(vector.X / distance, vector.Y / distance);
        }

        private void Movement()
        {
            switch (this.bullet.Type)
            {
                case BulletType.Basic:
                    this.BasicMovement();
                    break;
                case BulletType.BasicEnemyBullet:
                    this.BasicMovement();
                    break;
                case BulletType.Accelerating:
                    this.AcceleratigMovement();
                    break;
                case BulletType.Bouncing:
                    this.BouncingMovement();
                    break;
                case BulletType.CurvedBouncing:
                    this.CurvedMovement();
                    break;
                default:
                    break;
            }

            this.bullet.Position = new Point(this.bullet.Position.X + (int)this.moveVector.X, this.bullet.Position.Y + (int)this.moveVector.Y);
            MoveCommand cmd = new MoveCommand(this.bullet, this.bullet.Position, this.model);
        }

        private void BasicMovement()
        {
            PointF movementVector = Normalize(this.GetVectorDirection());
            this.moveVector.X += movementVector.X * this.acceleration;
            this.moveVector.Y += movementVector.Y * this.acceleration;
        }

        private void AcceleratigMovement()
        {
            if (!this.bulletStopwatch.IsRunning)
            {
                this.bulletStopwatch.Start();
            }

            if (this.bulletStopwatch.ElapsedMilliseconds > 1000)
            {
                this.acceleration++;
                this.bulletStopwatch.Restart();
            }

            this.BasicMovement();
        }

        private void BouncingMovement()
        {
            Point nextMove = new Point(this.model.CurrentWorld.ConvertPixelToTile(this.bullet.Position.X + (int)this.moveVector.X), this.model.CurrentWorld.ConvertPixelToTile(this.bullet.Position.Y));
            if (this.model.CurrentWorld.SearchGround(nextMove))
            {
                this.moveVector.X *= -1;
            }

            nextMove = new Point(this.model.CurrentWorld.ConvertPixelToTile(this.bullet.Position.X), this.model.CurrentWorld.ConvertPixelToTile(this.bullet.Position.Y + (int)this.moveVector.Y));
            if (this.model.CurrentWorld.SearchGround(nextMove))
            {
                this.moveVector.Y *= -1;
            }

            this.BasicMovement();
        }

        private void CurvedMovement()
        {
            this.BasicMovement();
            if (this.moveVector.Y > 0)
            {
                this.moveVector.Y--;
            }

            if (this.DetectGround())
            {
                this.moveVector.Y *= -1;
            }
        }

        private bool DetectGround()
        {
            Point nextMove = new Point(this.model.CurrentWorld.ConvertPixelToTile(this.bullet.Position.X + (int)this.moveVector.X), this.model.CurrentWorld.ConvertPixelToTile(this.bullet.Position.Y + (int)this.moveVector.Y));
            return this.model.CurrentWorld.SearchGround(nextMove);
        }

        private PointF GetVectorDirection()
        {
            return new PointF(this.destination.X - this.bullet.Position.X, this.destination.Y - this.bullet.Position.Y);
        }
    }
}
