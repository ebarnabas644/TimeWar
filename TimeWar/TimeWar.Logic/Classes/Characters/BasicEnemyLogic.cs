// <copyright file="BasicEnemyLogic.cs" company="Time War">
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
        private const int MaxMoveTime = 3000;
        private const int DetectionTime = 20000;
        private const int DetectionRange = 20;
        private int attackTime = 2500;
        private Stopwatch movementDirStopwatch = new Stopwatch();
        private Stopwatch movementStopwatch = new Stopwatch();
        private int movementDirTime;
        private int movementTime;
        private int moveDir;
        private Point lastKnownPlayerLocation;
        private bool isPlayerDetected;
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
            this.movementDirTime = RandomNumberGenerator.GetInt32(MaxMoveTime);
            this.movementTime = RandomNumberGenerator.GetInt32(MaxMoveTime);
            this.moveDir = 0;
            this.lastKnownPlayerLocation = new Point(0, 0);
            this.isPlayerDetected = false;
            this.Character.Health = 75;
            this.AttackStopwatch.Start();
        }

        /// <inheritdoc/>
        public override void OneTick()
        {
            this.DetectPlayer();
            this.Attack();
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
                    this.moveDir = RandomNumberGenerator.GetInt32(3);
                    this.movementDirTime = RandomNumberGenerator.GetInt32(MaxMoveTime);
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
                if (this.playerDetectionStopwatch.ElapsedMilliseconds < DetectionTime)
                {
                    int distance = this.PlayerDistance();
                    if (this.PixelToTile(this.Character.Position.X) > this.lastKnownPlayerLocation.X && distance >= 9)
                    {
                        x -= 2;
                    }

                    if (this.PixelToTile(this.Character.Position.X) < this.lastKnownPlayerLocation.X && distance >= 9)
                    {
                        x += 2;
                    }

                    if (this.PixelToTile(this.Character.Position.Y) < this.lastKnownPlayerLocation.Y)
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
                this.movementTime = RandomNumberGenerator.GetInt32(MaxMoveTime);
            }
        }

        private int PlayerDistance()
        {
            return Math.Abs(this.PixelToTile(this.Character.Position.X) - this.lastKnownPlayerLocation.X);
        }

        private void DetectPlayer()
        {
            if (this.MoveVector.X < 0)
            {
                if (this.DetectionCone(false))
                {
                    this.playerDetectionStopwatch.Start();
                }
            }
            else if (this.MoveVector.X > 0)
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

                if (this.DetectionCone())
                {
                    this.playerDetectionStopwatch.Start();
                }
            }

            if (this.playerDetectionStopwatch.ElapsedMilliseconds > DetectionTime)
            {
                Debug.WriteLine("Player lost!");
                this.isPlayerDetected = false;
                this.playerDetectionStopwatch.Reset();
            }

            // Uncomment for debug (Draw Detection cones)
            // for (int i = 0; i < this.Model.CurrentWorld.GetBullets.Count; i++)
            // {
            //    this.Model.CurrentWorld.RemoveBullet(this.Model.CurrentWorld.GetBullet(i));
            // }
        }

        private bool DetectionCone(bool right = true, int range = DetectionRange)
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
                if (i % 1 == 0)
                {
                    height++;
                }

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
            Point playerLocation = new Point(this.PixelToTile(this.Model.Hero.Position.X), this.PixelToTile(this.Model.Hero.Position.Y));
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

                // Uncomment for debug (Draw Detection cones)
                // Bullet b = new Bullet(new Point(this.TileToPixel(downDetection.X), this.TileToPixel(downDetection.Y)), 2, 2, "testenemy.png");
                // Bullet c = new Bullet(new Point(this.TileToPixel(upDetection.X), this.TileToPixel(upDetection.Y)), 2, 2, "testenemy.png");
                // this.Model.CurrentWorld.AddBullet(b);
                // this.Model.CurrentWorld.AddBullet(c);
                if (playerLocation == upDetection)
                {
                    this.lastKnownPlayerLocation = upDetection;
                    this.isPlayerDetected = true;
                    Debug.WriteLine("Player detected! " + this.lastKnownPlayerLocation);
                    return true;
                }
                else if (playerLocation == downDetection)
                {
                    this.lastKnownPlayerLocation = downDetection;
                    this.isPlayerDetected = true;
                    Debug.WriteLine("Player detected! " + this.lastKnownPlayerLocation);
                    return true;
                }
            }

            return false;
        }
    }
}
