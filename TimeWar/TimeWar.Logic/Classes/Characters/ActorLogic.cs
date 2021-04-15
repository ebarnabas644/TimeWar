// <copyright file="ActorLogic.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Logic.Classes.Characters
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TimeWar.Logic.Classes.Characters.Actions;
    using TimeWar.Model;
    using TimeWar.Model.Objects;

    /// <summary>
    /// Base class for characters and enemies.
    /// </summary>
    public abstract class ActorLogic
    {
        private int defaultAcceleration;
        private int maxMovementSpeed;
        private int maxJumpHeight;
        private GameModel model;
        private Character character;
        private CommandManager commandManager;
        private bool isJumping;
        private int acceleration;
        private Stopwatch accelerationStopwatch = new Stopwatch();
        private Stopwatch jumpingTimeOut = new Stopwatch();
        private Point moveVector;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActorLogic"/> class.
        /// </summary>
        /// <param name="model">Game model.</param>
        /// <param name="character">Character.</param>
        /// <param name="commandManager">Command manger.</param>
        protected ActorLogic(GameModel model, Character character, CommandManager commandManager)
        {
            this.model = model;
            this.character = character;
            this.commandManager = commandManager;
            this.isJumping = false;
            this.acceleration = this.defaultAcceleration;
            this.jumpingTimeOut.Start();
            this.moveVector = new Point(0, 0);
            this.defaultAcceleration = 1;
            this.maxMovementSpeed = 15;
            this.maxJumpHeight = 20;
        }

        /// <summary>
        /// Gets or sets movement vector of an actor.
        /// </summary>
        protected Point MoveVector
        {
            get { return this.moveVector; }
            set { this.moveVector = value; }
        }

        /// <summary>
        /// Gets or sets if a character can jump again.
        /// </summary>
        protected Stopwatch JumpingTimeOut
        {
            get { return this.jumpingTimeOut; }
            set { this.jumpingTimeOut = value; }
        }

        /// <summary>
        /// Gets or sets the time of falling for a character.
        /// </summary>
        protected Stopwatch AccelerationStopwatch
        {
            get { return this.accelerationStopwatch; }
            set { this.accelerationStopwatch = value; }
        }

        /// <summary>
        /// Gets or sets gravity acceleration.
        /// </summary>
        protected int Acceleration
        {
            get { return this.acceleration; }
            set { this.acceleration = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the actor is jumping.
        /// </summary>
        protected bool IsJumping
        {
            get { return this.isJumping; }
            set { this.isJumping = value; }
        }

        /// <summary>
        /// Gets or sets the character.
        /// </summary>
        protected Character Character
        {
            get { return this.character; }
            set { this.character = value; }
        }

        /// <summary>
        /// Gets or sets the force that is applied when an actor is jumping.
        /// </summary>
        protected int MaxJumpHeight
        {
            get { return this.maxJumpHeight; }
            set { this.maxJumpHeight = value; }
        }

        /// <summary>
        /// Gets or sets the force that is applied every tick when an actor is moving.
        /// </summary>
        protected int MaxMovementSpeed
        {
            get { return this.maxMovementSpeed; }
            set { this.maxMovementSpeed = value; }
        }

        /// <summary>
        /// Gets or sets the force that is applied every tick when a player is moving.
        /// </summary>
        protected int DefaultAcceleration
        {
            get { return this.defaultAcceleration; }
            set { this.defaultAcceleration = value; }
        }

        /// <summary>
        /// Gets or sets the game model.
        /// </summary>
        protected GameModel Model
        {
            get { return this.model; }
            set { this.model = value; }
        }

        /// <summary>
        /// Gets or sets the command manager.
        /// </summary>
        protected CommandManager CommandManager
        {
            get { return this.commandManager; }
            set { this.commandManager = value; }
        }

        /// <summary>
        /// One Tick.
        /// </summary>
        public virtual void OneTick()
        {
            this.Movement();
        }

        /// <summary>
        /// Converts pixel to tile values.
        /// </summary>
        /// <param name="num">Pixel value.</param>
        /// <returns>Tile value.</returns>
        protected int PixelToTile(int num)
        {
            return this.model.CurrentWorld.ConvertPixelToTile(num);
        }

        /// <summary>
        /// Converts tile to pixel values.
        /// </summary>
        /// <param name="num">Tile value.</param>
        /// <returns>Pixel value.</returns>
        protected int TileToPixel(int num)
        {
            return this.model.CurrentWorld.ConvertTileToPixel(num);
        }

        /// <summary>
        /// Ground collsiion.
        /// </summary>
        /// <param name="newPoint">New movement point.</param>
        /// <returns>True if the movemnt would collide.</returns>
        protected bool GroundCollision(Point newPoint)
        {
            if (!this.commandManager.IsFinished)
            {
                return false;
            }

            Rectangle actor = new Rectangle(
        this.character.Position.X + newPoint.X,
        this.character.Position.Y + newPoint.Y,
        this.character.Width / this.model.CurrentWorld.TileSize,
        this.character.Height / this.model.CurrentWorld.TileSize);
            Point actorLocation;

            for (int i = 0; i < actor.Width + 1; i++)
            {
                actorLocation = new Point(this.PixelToTile(actor.X + 1 + this.TileToPixel(i)), this.PixelToTile(actor.Y + this.moveVector.Y) + actor.Height);
                if (this.model.CurrentWorld.SearchGround(actorLocation))
                {
                    this.moveVector.Y = 0;
                    this.acceleration = this.defaultAcceleration;
                    this.accelerationStopwatch.Reset();
                    this.isJumping = false;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Top collsiion.
        /// </summary>
        /// <param name="newPoint">New movement point.</param>
        /// <returns>True if the movemnt would collide.</returns>
        protected bool TopCollision(Point newPoint)
        {
            if (!this.commandManager.IsFinished)
            {
                return false;
            }

            Rectangle actor = new Rectangle(
        this.character.Position.X + newPoint.X,
        this.character.Position.Y,
        this.character.Width / this.model.CurrentWorld.TileSize,
        this.character.Height / this.model.CurrentWorld.TileSize);
            Point actorLocation;

            for (int i = 0; i < actor.Width + 1; i++)
            {
                actorLocation = new Point(this.PixelToTile(actor.X + 1 + this.TileToPixel(i)), this.PixelToTile(actor.Y + this.moveVector.Y));
                if (this.model.CurrentWorld.SearchGround(actorLocation))
                {
                    this.moveVector.Y = 0;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Increases or decreases the movement vector.
        /// </summary>
        /// <param name="x">X direction.</param>
        /// <param name="y">Y direction.</param>
        protected void AddToVector(int x, int y)
        {
            this.moveVector.X += x;
            this.moveVector.Y += y;
        }

        /// <summary>
        /// Wall collsiion.
        /// </summary>
        /// <param name="newPoint">New movement point.</param>
        /// <param name="rightWall">True if you want to check right wall collision, false if you would like to check left wall.</param>
        /// <returns>True if the movemnt would collide.</returns>
        protected bool WallCollision(Point newPoint, bool rightWall = true)
        {
            if (!this.commandManager.IsFinished)
            {
                return false;
            }

            Rectangle actor = new Rectangle(
                this.character.Position.X + newPoint.X,
                this.character.Position.Y + newPoint.Y,
                this.character.Width / this.model.CurrentWorld.TileSize,
                this.character.Height / this.model.CurrentWorld.TileSize);
            Point actorLocation;

            if (rightWall)
            {
                // Right wall collision
                for (int i = 0; i < actor.Height; i++)
                {
                    actorLocation = new Point(this.PixelToTile(actor.X + 1 + this.moveVector.X) + actor.Width, this.PixelToTile(actor.Y) + i);
                    if (this.model.CurrentWorld.SearchGround(actorLocation))
                    {
                        this.moveVector.X = 0;
                        return true;
                    }
                }
            }
            else
            {
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
            }

            return false;
        }

        /// <summary>
        /// Default Movement funtion.
        /// </summary>
        protected virtual void Movement()
        {
            Point newPoint = this.Move();
            if (Math.Abs(this.MoveVector.X) < this.MaxMovementSpeed)
            {
                this.AddToVector(newPoint.X, 0);
            }

            this.AddToVector(0, newPoint.Y);

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
                this.AddToVector(-1, 0);
            }

            if (this.MoveVector.X < 0)
            {
                this.AddToVector(1, 0);
            }
        }

        /// <summary>
        /// Default move funtion.
        /// </summary>
        /// <returns>New movement point.</returns>
        protected virtual Point Move()
        {
            return new Point(0, 0);
        }
    }
}
