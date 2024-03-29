﻿// <copyright file="BasicEnemyLogic.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Logic.Classes.Characters
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using TimeWar.Logic.Classes.Characters.Actions;
    using TimeWar.Model;
    using TimeWar.Model.Objects;
    using TimeWar.Model.Objects.Classes;

    /// <summary>
    /// Enemy logic.
    /// </summary>
    public class BasicEnemyLogic : ActorLogic
    {
        private int followDistance = 9;
        private Stopwatch movementDirStopwatch = new Stopwatch();
        private Stopwatch movementStopwatch = new Stopwatch();
        private int movementDirTime;
        private int movementTime;
        private int moveDir;
        private bool isPlayerVisible;
        private Stopwatch playerDetectionStopwatch = new Stopwatch();

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicEnemyLogic"/> class.
        /// </summary>
        /// <param name="model">Game model.</param>
        /// <param name="character">Charater.</param>
        /// <param name="commandManager">Command manger.</param>
        public BasicEnemyLogic(GameModel model, Character character, CommandManager commandManager)
            : base(model, character, commandManager)
        {
            this.movementDirStopwatch.Start();
            this.movementStopwatch.Start();
            this.moveDir = 0;
            this.AttackTime = 1500;
            this.MaxMoveTime = 1000;
            this.DetectionTime = 20000;
            this.DetectionRange = 20;
            this.movementDirTime = RandomNumberGenerator.GetInt32(this.MaxMoveTime);
            this.movementTime = RandomNumberGenerator.GetInt32(this.MaxMoveTime);
            this.DefaultFollowDistance = RandomNumberGenerator.GetInt32(5, 11);
            this.LastKnownPlayerLocation = new Point(0, 0);
            this.IsPlayerDetected = false;
            this.isPlayerVisible = false;
            this.Character.Health = 75;
            this.Character.CanAttack = true;
            this.AttackStopwatch.Start();
            this.TypeOfBullet = BulletType.BasicEnemyBullet;
            this.AttackValue = 20;
        }

        /// <summary>
        /// Gets or sets the attack damage of the enemy.
        /// </summary>
        public int AttackValue { get; set; }

        /// <summary>
        /// Gets or sets last known player location.
        /// </summary>
        public Point LastKnownPlayerLocation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether player detected.
        /// </summary>
        public bool IsPlayerDetected { get; set; }

        /// <summary>
        /// Gets or sets attack time.
        /// </summary>
        protected int AttackTime { get; set; }

        /// <summary>
        /// Gets or sets max move time.
        /// </summary>
        protected int MaxMoveTime { get; set; }

        /// <summary>
        /// Gets or sets max move time.
        /// </summary>
        protected int DetectionTime { get; set; }

        /// <summary>
        /// Gets or sets detection range.
        /// </summary>
        protected int DetectionRange { get; set; }

        /// <summary>
        /// Gets or sets default follow distance.
        /// </summary>
        protected int DefaultFollowDistance { get; set; }

        /// <inheritdoc/>
        public override void OneTick()
        {
            if (this.CommandManager.IsFinished)
            {
                if (this.PlayerIsDetectable())
                {
                    this.DetectPlayer();
                }

                this.Attack();
                base.OneTick();
            }
        }

        /// <inheritdoc/>
        protected override Point Move()
        {
            int x = 0;
            int y = 0;
            if (!this.IsPlayerDetected)
            {
                if (this.movementDirStopwatch.ElapsedMilliseconds > this.movementDirTime)
                {
                    this.moveDir = RandomNumberGenerator.GetInt32(3);
                    this.movementDirTime = RandomNumberGenerator.GetInt32(this.MaxMoveTime);
                    this.movementDirStopwatch.Restart();
                }

                switch (this.moveDir)
                {
                    case 0:
                        x -= 2;
                        break;

                    case 1:
                        x += 2;
                        break;

                    case 2:
                        x += 0;
                        break;

                    default:
                        x += 0;
                        break;
                }
            }
            else
            {
                if (this.playerDetectionStopwatch.ElapsedMilliseconds < this.DetectionTime)
                {
                    int distance = this.PlayerDistance();
                    if (!this.isPlayerVisible)
                    {
                        this.followDistance = 0;
                    }
                    else
                    {
                        this.followDistance = this.DefaultFollowDistance;
                    }

                    if (this.PixelToTile(this.Character.Position.X) > this.LastKnownPlayerLocation.X && distance >= this.followDistance)
                    {
                        x -= 2;
                    }

                    if (this.PixelToTile(this.Character.Position.X) < this.LastKnownPlayerLocation.X && distance >= this.followDistance)
                    {
                        x += 2;
                    }

                    if (this.PixelToTile(this.Character.Position.Y) < this.LastKnownPlayerLocation.Y)
                    {
                        y += this.Jump();
                    }
                }
                else
                {
                    this.moveDir = RandomNumberGenerator.GetInt32(4);
                    this.movementDirStopwatch.Restart();
                }
            }

            int rnd = RandomNumberGenerator.GetInt32(10000);
            if (rnd < 100)
            {
                y += this.Jump();
            }

            return new Point(x, y);
        }

        /// <inheritdoc/>
        protected override void Attack()
        {
            if (this.Model.CurrentWorld.GetBullets.Count < ushort.MaxValue)
            {
                int inaccuracy = RandomNumberGenerator.GetInt32(-75, 76);
                if (this.IsPlayerDetected && this.CommandManager.IsFinished && this.Character.CanAttack && this.AttackStopwatch.ElapsedMilliseconds > this.AttackTime)
                {
                    Point attackPoint = new Point(this.Character.Position.X + this.Model.CurrentWorld.ConvertTileToPixel(1), this.Character.Position.Y + this.Model.CurrentWorld.ConvertTileToPixel(1));

                    Bullet b = new Bullet(attackPoint, 4, 4, "testenemy.png", new Point(this.TileToPixel(this.LastKnownPlayerLocation.X), this.TileToPixel(this.LastKnownPlayerLocation.Y + 2) - inaccuracy), this.AttackValue, this.TypeOfBullet);
                    this.Model.CurrentWorld.AddBullet(b);
                    this.AttackStopwatch.Restart();
                }
            }
        }

        /// <inheritdoc/>
        protected override void Movement()
        {
            if (this.movementStopwatch.ElapsedMilliseconds < this.movementTime)
            {
                base.Movement();
            }
            else
            {
                this.movementStopwatch.Restart();
                this.movementTime = RandomNumberGenerator.GetInt32(this.MaxMoveTime);
            }
        }

        private int PlayerDistance()
        {
            return Math.Abs(this.PixelToTile(this.Character.Position.X) - this.LastKnownPlayerLocation.X);
        }

        private void DetectPlayer()
        {
            if (this.Character.MovementVector.X < 0)
            {
                if (this.DetectionCone(false))
                {
                    this.playerDetectionStopwatch.Start();
                }
            }
            else if (this.Character.MovementVector.X > 0)
            {
                if (this.DetectionCone())
                {
                    this.playerDetectionStopwatch.Start();
                }
            }
            else
            {
                if (this.DetectionCone(false))
                {
                    this.playerDetectionStopwatch.Start();
                }
                else if (this.DetectionCone())
                {
                    this.playerDetectionStopwatch.Start();
                }
                else
                {
                    this.isPlayerVisible = false;
                }
            }

            if (this.playerDetectionStopwatch.ElapsedMilliseconds > this.DetectionTime)
            {
                this.IsPlayerDetected = false;
                this.playerDetectionStopwatch.Reset();
            }
        }

        private bool DetectionCone(bool right = true)
        {
            int dir = -1;
            int height = 1;
            Point startPoint = new Point(this.PixelToTile(this.Character.Position.X) + 1, this.PixelToTile(this.Character.Position.Y));
            if (right)
            {
                dir = 1;
                startPoint.X++;
            }

            for (int i = 0; i < this.DetectionRange; i++)
            {
                height++;

                if (this.Model.CurrentWorld.SearchGround(startPoint))
                {
                    return false;
                }

                if (this.DetectionSpike(startPoint, height))
                {
                    return true;
                }

                startPoint.X += dir;
            }

            return false;
        }

        private bool DetectionSpike(Point starterPoint, int range)
        {
            Point playerLocation = new Point(this.PixelToTile(this.Model.Hero.Position.X) + 1, this.PixelToTile(this.Model.Hero.Position.Y));
            int upRange = 0;
            int downRange = 1;

            for (int i = 0; i < range + 1; i++)
            {
                Point upDetection = new Point(starterPoint.X, starterPoint.Y + upRange);
                Point downDetection = new Point(starterPoint.X, starterPoint.Y - downRange);

                if (!this.Model.CurrentWorld.SearchGround(upDetection))
                {
                    upRange++;
                }

                if (!this.Model.CurrentWorld.SearchGround(downDetection))
                {
                    downRange++;
                }

                if (playerLocation == upDetection)
                {
                    this.LastKnownPlayerLocation = upDetection;
                    this.IsPlayerDetected = true;
                    this.isPlayerVisible = true;
                    return true;
                }
                else if (playerLocation == downDetection)
                {
                    this.LastKnownPlayerLocation = downDetection;
                    this.IsPlayerDetected = true;
                    this.isPlayerVisible = true;
                    return true;
                }
            }

            return false;
        }

        private bool PlayerIsDetectable()
        {
            int distance = this.Model.CurrentWorld.ConvertPixelToTile((int)Math.Sqrt(Math.Pow(this.Character.Position.X - this.Model.Hero.Position.X, 2) + Math.Pow(this.Character.Position.Y - this.Model.Hero.Position.Y, 2)));

            if (distance < this.DetectionRange + 5)
            {
                return true;
            }

            return false;
        }
    }
}
