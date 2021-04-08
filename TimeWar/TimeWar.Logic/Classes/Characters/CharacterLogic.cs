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
        private GameModel model;
        private Character character;
        private CommandManager commandManager;
        private bool isJumping;

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
        }

        /// <summary>
        /// 1 frame event.
        /// </summary>
        public void OneTick()
        {
            bool commandAdded = false;
            Point newPoint = this.Move();

            if (!this.Collision(newPoint))
            {
                MoveCommand command = new MoveCommand(this.character, newPoint, this.model);
                command.Execute();
                if (newPoint.Y == -1)
                {
                    int counter = 0;
                    while (counter < 14 && !this.Collision(newPoint))
                    {
                        command = new MoveCommand(this.character, newPoint, this.model);
                        command.Execute();
                        counter++;
                        this.commandManager.AddCommand(command);
                    }

                    commandAdded = true;
                }

                if (!this.Collision(new Point(0, 1)))
                {
                    MoveCommand gravity = new MoveCommand(this.character, new Point(0, 1), this.model);
                    gravity.Execute();
                }

                if (!commandAdded)
                {
                    this.commandManager.AddCommand(command);
                }
            }
        }

        private Point Move()
        {
            Point direction;
            switch (this.character.Direction)
            {
                case Stances.StandRight:
                    direction = new Point(0, 0);
                    break;
                case Stances.Right:
                    direction = new Point(1, 0);
                    break;
                case Stances.Left:
                    direction = new Point(-1, 0);
                    break;
                case Stances.Up:
                    direction = new Point(0, 0);
                    if (!this.isJumping)
                    {
                        this.isJumping = true;
                        direction = new Point(0, -1);
                    }

                    break;
                case Stances.Down:
                    direction = new Point(0, 1);
                    break;
                default:
                    direction = new Point(0, 0);
                    break;
            }

            return direction;
        }

        private bool Collision(Point newPoint)
        {
            Rectangle actor = new Rectangle(
                this.character.Position.X + newPoint.X,
                this.character.Position.Y + newPoint.Y,
                this.character.Width / 8,
                this.character.Height / 8);

            Point actorLocation;

            for (int i = 0; i < actor.Width; i++)
            {
                actorLocation = new Point(this.PixelToTile(actor.X) + i, this.PixelToTile(actor.Y - 1) + actor.Height);
                if (this.model.CurrentWorld.SearchGround(actorLocation))
                {
                    this.isJumping = false;
                    return true;
                }
            }

            for (int i = 0; i < actor.Height; i++)
            {
                actorLocation = new Point(this.PixelToTile(actor.X - 1) + actor.Width, this.PixelToTile(actor.Y) + i);
                if (this.model.CurrentWorld.SearchGround(actorLocation))
                {
                    return true;
                }
            }

            for (int i = 0; i < actor.Width; i++)
            {
                actorLocation = new Point(this.PixelToTile(actor.X) + i, this.PixelToTile(actor.Y));
                if (this.model.CurrentWorld.SearchGround(actorLocation))
                {
                    return true;
                }
            }

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
    }
}
