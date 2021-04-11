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
    using TimeWar.Logic.Classes.Characters.Actions;
    using TimeWar.Model;
    using TimeWar.Model.Objects;
    using TimeWar.Model.Objects.Interfaces;

    /// <summary>
    /// Basic character logic class.
    /// </summary>
    public class CharacterLogic
    {
        private const int DefaultAcceleration = 1;
        private const int MaxMovementSpeed = 15;
        private const int MaxJumpHeight = 20;

        private GameModel model;
        private Character character;
        private CommandManager commandManager;
        private bool isJumping;
        private int acceleration;
        private Stopwatch accelerationStopwatch = new Stopwatch();
        private Stopwatch jumpingTimeOut = new Stopwatch();
        private Point moveVector;

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterLogic"/> class.
        /// </summary>
        /// <param name="model">Game model entity.</param>
        /// <param name="character">Moveable entity.</param>
        /// <param name="commandManager">Command manager entity.</param>
        public CharacterLogic(GameModel model, Character character, CommandManager commandManager)
        {
            this.model = model;
            this.character = character;
            this.commandManager = commandManager;
            this.isJumping = false;
            this.acceleration = DefaultAcceleration;
            this.jumpingTimeOut.Start();
            this.moveVector = new Point(0, 0);
        }

        /// <summary>
        /// 1 frame event.
        /// </summary>
        public void OneTick()
        {
            Point newPoint = this.Move();
            if (Math.Abs(this.moveVector.X) < MaxMovementSpeed)
            {
                this.moveVector.X += newPoint.X;
            }

            this.moveVector.Y += newPoint.Y;

            if (this.MovementCollision(new Point(0, newPoint.Y)))
            {
                while (this.MovementCollision(new Point(0, newPoint.Y)))
                {
                    this.character.Position = new Point(this.character.Position.X, this.character.Position.Y - 1);
                }
            }

            if (!this.MovementCollision(new Point(0, this.acceleration)))
            {
                this.moveVector.Y += this.acceleration;

                if ((!this.MovementCollision(new Point(0, this.acceleration + 1)) && this.acceleration < 10) && !this.accelerationStopwatch.IsRunning)
                {
                    this.accelerationStopwatch.Start();
                }

                if (this.accelerationStopwatch.ElapsedMilliseconds > 100)
                {
                    this.acceleration++;
                    this.accelerationStopwatch.Restart();
                }
            }

            if (!this.MovementCollision(newPoint))
            {
                this.character.Position = new Point(this.character.Position.X + this.moveVector.X, this.character.Position.Y + this.moveVector.Y);
            }

            if (this.moveVector.X > 0)
            {
                this.moveVector.X--;
            }

            if (this.moveVector.X < 0)
            {
                this.moveVector.X++;
            }
        }

        /*
        /// <summary>
        /// 1 frame event.
        /// </summary>
        public void OneTick()
        {
            bool commandAdded = false;
            Point newPoint = this.Move();

            if (!this.MovementCollision(newPoint))
            {
                MoveCommand command = new MoveCommand(this.character, newPoint, this.model);
                command.Execute();
                if (newPoint.Y == -1)
                {
                    int counter = 0;
                    while (counter < 14 && !this.MovementCollision(newPoint))
                    {
                        command = new MoveCommand(this.character, newPoint, this.model);
                        command.Execute();
                        counter++;
                        this.commandManager.AddCommand(command);
                    }

                    commandAdded = true;
                }

                if (!this.MovementCollision(new Point(0, this.acceleration)))
                {
                    MoveCommand gravity = new MoveCommand(this.character, new Point(0, this.acceleration), this.model);
                    gravity.Execute();
                    this.commandManager.AddCommand(gravity);
                    if ((!this.MovementCollision(new Point(0, this.acceleration + 1)) && this.acceleration < 3) && !this.accelerationStopwatch.IsRunning)
                    {
                        this.accelerationStopwatch.Start();
                    }

                    if (this.accelerationStopwatch.ElapsedMilliseconds > 100)
                    {
                        this.acceleration++;
                        this.accelerationStopwatch.Restart();
                    }
                }

                if (!commandAdded)
                {
                    this.commandManager.AddCommand(command);
                }

                if (this.MovementCollision(new Point(0, 0)))
                {
                    while (this.MovementCollision(new Point(0, 0)))
                    {
                        this.character.Position = new Point(this.character.Position.X, this.character.Position.Y - 1);
                    }
                }
            }
        }

        private bool MovementCollision(Point newPoint)
        {
            Rectangle actor = new Rectangle(
                this.character.Position.X + newPoint.X,
                this.character.Position.Y + newPoint.Y,
                this.character.Width / this.model.CurrentWorld.TileSize,
                this.character.Height / this.model.CurrentWorld.TileSize);
            Point actorLocation;

            // Ground collision
            for (int i = 0; i < actor.Width; i++)
            {
                actorLocation = new Point(this.PixelToTile(actor.X + 1) + i, this.PixelToTile(actor.Y) + actor.Height); // itt
                if (this.model.CurrentWorld.SearchGround(actorLocation))
                {
                    this.acceleration = DefaultAcceleration;
                    this.accelerationStopwatch.Reset();
                    this.isJumping = false;
                    return true;
                }
            }

            // Top collision
            for (int i = 0; i < actor.Width; i++)
            {
                actorLocation = new Point(this.PixelToTile(actor.X) + i, this.PixelToTile(actor.Y));
                if (this.model.CurrentWorld.SearchGround(actorLocation))
                {
                    return true;
                }
            }

            // Right wall collision
            for (int i = 0; i < actor.Height; i++)
            {
                actorLocation = new Point(this.PixelToTile(actor.X - 1) + actor.Width, this.PixelToTile(actor.Y) + i);
                if (this.model.CurrentWorld.SearchGround(actorLocation))
                {
                    return true;
                }
            }

            // Left wall collision
            for (int i = 0; i < actor.Height; i++)
            {
                actorLocation = new Point(this.PixelToTile(actor.X), this.PixelToTile(actor.Y) + i);
                if (this.model.CurrentWorld.SearchGround(actorLocation))
                {
                    return true;
                }
            }

            return false;
        }

        private int PixelToTile(int num)
        {
            return this.model.CurrentWorld.ConvertPixelToTile(num);
        }

        private int TileToPixel(int num)
        {
            return this.model.CurrentWorld.ConvertTileToPixel(num);
        }
    }
        */

        private Point Move()
        {
            int x = 0;
            int y = 0;
            if (this.character.ContainKey("a"))
            {
                x -= 2;
                Debug.Write("a");
            }

            if (this.character.ContainKey("d"))
            {
                x += 2;
            }

            if (this.character.ContainKey("s"))
            {
                y += 1;
            }

            if (this.character.ContainKey("space"))
            {
                if (!this.isJumping && this.jumpingTimeOut.ElapsedMilliseconds > 250)
                {
                    this.jumpingTimeOut.Restart();
                    this.isJumping = true;
                    this.accelerationStopwatch.Start();
                    y = -MaxJumpHeight;
                }
            }

            // switch (this.character.Direction)
            // {
            //    case Stances.StandRight:
            //        direction = new Point(0, 0);
            //        lastX = direction.X;
            //        break;
            //    case Stances.Right:
            //        direction = new Point(2, 0);
            //        lastX = direction.X;
            //        break;
            //    case Stances.Left:
            //        direction = new Point(-2, 0);
            //        lastX = direction.X;
            //        break;
            //    case Stances.Up:
            //        direction = new Point(0, 0);
            //        if (!this.isJumping && this.jumpingTimeOut.ElapsedMilliseconds > 250)
            //        {
            //            this.jumpingTimeOut.Restart();
            //            this.isJumping = true;
            //            this.accelerationStopwatch.Start();
            //            direction = new Point(lastX, -MaxJumpHeight);
            //        }

            // break;
            //    case Stances.Down:
            //        direction = new Point(0, 1);
            //        break;
            //    default:
            //        direction = new Point(0, 0);
            //        break;
            // }
            return new Point(x, y);
        }

        private bool MovementCollision(Point newPoint)
        {
            Rectangle actor = new Rectangle(
                this.character.Position.X + newPoint.X,
                this.character.Position.Y + newPoint.Y,
                this.character.Width / this.model.CurrentWorld.TileSize,
                this.character.Height / this.model.CurrentWorld.TileSize);
            Point actorLocation;

            // Ground collision
            for (int i = 0; i < actor.Width + 1; i++)
            {
                actorLocation = new Point(this.PixelToTile(actor.X + 1) + i, this.PixelToTile(actor.Y + this.moveVector.Y) + actor.Height); // itt
                if (this.model.CurrentWorld.SearchGround(actorLocation))
                {
                    this.moveVector.Y = 0;
                    this.acceleration = DefaultAcceleration;
                    this.accelerationStopwatch.Reset();
                    this.isJumping = false;
                    return true;
                }
            }

            // Top collision
            for (int i = 0; i < actor.Width + 1; i++)
            {
                actorLocation = new Point(this.PixelToTile(actor.X) + i, this.PixelToTile(actor.Y + this.moveVector.Y));
                if (this.model.CurrentWorld.SearchGround(actorLocation))
                {
                    this.moveVector.Y = 0;
                    return true;
                }
            }

            // Right wall collision
            for (int i = 0; i < actor.Height; i++)
            {
                actorLocation = new Point(this.PixelToTile(actor.X + this.moveVector.X) + actor.Width, this.PixelToTile(actor.Y) + i);
                if (this.model.CurrentWorld.SearchGround(actorLocation))
                {
                    this.moveVector.X = 0;
                    return true;
                }
            }

            // Left wall collision
            for (int i = 0; i < actor.Height; i++)
            {
                actorLocation = new Point(this.PixelToTile(actor.X + this.moveVector.X), this.PixelToTile(actor.Y) + i);
                if (this.model.CurrentWorld.SearchGround(actorLocation))
                {
                    this.moveVector.X = 0;
                    return true;
                }
            }

            return false;
        }

        private int PixelToTile(int num)
        {
            return this.model.CurrentWorld.ConvertPixelToTile(num);
        }
    }
}
