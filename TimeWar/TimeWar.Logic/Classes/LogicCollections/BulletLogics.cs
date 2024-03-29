﻿// <copyright file="BulletLogics.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Logic.Classes.LogicCollections
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TimeWar.Data.Models;
    using TimeWar.Logic.Classes.Characters.Actions;
    using TimeWar.Model;
    using TimeWar.Model.Objects.Classes;

    /// <summary>
    /// Default bullet logic.
    /// </summary>
    public class BulletLogics
    {
        private const int MaxBulletSpeed = 15;
        private GameModel model;
        private List<Bullet> bullets;
        private CommandManager commandManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="BulletLogics"/> class.
        /// </summary>
        /// <param name="model">Game Model.</param>
        /// <param name="bullet">Bullet.</param>
        /// <param name="commandManager">Command manager.</param>
        public BulletLogics(GameModel model, ICollection<Bullet> bullet, CommandManager commandManager)
        {
            this.model = model;
            if (bullet != null)
            {
                this.bullets = (List<Bullet>)bullet;
            }

            this.commandManager = commandManager;
        }

        /// <summary>
        /// Saves bullets into a string.
        /// </summary>
        /// <returns>A list of all bullets.</returns>
        public ICollection<string> SaveBullets()
        {
            List<string> bulletStrings = new List<string>();
            foreach (var item in this.bullets)
            {
                bulletStrings.Add(item.ToString());
            }

            return bulletStrings;
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
            if (this.commandManager.IsFinished)
            {
                for (int i = 0; i < this.bullets.Count; i++)
                {
                    this.Movement(this.bullets[i]);
                    if (this.DetectEntity(this.bullets[i]))
                    {
                        if (this.bullets[i].Type != BulletType.Basic)
                        {
                            this.Despawn(this.bullets[i], true);
                        }
                        else
                        {
                            this.Despawn(this.bullets[i]);
                        }
                    }
                    else
                    {
                        this.Despawn(this.bullets[i]);
                    }
                }
            }
        }

        private static void BasicMovement(Bullet bullet)
        {
            if (bullet.Type == BulletType.Accelerating || (Math.Abs(bullet.MoveVector.X) <= MaxBulletSpeed && Math.Abs(bullet.MoveVector.Y) <= MaxBulletSpeed))
            {
                bullet.MoveVector = new PointF(bullet.MoveVector.X + (bullet.MovementVectorF.X * bullet.Acceleration), bullet.MoveVector.Y + (bullet.MovementVectorF.Y * bullet.Acceleration));
            }
        }

        private static void AcceleratingMovement(Bullet bullet)
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
                case BulletType.Bouncing:
                    this.BouncingMovement(bullet);
                    break;
                case BulletType.Accelerating:
                    AcceleratingMovement(bullet);
                    break;
                case BulletType.CurvedBouncing:
                    this.CurvedMovement(bullet);
                    break;
                default:
                    BasicMovement(bullet);
                    break;
            }

            bullet.Position = new Point(bullet.Position.X + (int)bullet.MoveVector.X, bullet.Position.Y + (int)bullet.MoveVector.Y);
            MoveCommand cmd = new MoveCommand(bullet, bullet.Position, this.model);
            this.commandManager.AddCommand(cmd);
        }

        private void BouncingMovement(Bullet bullet)
        {
            Point nextMove = new Point(this.model.CurrentWorld.ConvertPixelToTile(bullet.Position.X + (int)bullet.MoveVector.X), this.model.CurrentWorld.ConvertPixelToTile(bullet.Position.Y));
            if (this.model.CurrentWorld.SearchGround(nextMove))
            {
                bullet.MoveVector = new PointF(bullet.MoveVector.X * -1, bullet.MoveVector.Y);
                bullet.MovementVectorF = new PointF(bullet.MovementVectorF.X * -1, bullet.MovementVectorF.Y);
            }

            nextMove = new Point(this.model.CurrentWorld.ConvertPixelToTile(bullet.Position.X), this.model.CurrentWorld.ConvertPixelToTile(bullet.Position.Y + (int)bullet.MoveVector.Y));
            if (this.model.CurrentWorld.SearchGround(nextMove))
            {
                bullet.MoveVector = new PointF(bullet.MoveVector.X, bullet.MoveVector.Y * -1);
                bullet.MovementVectorF = new PointF(bullet.MovementVectorF.X, bullet.MovementVectorF.Y * -1);
            }

            if (Math.Abs(bullet.MoveVector.X) <= MaxBulletSpeed && Math.Abs(bullet.MoveVector.Y) <= MaxBulletSpeed)
            {
                bullet.MoveVector = new PointF(bullet.MoveVector.X + (bullet.MovementVectorF.X * bullet.Acceleration), bullet.MoveVector.Y + (bullet.MovementVectorF.Y * bullet.Acceleration));
            }
        }

        private void CurvedMovement(Bullet bullet)
        {
            BasicMovement(bullet);
            this.BouncingMovement(bullet);
            bullet.MoveVector = new PointF(bullet.MoveVector.X, bullet.MoveVector.Y + 5);
            if (this.DetectGround(bullet))
            {
                BasicMovement(bullet);
                this.BouncingMovement(bullet);
            }
        }

        private bool DetectGround(Bullet bullet)
        {
            Point nextMove = new Point(this.model.CurrentWorld.ConvertPixelToTile(bullet.Position.X + (bullet.Width / 2) + (int)bullet.MoveVector.X), this.model.CurrentWorld.ConvertPixelToTile(bullet.Position.Y + (bullet.Height / 2) + (int)bullet.MoveVector.Y));

            return this.model.CurrentWorld.SearchGround(nextMove);
        }

        private bool DetectEntity(Bullet bullet)
        {
            Point bulletPos = new Point(bullet.Position.X + (bullet.Width / 2), bullet.Position.Y + (bullet.Width / 2));

            if (bullet.PlayerBullet)
            {
                foreach (Enemy enemy in this.model.CurrentWorld.GetEnemies)
                {
                    Rectangle enemyRect = new Rectangle(enemy.Position.X, enemy.Position.Y, this.model.CurrentWorld.ConvertTileToPixel(enemy.Width / this.model.CurrentWorld.TileSize), this.model.CurrentWorld.ConvertTileToPixel(enemy.Height / this.model.CurrentWorld.TileSize));

                    if (enemyRect.Contains(bulletPos))
                    {
                        enemy.CurrentHealth -= bullet.Damage;
                        return true;
                    }
                }
            }
            else
            {
                Rectangle playerRect = new Rectangle(this.model.Hero.Position.X, this.model.Hero.Position.Y, this.model.CurrentWorld.ConvertTileToPixel(this.model.Hero.Width / this.model.CurrentWorld.TileSize), this.model.CurrentWorld.ConvertTileToPixel(this.model.Hero.Height / this.model.CurrentWorld.TileSize));

                if (playerRect.Contains(bulletPos))
                {
                    int shieldValue = this.model.Hero.CurrentShield;
                    shieldValue -= bullet.Damage;

                    if (shieldValue < 0)
                    {
                        if (!this.model.Hero.IsInvincible)
                        {
                            this.model.Hero.CurrentShield = 0;
                            this.model.Hero.CurrentHealth += shieldValue;
                        }
                    }
                    else
                    {
                        this.model.Hero.CurrentShield -= bullet.Damage;
                    }

                    this.model.Hero.ShieldRegenTimer.Reset();
                    return true;
                }
            }

            return false;
        }

        private void Despawn(Bullet bullet, bool despawn = false)
        {
            if (despawn || (bullet.Position.X < 0 || bullet.Position.X > this.model.CurrentWorld.GameWidth) || (bullet.Position.Y < 0 || bullet.Position.Y > this.model.CurrentWorld.GameHeight) || (bullet.DespawnStopwatch.ElapsedMilliseconds > 5000) || (this.DetectGround(bullet) && !(bullet.Type == BulletType.Bouncing || bullet.Type == BulletType.CurvedBouncing)))
            {
                bullet.DespawnStopwatch.Stop();
                this.model.CurrentWorld.RemoveBullet(bullet);
            }
        }
    }
}
