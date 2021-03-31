// <copyright file="MoveCommand.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Logic.Classes.Characters.Actions
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TimeWar.Logic.Interfaces;
    using TimeWar.Model;
    using TimeWar.Model.Objects.Interfaces;

    /// <summary>
    /// Move commands.
    /// </summary>
    public class MoveCommand : ICommand
    {
        private GameModel model;
        private IMoveable gameEntity;
        private Point direction;

        /// <summary>
        /// Initializes a new instance of the <see cref="MoveCommand"/> class.
        /// </summary>
        /// <param name="gameEntity">Moveable game entity.</param>
        /// <param name="direction">Moving direction.</param>
        /// <param name="model">Game model.</param>
        public MoveCommand(IMoveable gameEntity, Point direction, GameModel model)
        {
            this.gameEntity = gameEntity;
            this.direction = direction;
            this.model = model;
        }

        /// <inheritdoc/>
        public void Execute()
        {
            int newX = this.gameEntity.Position.X + (this.direction.X * this.gameEntity.Speed);
            int newY = this.gameEntity.Position.Y + (this.direction.Y * this.gameEntity.Speed);
            if (newX >= 0 && newX < this.model.CurrentWorld.GameWidth && newY >= 0 && newY < this.model.CurrentWorld.GameHeight)
            {
                this.gameEntity.Position = new Point(newX, newY);
            }
        }

        /// <inheritdoc/>
        public void Undo()
        {
            int newX = this.gameEntity.Position.X - (this.direction.X * this.gameEntity.Speed);
            int newY = this.gameEntity.Position.Y - (this.direction.Y * this.gameEntity.Speed);
            if (newX >= 0 && newX < this.model.CurrentWorld.GameWidth && newY >= 0 && newY < this.model.CurrentWorld.GameHeight)
            {
                this.gameEntity.Position = new Point(newX, newY);
            }
        }
    }
}
