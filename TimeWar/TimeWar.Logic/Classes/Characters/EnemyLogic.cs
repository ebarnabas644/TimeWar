// <copyright file="EnemyLogic.cs" company="Time War">
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

    /// <summary>
    /// Enemy logic.
    /// </summary>
    public class EnemyLogic : ActorLogic
    {
        private const int DetectionTime = 60;
        private Stopwatch movementDirStopwatch;
        private Stopwatch movementStopwatch;
        private int movementDirTime;
        private int movementTime;
        private int moveDir;
        private Point lastKnownPlayerLocation;
        private bool isPlayerDetected;
        private Stopwatch playerDetectionTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnemyLogic"/> class.
        /// </summary>
        /// <param name="model">Game model.</param>
        /// <param name="character">Charater.</param>
        /// <param name="commandManager">Command manger.</param>
        public EnemyLogic(GameModel model, Character character, CommandManager commandManager)
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
            this.playerDetectionTime = new Stopwatch();
        }

        /// <inheritdoc/>
        public override void OneTick()
        {
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
                        if (!this.IsJumping && this.JumpingTimeOut.ElapsedMilliseconds > 250)
                        {
                            this.JumpingTimeOut.Restart();
                            this.IsJumping = true;
                            this.AccelerationStopwatch.Start();
                            this.moveDir = RandomNumberGenerator.GetInt32(4);
                            this.movementDirStopwatch.Restart();
                            y -= this.MaxJumpHeight;
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

                //lastKnownPlayerLocation.Y;
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
            throw new NotImplementedException();
        }
    }
}
