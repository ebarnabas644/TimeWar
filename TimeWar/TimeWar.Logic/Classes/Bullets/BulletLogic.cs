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
        private List<Bullet> bullets;
        private CommandManager commandManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="BulletLogic"/> class.
        /// </summary>
        /// <param name="model">Game Model.</param>
        /// <param name="bullet">Bullet.</param>
        /// <param name="commandManager">Command manager.</param>
        public BulletLogic(GameModel model, ICollection<Bullet> bullet, CommandManager commandManager)
        {
            this.model = model;
            if (bullet != null)
            {
                this.bullets = (List<Bullet>)bullet;
            }

            this.commandManager = commandManager;
        }

        /// <summary>
        /// Replaces the bullets list.
        /// </summary>
        /// <param name="bullets">Bullet list.</param>
        public void Addbullets(ICollection<Bullet> bullets)
        {
            this.bullets = (List<Bullet>)bullets;
        }

        /// <summary>
        /// One tick method.
        /// </summary>
        public void OneTick()
        {
            for (int i = 0; i < this.bullets.Count; i++)
            {
                this.Movement(this.bullets[i]);
                this.Despawn(this.bullets[i]);
            }

            // this.DetectEntity(item);
        }

        private static PointF GetVectorDirection(Bullet bullet)
        {
            return new PointF(bullet.Destination.X - bullet.Position.X, bullet.Destination.Y - bullet.Position.Y);
        }

        private static PointF Normalize(PointF vector)
        {
            float distance = (float)Math.Sqrt((vector.X * vector.X) + (vector.Y * vector.Y));
            return new PointF(vector.X / distance, vector.Y / distance);
        }

        private static void BasicMovement(Bullet bullet)
        {
            //Mindig normalizeolja ezért kering a kattintott pont körül
            PointF movementVector = Normalize(GetVectorDirection(bullet));
            bullet.MoveVector = new PointF(bullet.MoveVector.X + (movementVector.X * bullet.Acceleration), bullet.MoveVector.Y + (movementVector.Y * bullet.Acceleration));
        }

        private static void AcceleratigMovement(Bullet bullet)
        {
            if (!bullet.BulletStopwatch.IsRunning)
            {
                bullet.BulletStopwatch.Start();
            }

            if (bullet.BulletStopwatch.ElapsedMilliseconds > 1000)
            {
                bullet.Acceleration++;
                bullet.BulletStopwatch.Restart();
            }

            BasicMovement(bullet);
        }

        private void Movement(Bullet bullet)
        {
            switch (bullet.Type)
            {
                case BulletType.Basic:
                    BasicMovement(bullet);
                    break;
                case BulletType.Accelerating:
                    AcceleratigMovement(bullet);
                    break;
                case BulletType.Bouncing:
                    this.BouncingMovement(bullet);
                    break;
                case BulletType.CurvedBouncing:
                    this.CurvedMovement(bullet);
                    break;
                default:
                    break;
            }

            bullet.Position = new Point(bullet.Position.X + (int)bullet.MoveVector.X, bullet.Position.Y + (int)bullet.MoveVector.Y);
            MoveCommand cmd = new MoveCommand(bullet, bullet.Position, this.model);
        }

        private void BouncingMovement(Bullet bullet)
        {
            Point nextMove = new Point(this.model.CurrentWorld.ConvertPixelToTile(bullet.Position.X + (int)bullet.MoveVector.X), this.model.CurrentWorld.ConvertPixelToTile(bullet.Position.Y));
            if (this.model.CurrentWorld.SearchGround(nextMove))
            {
                bullet.MoveVector = new PointF(bullet.MoveVector.X * -1, bullet.MoveVector.Y);
            }

            nextMove = new Point(this.model.CurrentWorld.ConvertPixelToTile(bullet.Position.X), this.model.CurrentWorld.ConvertPixelToTile(bullet.Position.Y + (int)bullet.MoveVector.Y));
            if (this.model.CurrentWorld.SearchGround(nextMove))
            {
                bullet.MoveVector = new PointF(bullet.MoveVector.X, bullet.MoveVector.Y * -1);
            }

            BasicMovement(bullet);
        }

        private void CurvedMovement(Bullet bullet)
        {
            BasicMovement(bullet);
            if (bullet.MoveVector.Y > 0)
            {
                bullet.MoveVector = new PointF(bullet.MoveVector.X, bullet.MoveVector.Y - 1);
            }

            if (this.DetectGround(bullet))
            {
                bullet.MoveVector = new PointF(bullet.MoveVector.X, bullet.MoveVector.Y * -1);
            }
        }

        private bool DetectGround(Bullet bullet)
        {
            Point nextMove = new Point(this.model.CurrentWorld.ConvertPixelToTile(bullet.Position.X + (int)bullet.MoveVector.X), this.model.CurrentWorld.ConvertPixelToTile(bullet.Position.Y + (int)bullet.MoveVector.Y));
            return this.model.CurrentWorld.SearchGround(nextMove);
        }

        private bool DetectEntity(Bullet bullet)
        {
            throw new NotImplementedException();
        }

        private void Despawn(Bullet bullet)
        {
            if (this.DetectGround(bullet) && !(bullet.Type == BulletType.Bouncing || bullet.Type == BulletType.CurvedBouncing))
            {
                this.model.CurrentWorld.RemoveBullet(bullet);
            }
            else if (bullet.DespawnStopwatch.ElapsedMilliseconds > 20000)
            {
                bullet.DespawnStopwatch.Stop();
                this.model.CurrentWorld.RemoveBullet(bullet);
            }
        }
    }
}
