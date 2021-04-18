// <copyright file="BasicEnemyLogic.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Logic.Classes.Characters
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using TimeWar.Model;
    using TimeWar.Model.Objects;
    using TimeWar.Model.Objects.Classes;

    /// <summary>
    /// Enemy logic.
    /// </summary>
    public class BasicEnemyLogic : ActorLogic
    {
        private const int DetectionTime = 60;
        private Stopwatch movementDirStopwatch;
        private Stopwatch movementStopwatch;
        private int movementDirTime;
        private int movementTime;
        private int moveDir;
        private Point lastKnownPlayerLocation;
        private bool isPlayerDetected;
        private Stopwatch playerDetectionStopwatch;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicEnemyLogic"/> class.
        /// </summary>
        /// <param name="model">Game model.</param>
        /// <param name="character">Charater.</param>
        /// <param name="commandManager">Command manger.</param>
        public BasicEnemyLogic(GameModel model, Character character, CommandManager commandManager)
            : base(model, character, commandManager)
        {
            this.movementDirStopwatch = new Stopwatch();
            this.movementDirStopwatch.Start();
            this.movementStopwatch = new Stopwatch();
            this.movementDirTime = RandomNumberGenerator.GetInt32(7000);
            this.movementTime = RandomNumberGenerator.GetInt32(7000);
            this.moveDir = 0;
            this.lastKnownPlayerLocation = new Point(0, 0);
            this.isPlayerDetected = false;
            this.playerDetectionStopwatch = new Stopwatch();
            this.Character.Health = 75;
        }

        /// <inheritdoc/>
        public override void OneTick()
        {
            this.DetectPlayer();
            base.OneTick();
        }

        /// <inheritdoc/>
        protected override Point Move()
        {
            int x = 0;
            int y = 0;
            if (!this.isPlayerDetected)
            {
                if (this.movementDirStopwatch.ElapsedMilliseconds > this.movementDirTime)
                {
                    this.moveDir = RandomNumberGenerator.GetInt32(4);
                    this.movementDirTime = RandomNumberGenerator.GetInt32(7000);
                    this.movementDirStopwatch.Restart();
                }

                switch (this.moveDir)
                {
                    case 0:
                        x -= 2;
                        break;

                    case 1:
                        y += this.Jump();
                        if (y != 0)
                        {
                            this.moveDir = RandomNumberGenerator.GetInt32(4);
                            this.movementDirStopwatch.Restart();
                        }

                        break;

                    case 2:
                        x += 2;
                        break;

                    case 3:
                        x += 0;
                        break;

                    default:
                        x += 0;
                        break;
                }
            }
            else
            {
                if (this.playerDetectionStopwatch.ElapsedMilliseconds < DetectionTime * 1000)
                {
                    x = 0;
                    y = 0;
                    if (this.Character.Position.X > this.lastKnownPlayerLocation.X)
                    {
                        x -= 2;
                    }

                    if (this.Character.Position.X < this.lastKnownPlayerLocation.X)
                    {
                        x += 2;
                    }

                    if (this.Character.Position.Y < this.lastKnownPlayerLocation.Y)
                    {
                        y += this.Jump();
                    }

                    if (RandomNumberGenerator.GetInt32(100) > 80)
                    {
                        y += this.Jump();
                    }
                }
                else
                {
                    this.isPlayerDetected = false;
                    this.moveDir = RandomNumberGenerator.GetInt32(4);
                    this.movementDirStopwatch.Restart();
                }

                // lastKnownPlayerLocation.Y;
            }

            return new Point(x, y);
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
                this.movementStopwatch.Reset();
                this.movementTime = RandomNumberGenerator.GetInt32(7000);
            }
        }

        private void DetectPlayer()
        {
            if (this.Character.Direction == Stances.StandLeft || this.Character.Direction == Stances.Left)
            {
                if (this.DetectionCone(false))
                {
                    this.playerDetectionStopwatch.Start();
                }
            }
            else if (this.Character.Direction == Stances.Right || this.Character.Direction == Stances.Right)
            {
                if (this.DetectionCone())
                {
                    this.playerDetectionStopwatch.Start();
                }
            }
        }

        private bool DetectionCone(bool right = true, int range = 15)
        {
            int dir = -1;
            int height = 1;
            Point startPoint = new Point(this.PixelToTile(this.Character.Position.X), this.PixelToTile(this.Character.Position.Y));
            if (right)
            {
                dir = 1;
                startPoint.X++;
            }

            for (int i = 0; i < range; i++)
            {
                if (i % 2 == 0)
                {
                    height++;
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
            Point playerLocation = new Point(this.PixelToTile(this.Model.Hero.Position.X), this.PixelToTile(this.Model.Hero.Position.Y));

            for (int i = 0; i < range + 1; i++)
            {
                Point upDetection = new Point(starterPoint.X + i, starterPoint.Y);
                Point downDetection = new Point(starterPoint.X - i, starterPoint.Y);
                if (playerLocation == upDetection)
                {
                    this.lastKnownPlayerLocation = upDetection;
                    return true;
                }
                else if (playerLocation == downDetection)
                {
                    this.lastKnownPlayerLocation = downDetection;
                    return true;
                }
            }

            return false;
        }
    }
}
