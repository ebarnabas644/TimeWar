// <copyright file="CharacterLogic.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using TimeWar.Logic.Classes;
    using TimeWar.Logic.Classes.Characters;
    using TimeWar.Logic.Classes.Characters.Actions;
    using TimeWar.Model;
    using TimeWar.Model.Objects;
    using TimeWar.Model.Objects.Interfaces;

    /// <summary>
    /// Basic character logic class.
    /// </summary>
    public class CharacterLogic : ActorLogic
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterLogic"/> class.
        /// </summary>
        /// <param name="model">Game model entity.</param>
        /// <param name="character">Moveable entity.</param>
        /// <param name="commandManager">Command manager entity.</param>
        public CharacterLogic(GameModel model, Character character, CommandManager commandManager)
            : base(model, character, commandManager)
        {
        }

        /// <inheritdoc/>
        protected override void Movement()
        {
            Point newPoint = this.Move();
            if (Math.Abs(this.MoveVector.X) < this.MaxMovementSpeed)
            {
                // this.MoveVector.X += newPoint.X;
                this.AddToVector(newPoint.X, 0);
            }

            this.AddToVector(0, newPoint.Y);

            // this.MoveVector.Y += newPoint.Y;
            if (this.GroundCollision(new Point(0, newPoint.Y)))
            {
                if (this.CommandManager.IsFinished)
                {
                    while (this.GroundCollision(new Point(0, newPoint.Y)))
                    {
                        this.Character.Position = new Point(this.Character.Position.X, this.Character.Position.Y - 1);
                    }
                }
            }

            if (!this.GroundCollision(new Point(0, this.Acceleration)))
            {
                if (this.CommandManager.IsFinished)
                {
                    // this.MoveVector.Y += this.Acceleration;
                    this.AddToVector(0, this.Acceleration);

                    if ((!this.GroundCollision(new Point(0, this.Acceleration + 1)) && this.Acceleration < 10) && !this.AccelerationStopwatch.IsRunning)
                    {
                        this.AccelerationStopwatch.Start();
                    }

                    if (this.AccelerationStopwatch.ElapsedMilliseconds > 100)
                    {
                        this.Acceleration++;
                        this.AccelerationStopwatch.Restart();
                    }
                }
            }

            if (!this.WallCollision(newPoint) && !this.WallCollision(newPoint, false))
            {
                this.Character.Position = new Point(this.Character.Position.X + this.MoveVector.X, this.Character.Position.Y);
            }

            if (!this.GroundCollision(newPoint) && (!this.WallCollision(newPoint) && !this.WallCollision(newPoint, false)))
            {
                if (!this.TopCollision(newPoint))
                {
                    this.Character.Position = new Point(this.Character.Position.X, this.Character.Position.Y + this.MoveVector.Y);
                }
            }

            MoveCommand moveCommand = new MoveCommand(this.Character, this.Character.Position, this.Model);
            this.CommandManager.AddCommand(moveCommand);

            if (this.MoveVector.X > 0)
            {
                // this.MoveVector.X--;
                this.AddToVector(-1, 0);
            }

            if (this.MoveVector.X < 0)
            {
                // this.MoveVector.X++;
                this.AddToVector(1, 0);
            }
        }

        /// <inheritdoc/>
        protected override Point Move()
        {
            int x = 0;
            int y = 0;
            if (this.Character.ContainKey("a"))
            {
                x -= 2;
            }

            if (this.Character.ContainKey("d"))
            {
                x += 2;
            }

            if (this.Character.ContainKey("s"))
            {
                y += 1;
            }

            if (this.Character.ContainKey("space"))
            {
                if (!this.IsJumping && this.JumpingTimeOut.ElapsedMilliseconds > 250)
                {
                    this.JumpingTimeOut.Restart();
                    this.IsJumping = true;
                    this.AccelerationStopwatch.Start();
                    if (Math.Abs(this.MoveVector.X) >= 15)
                    {
                        y -= this.MaxJumpHeight + 2;
                    }
                    else
                    {
                        y -= this.MaxJumpHeight;
                    }
                }
            }

            return new Point(x, y);
        }
    }
}