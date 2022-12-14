// <copyright file="ActorLogic.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Logic.Classes.Characters
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using TimeWar.Logic.Classes.Characters.Actions;
    using TimeWar.Model;
    using TimeWar.Model.Objects;
    using TimeWar.Model.Objects.Classes;

    /// <summary>
    /// Base class for characters and enemies.
    /// </summary>
    public abstract class ActorLogic
    {
        private const int GravityAcceleration = 5;
        private const int JumpTimer = 250;
        private Stopwatch accelerationStopwatch = new Stopwatch();
        private Stopwatch jumpingTimeOut = new Stopwatch();
        private Stopwatch attackStopwatch = new Stopwatch();

        /// <summary>
        /// Initializes a new instance of the <see cref="ActorLogic"/> class.
        /// </summary>
        /// <param name="model">Game model.</param>
        /// <param name="character">Character.</param>
        /// <param name="commandManager">Command manger.</param>
        protected ActorLogic(GameModel model, Character character, CommandManager commandManager)
        {
            this.Model = model;
            this.Character = character;
            this.CommandManager = commandManager;
            this.DefaultJumpHeight = 20;
            this.IsJumping = false;
            this.Acceleration = this.DefaultAcceleration;
            this.jumpingTimeOut.Start();
            this.DefaultAcceleration = 1;
            this.MaxMovementSpeed = 15;
            this.MaxJumpHeight = this.DefaultJumpHeight;
            this.TypeOfBullet = BulletType.Basic;
            this.Character.MovementVector = new Point(0, 0);
        }

        /// <summary>
        /// Gets or sets default jump height.
        /// </summary>
        public int DefaultJumpHeight { get; set; }

        /// <summary>
        /// Gets the character.
        /// </summary>
        public Character Character { get; private set; }

        /// <summary>
        /// Gets or sets the force that is applied when an actor is jumping.
        /// </summary>
        public int MaxJumpHeight { get; set; }

        /// <summary>
        /// Gets or sets bullet type.
        /// </summary>
        protected BulletType TypeOfBullet { get; set; }

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
        /// Gets or sets the attack stopwatch.
        /// </summary>
        protected Stopwatch AttackStopwatch
        {
            get { return this.attackStopwatch; }
            set { this.attackStopwatch = value; }
        }

        /// <summary>
        /// Gets or sets gravity acceleration.
        /// </summary>
        protected int Acceleration { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the actor is jumping.
        /// </summary>
        protected bool IsJumping { get; set; }

        /// <summary>
        /// Gets or sets the force that is applied every tick when an actor is moving.
        /// </summary>
        protected int MaxMovementSpeed { get; set; }

        /// <summary>
        /// Gets or sets the force that is applied every tick when a player is moving.
        /// </summary>
        protected int DefaultAcceleration { get; set; }

        /// <summary>
        /// Gets or sets the game model.
        /// </summary>
        protected GameModel Model { get; set; }

        /// <summary>
        /// Gets or sets the command manager.
        /// </summary>
        protected CommandManager CommandManager { get; set; }

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
            return this.Model.CurrentWorld.ConvertPixelToTile(num);
        }

        /// <summary>
        /// Converts tile to pixel values.
        /// </summary>
        /// <param name="num">Tile value.</param>
        /// <returns>Pixel value.</returns>
        protected int TileToPixel(int num)
        {
            return this.Model.CurrentWorld.ConvertTileToPixel(num);
        }

        /// <summary>
        /// Ground collsiion.
        /// </summary>
        /// <param name="newPoint">New movement point.</param>
        /// <returns>True if the movemnt would collide.</returns>
        protected bool GroundCollision(Point newPoint)
        {
            if (!this.CommandManager.IsFinished)
            {
                return false;
            }

            Rectangle actor = new Rectangle(
        this.Character.Position.X + newPoint.X,
        this.Character.Position.Y + newPoint.Y,
        this.Character.Width / this.Model.CurrentWorld.TileSize,
        this.Character.Height / this.Model.CurrentWorld.TileSize);
            Point actorLocation;

            for (int i = 0; i < actor.Width + 1; i++)
            {
                actorLocation = new Point(this.PixelToTile(actor.X + 1 + this.TileToPixel(i)), this.PixelToTile(actor.Y + this.Character.MovementVector.Y) + actor.Height);
                if (this.Model.CurrentWorld.SearchGround(actorLocation))
                {
                    this.SetVectorY(0);
                    this.Acceleration = this.DefaultAcceleration;
                    this.AccelerationStopwatch.Reset();
                    this.IsJumping = false;
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
            if (!this.CommandManager.IsFinished)
            {
                return false;
            }

            Rectangle actor = new Rectangle(
        this.Character.Position.X + newPoint.X,
        this.Character.Position.Y,
        this.Character.Width / this.Model.CurrentWorld.TileSize,
        this.Character.Height / this.Model.CurrentWorld.TileSize);
            Point actorLocation;

            for (int i = 0; i < actor.Width + 1; i++)
            {
                actorLocation = new Point(this.PixelToTile(actor.X + 1 + this.TileToPixel(i)), this.PixelToTile(actor.Y + this.Character.MovementVector.Y));
                if (this.Model.CurrentWorld.SearchGround(actorLocation))
                {
                    this.SetVectorY(0);
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
            this.Character.MovementVector = new Point(this.Character.MovementVector.X + x, this.Character.MovementVector.Y + y);
        }

        /// <summary>
        /// Sets x value of the vector.
        /// </summary>
        /// <param name="x">X value.</param>
        protected void SetVectorX(int x)
        {
            this.Character.MovementVector = new Point(x, this.Character.MovementVector.Y);
        }

        /// <summary>
        /// Sets y value of the vector.
        /// </summary>
        /// <param name="y">Y value.</param>
        protected void SetVectorY(int y)
        {
            this.Character.MovementVector = new Point(this.Character.MovementVector.X, y);
        }

        /// <summary>
        /// Wall collsiion.
        /// </summary>
        /// <param name="newPoint">New movement point.</param>
        /// <param name="rightWall">True if you want to check right wall collision, false if you would like to check left wall.</param>
        /// <returns>True if the movemnt would collide.</returns>
        protected bool WallCollision(Point newPoint, bool rightWall = true)
        {
            if (!this.CommandManager.IsFinished)
            {
                return false;
            }

            Rectangle actor = new Rectangle(
                this.Character.Position.X + newPoint.X,
                this.Character.Position.Y + newPoint.Y,
                this.Character.Width / this.Model.CurrentWorld.TileSize,
                this.Character.Height / this.Model.CurrentWorld.TileSize);
            Point actorLocation;

            if (rightWall)
            {
                // Right wall collision
                for (int i = 0; i < actor.Height; i++)
                {
                    actorLocation = new Point(this.PixelToTile(actor.X + 1 + this.Character.MovementVector.X) + actor.Width, this.PixelToTile(actor.Y) + i);
                    if (this.Model.CurrentWorld.SearchGround(actorLocation))
                    {
                        this.SetVectorX(0);
                        return true;
                    }
                }
            }
            else
            {
                // Left wall collision
                for (int i = 0; i < actor.Height; i++)
                {
                    actorLocation = new Point(this.PixelToTile(actor.X + this.Character.MovementVector.X), this.PixelToTile(actor.Y) + i);
                    if (this.Model.CurrentWorld.SearchGround(actorLocation))
                    {
                        this.SetVectorX(0);
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
            if (Math.Abs(this.Character.MovementVector.X) < this.MaxMovementSpeed)
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

                    if ((!this.GroundCollision(new Point(0, this.Acceleration + 1)) && this.Acceleration < GravityAcceleration) && !this.AccelerationStopwatch.IsRunning)
                    {
                        this.AccelerationStopwatch.Start();
                    }

                    if (this.AccelerationStopwatch.ElapsedMilliseconds > 100)
                    {
                        if (Math.Abs(this.Acceleration) <= 2)
                        {
                            this.Acceleration++;
                        }

                        this.AccelerationStopwatch.Restart();
                    }
                }
            }

            if (!this.WallCollision(newPoint) && !this.WallCollision(newPoint, false))
            {
                this.Character.Position = new Point(this.Character.Position.X + this.Character.MovementVector.X, this.Character.Position.Y);
            }

            if (!this.GroundCollision(newPoint) && (!this.WallCollision(newPoint) && !this.WallCollision(newPoint, false)))
            {
                if (!this.TopCollision(newPoint))
                {
                    this.Character.Position = new Point(this.Character.Position.X, this.Character.Position.Y + this.Character.MovementVector.Y);
                }
            }

            MoveCommand moveCommand = new MoveCommand(this.Character, this.Character.Position, this.Model);
            this.CommandManager.AddCommand(moveCommand);
            if (this.Character.MovementVector.X > 0)
            {
                this.AddToVector(-1, 0);
            }

            if (this.Character.MovementVector.X < 0)
            {
                this.AddToVector(1, 0);
            }
        }

        /// <summary>
        /// Default move funtion.
        /// </summary>
        /// <returns>New movement point.</returns>
        protected abstract Point Move();

        /// <summary>
        /// Jumping.
        /// </summary>
        /// <returns>Jumping value.</returns>
        protected virtual int Jump()
        {
            if (this.Character.CanJump || (!this.IsJumping && this.JumpingTimeOut.ElapsedMilliseconds > JumpTimer))
            {
                this.JumpingTimeOut.Restart();
                this.AccelerationStopwatch.Start();
                this.IsJumping = true;
                this.Character.CanJump = false;
                return this.MaxJumpHeight * -1;
            }

            return 0;
        }

        /// <summary>
        /// Default attack method.
        /// </summary>
        protected abstract void Attack();
    }
}
